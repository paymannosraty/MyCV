﻿namespace Infrastructure.Middlewares
{
	public static class ExtentionMethods
	{
		static ExtentionMethods()
		{
		}

		public static IApplicationBuilder UseCultureCookie(this IApplicationBuilder app)
		{
			return app.UseMiddleware<CultureCookieHandlerMiddleware>();
		}

		public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app)
		{
			return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
		}

		public static IApplicationBuilder UseActivationKeys(this IApplicationBuilder app)
		{
			return app.UseMiddleware<ActivationKeysHandlerMiddleware>();
		}

		public static IApplicationBuilder UseCustomStaticFiles(this IApplicationBuilder app)
		{
			return app.UseMiddleware<CustomStaticFilesHandlerMiddleware>();
		}
	}
}
