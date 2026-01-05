var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ENABLE SWAGGER (IMPORTANT: NO IF CONDITION)
app.UseSwagger();
app.UseSwaggerUI();

// Comment HTTPS redirection for now
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
