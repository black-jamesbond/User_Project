using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using User_Project.DbContexts;
using User_Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddDbContext<UserContext>(dbContextOptions
    => dbContextOptions.UseSqlite("Data Source=UserInformation.db"));

builder.Services.AddAuthentication("Bearer")
   .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new()
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = builder.Configuration["Authentication:Issuer"],
               ValidAudience = builder.Configuration["Authentication:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(
                   Convert.FromBase64String(builder.Configuration["Authentication:SecretForKey"]
                   ))
           };
       }
   );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
