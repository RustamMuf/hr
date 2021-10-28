using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Text;
using System.Threading.Tasks;
using Utg.HR.Api.Configuration;
using Utg.HR.Api.Middlewares;
using Utg.HR.BL.Configuration;
using FluentValidation.AspNetCore;
using Utg.HR.Dal.Configuration;
using Utg.HR.API.Validation;

namespace Utg.HR.Api
{
	public class Startup
	{
		private readonly IConfiguration configuration;
		public Startup(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddControllers();
			//services.AddControllers(options =>
			//{
			//	options.Filters.Add(new HttpResponseExceptionFilter());
			//}).AddFluentValidation();

			services.AddControllers(options =>
			{
				options.Filters.Add(new HttpResponseExceptionFilter());
			})
				.AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
			 .RegisterFluentValidation();

			services.AddSwaggerDocument(c =>
			{
				c.AddSecurity("JWT", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Cookie",
					In = OpenApiSecurityApiKeyLocation.Cookie,
					Type = OpenApiSecuritySchemeType.ApiKey
				});
				c.OperationProcessors.Add(
				   new AspNetCoreOperationSecurityScopeProcessor("JWT"));
				c.PostProcess = doc =>
				{
					doc.Info.Version = "v1";
					doc.Info.Title = "Utg HR Api";
					doc.Info.Description = "The documentation Utg HR API";
				};
			});

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = false;
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = configuration["Jwt:Issuer"],
						ValidAudience = configuration["Jwt:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
						ClockSkew = TimeSpan.Zero
					};
					options.Events = new JwtBearerEvents
					{
						OnAuthenticationFailed = context =>
						{
							if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
							{
								context.Response.Headers.Add("Token-Expired", "true");
							}
							return Task.CompletedTask;
						}
					};
				});

			services.ConfigureDal(configuration)
			  .ConfigureBL();

			var config = new MapperConfiguration(cfg =>
			{
				ApiMapperConfiguration.ConfigureMappings(cfg);
			});

			config.AssertConfigurationIsValid();
			var mapper = config.CreateMapper();
			services.AddSingleton(mapper);

			services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
			{
				builder.AllowAnyOrigin()
					   .AllowAnyMethod()
					   .AllowAnyHeader();
			}));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder builder, IWebHostEnvironment env)
		{
			var swaggerPrefix = string.Empty;
			if (env.IsDevelopment())
			{
				builder.UseDeveloperExceptionPage();
			}
			else
			{
				builder.UseHttpsRedirection();
			}

			builder.UseCors("MyPolicy");
			builder.UseRouting();

			builder.UseAuthentication();
			builder.UseAuthorization();
			builder.UseMiddleware<ErrorHandlingMiddleware>();

			builder.UseEndpoints(endpoints => endpoints.MapControllers());


			var swaggerPath = $"/hr/swagger";
			builder.UseOpenApi(options =>
			{
				options.PostProcess = (document, _) => document.Schemes = new[] { OpenApiSchema.Https };
				options.Path = $"{swaggerPath}/v1/swagger.json";
			});
			builder.UseSwaggerUi3(options =>
			{
				options.Path = swaggerPath;
				options.DocumentPath = $"/{swaggerPath}/" + "v1" + "/swagger.json";
			});


			Dal.Configuration.Startup.Initialize(builder);
		}
	}
}
