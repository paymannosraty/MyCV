using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middlewares
{
	public class CustomStaticFilesHandlerMiddleware : object
	{
		public CustomStaticFilesHandlerMiddleware
			(RequestDelegate next) : base()
		{
			Next = next;
		}

		private RequestDelegate Next { get; }

		public async Task InvokeAsync(HttpContext httpContext, IHostEnvironment hostEnvironment)
		{
			var requestPath =
				httpContext.Request.Path.Value;

			if (string.IsNullOrWhiteSpace(requestPath) || requestPath == "/")
			{
				await Next(httpContext);
				return;
			}

			if (requestPath.StartsWith("/") == false)
			{
				await Next(httpContext);
				return;
			}

			requestPath =
				requestPath[1..];

			var rootPath =
				hostEnvironment.ContentRootPath;

			var physicalPathName =
					Path.Combine(path1: rootPath, path2: "wwwroot", path3: requestPath);

			if (File.Exists(physicalPathName) == false)
			{
				await Next(httpContext);
				return;
			}

			var fileExtension =
					Path.GetExtension(physicalPathName)?.ToLower();

			switch (fileExtension)
			{
				case ".htm":
				case ".html":
					{
						httpContext.Response.StatusCode = 200;
						httpContext.Response.ContentType = "text/html";
						break;
					}

				case ".css":
					{
						httpContext.Response.StatusCode = 200;
						httpContext.Response.ContentType = "text/css";
						break;
					}

				case ".js":
					{
						httpContext.Response.StatusCode = 200;
						httpContext.Response.ContentType = "application/x-javascript";
						break;
					}

				case ".jpg":
				case ".jpeg":
					{
						httpContext.Response.StatusCode = 200;
						httpContext.Response.ContentType = "image/jpeg";
						break;
					}

				case ".txt":
					{
						httpContext.Response.StatusCode = 200;
						httpContext.Response.ContentType = "text/plain";
						break;
					}

				default:
					{
						await Next(httpContext);
						return;
					}
			}

			await httpContext.Response
				.SendFileAsync(fileName: physicalPathName);
		}
	}
}
