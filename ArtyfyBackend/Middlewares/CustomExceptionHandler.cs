using ArtyfyBackend.Bll.Exceptions;
using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Responses;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace ArtyfyBackend.API.Middlewares
{
	public static class CustomExceptionHandler
	{
		public static void UseCustomExceptionHandler(this IApplicationBuilder app)
		{
			app.UseExceptionHandler(config =>
			{
				config.Run(async context =>
				{
					context.Response.ContentType = "application/json";

					var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

					var statusCode = exceptionFeature.Error switch
					{
						ClientSideException => 400,
						NotFoundException => 404,
						_ => 500
					};
					context.Response.StatusCode = statusCode;

					var response = Response<NoDataModel>.Fail(exceptionFeature.Error.Message, statusCode, true);

					await context.Response.WriteAsync(JsonSerializer.Serialize(response));
				});
			});
		}
	}
}
