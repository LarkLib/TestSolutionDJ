namespace WebMinimalAPIAuthApplication
{
    using System.Security.Claims;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var Configuration = builder.Configuration;
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
            });

            var app = builder.Build();

            app.UseAuthorization();

            app.MapGet("/", () => "Hello, World!");
            app.MapGet("/secret", (ClaimsPrincipal user) => $"Hello {user.Identity?.Name}. My secret")
                .RequireAuthorization();

            app.MapGet("/secret2", () => "This is a different secret!")
                .RequireAuthorization(p => p.RequireClaim("scope", "myapi:secrets"));

            app.Run();
        }
    }
}