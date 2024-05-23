namespace Artyfy.WebAPI.Extensions;

public static class CorsExtension
{
	public static void UseCors(this IServiceCollection services)
	{
		services.AddCors(options =>
		{
			//that allows other members to reach my swagger, customization of swagger
			options.AddPolicy("MyAllowedOrigins",
				policy =>
				{
					policy.WithOrigins("http://localhost:8080") 
						.AllowAnyHeader()
						.AllowAnyMethod();
					
					policy.WithOrigins("http://artyfy.com.tr") 
						.AllowAnyHeader()
						.AllowAnyMethod();
					
					policy.WithOrigins("https://artyfy.com.tr") 
						.AllowAnyHeader()
						.AllowAnyMethod();
					
					policy.WithOrigins("https://www.artyfy.com.tr") 
						.AllowAnyHeader()
						.AllowAnyMethod();
				});
		});
	}
}