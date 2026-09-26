using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;
using WebApiDemo.Services;

namespace WebApiDemo.Controllers;

// Express: router.get('/', ...)  router.post('/', ...)
[ApiController]
[Route("api/[controller]")] // -> /api/patients
public class PatientsController : ControllerBase
{
    private readonly IPatientService _service;

    // Constructor injection = Express middleware deps, but typed & testable
    public PatientsController(IPatientService service) => _service = service;

    [HttpGet]
    public ActionResult<IEnumerable<PatientDto>> GetAll() => Ok(_service.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<PatientDto> GetById(int id)
    {
        var patient = _service.GetById(id);
        return patient is null ? NotFound() : Ok(patient);
    }

    [HttpPost]
    public ActionResult<PatientDto> Create([FromBody] CreatePatientDto dto)
    {
        // [ApiController] auto-returns 400 if DataAnnotations fail (like express-validator)
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var created = _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created); // 201
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id) =>
        _service.Delete(id) ? NoContent() : NotFound(); // 204 or 404
}
