using WebApiDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Express analogy:
//   app.use(express.json())     -> model binding is built-in
//   app.use('/patients', router)-> MapControllers + [Route]
//   app.listen(port)            -> app.Run()
//   require('...') DI           -> builder.Services.Add...

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IPatientService, InMemoryPatientService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.Run();
