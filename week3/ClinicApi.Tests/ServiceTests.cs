using ClinicApi.Contracts;
using ClinicApi.Domain;
using ClinicApi.Exceptions;
using ClinicApi.Repositories;
using ClinicApi.Services;
using Moq;

namespace ClinicApi.Tests;

public class PatientServiceTests
{
    private readonly Mock<IPatientRepository> _repo = new();
    private PatientService Sut => new(_repo.Object);

    [Fact]
    public async Task GetById_WhenMissing_ThrowsNotFound()
    {
        _repo.Setup(r => r.GetByIdAsync(99, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Patient?)null);

        await Assert.ThrowsAsync<NotFoundException>(() => Sut.GetByIdAsync(99, CancellationToken.None));
    }

    [Fact]
    public async Task Create_WhenFutureDob_ThrowsBusiness()
    {
        var dto = new CreatePatientDto
        {
            FullName = "Test",
            DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1))
        };

        await Assert.ThrowsAsync<BusinessException>(() => Sut.CreateAsync(dto, CancellationToken.None));
    }

    [Fact]
    public async Task Create_WhenValid_ReturnsDto()
    {
        _repo.Setup(r => r.AddAsync(It.IsAny<Patient>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Patient p, CancellationToken _) =>
            {
                p.Id = 7;
                return p;
            });

        var result = await Sut.CreateAsync(new CreatePatientDto
        {
            FullName = "  Sara Ali ",
            DateOfBirth = new DateOnly(1990, 1, 1),
            Phone = "0300"
        }, CancellationToken.None);

        Assert.Equal(7, result.Id);
        Assert.Equal("Sara Ali", result.FullName);
    }

    [Fact]
    public async Task GetPaged_NormalizesPageSize()
    {
        _repo.Setup(r => r.GetPagedAsync(1, 10, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Array.Empty<Patient>(), 0));

        var result = await Sut.GetPagedAsync(0, 999, CancellationToken.None);

        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
        _repo.Verify(r => r.GetPagedAsync(1, 10, It.IsAny<CancellationToken>()), Times.Once);
    }
}

public class AppointmentServiceTests
{
    private readonly Mock<IAppointmentRepository> _repo = new();
    private readonly Mock<IPatientRepository> _patients = new();
    private readonly Mock<IDoctorRepository> _doctors = new();
    private readonly Mock<IAppointmentReportRepository> _report = new();

    private AppointmentService Sut => new(_repo.Object, _patients.Object, _doctors.Object, _report.Object);

    [Fact]
    public async Task Create_WhenPatientMissing_ThrowsBusiness()
    {
        _patients.Setup(p => p.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Patient?)null);

        var dto = new CreateAppointmentDto
        {
            PatientId = 1,
            DoctorId = 1,
            ScheduledAt = DateTime.UtcNow.AddDays(1)
        };

        await Assert.ThrowsAsync<BusinessException>(() => Sut.CreateAsync(dto, CancellationToken.None));
    }

    [Fact]
    public async Task Create_WhenPast_ThrowsBusiness()
    {
        _patients.Setup(p => p.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Patient { Id = 1, FullName = "P" });
        _doctors.Setup(d => d.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Doctor { Id = 1, FullName = "D", Specialty = "X" });

        var dto = new CreateAppointmentDto
        {
            PatientId = 1,
            DoctorId = 1,
            ScheduledAt = DateTime.UtcNow.AddDays(-1)
        };

        await Assert.ThrowsAsync<BusinessException>(() => Sut.CreateAsync(dto, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateStatus_WhenInvalid_ThrowsBusiness()
    {
        _repo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Appointment { Id = 1, PatientId = 1, DoctorId = 1, ScheduledAt = DateTime.UtcNow });

        await Assert.ThrowsAsync<BusinessException>(() =>
            Sut.UpdateStatusAsync(1, new UpdateAppointmentStatusDto { Status = "Nope" }, CancellationToken.None));
    }

    [Fact]
    public async Task GetReport_DelegatesToAdoRepository()
    {
        var expected = new List<AppointmentReportRow>
        {
            new(1, "P", "D", "Cardio", DateTime.UtcNow, "Scheduled")
        };
        _report.Setup(r => r.GetReportAsync(1, null, null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await Sut.GetReportAsync(1, null, null, CancellationToken.None);

        Assert.Single(result);
        _report.Verify(r => r.GetReportAsync(1, null, null, It.IsAny<CancellationToken>()), Times.Once);
    }
}

public class DoctorServiceTests
{
    private readonly Mock<IDoctorRepository> _repo = new();
    private DoctorService Sut => new(_repo.Object);

    [Fact]
    public async Task Delete_WhenMissing_ThrowsNotFound()
    {
        _repo.Setup(r => r.DeleteAsync(5, It.IsAny<CancellationToken>())).ReturnsAsync(false);
        await Assert.ThrowsAsync<NotFoundException>(() => Sut.DeleteAsync(5, CancellationToken.None));
    }

    [Fact]
    public async Task Update_WhenExists_UpdatesFields()
    {
        var entity = new Doctor { Id = 2, FullName = "Old", Specialty = "OldSpec" };
        _repo.Setup(r => r.GetByIdAsync(2, It.IsAny<CancellationToken>())).ReturnsAsync(entity);

        var result = await Sut.UpdateAsync(2, new UpdateDoctorDto
        {
            FullName = "New Name",
            Specialty = "Cardiology",
            Email = "a@b.com"
        }, CancellationToken.None);

        Assert.Equal("New Name", result.FullName);
        Assert.Equal("Cardiology", result.Specialty);
        _repo.Verify(r => r.UpdateAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
    }
}
