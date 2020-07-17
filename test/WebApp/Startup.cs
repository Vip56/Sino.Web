using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using NLog;
using Sino.Web.Filter;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApp.Services;
using NLog.Extensions.Logging;

namespace WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private const string SecretKey = "xxxxxaaaaa";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public IConfigurationRoot Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IValueService, ValueService>();

            CheckSignatureFilter.Token = "123456fefef";
            services.AddMvc(x =>
            {
                x.Filters.Add(typeof(GlobalResultFilter));
                //x.Filters.Add(typeof(CheckSignatureFilter));
                x.Filters.Add(typeof(InputOutputLogFilter));
            });
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Fastest);
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = new[]
                {
                    "application/json",
                    "text/json"
                };
            });

            var jwtSection = Configuration.GetSection(nameof(JwtOptions));

            services.Configure<JwtOptions>(options =>
            {
                options.Audience = jwtSection[nameof(JwtOptions.Audience)];
                options.Issuer = jwtSection[nameof(JwtOptions.Issuer)];
                options.SigningKey = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
            return services.BuildServiceProvider();
            //return new AspectCoreServiceProviderFactory().CreateServiceProvider(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            loggerFactory.ConfigureNLog($"nlog.{env.EnvironmentName}.config");

            app.UseResponseCompression();
            //app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    ExpireTimeSpan = TimeSpan.FromDays(1),
            //    AuthenticationScheme = "Automatic",
            //    AutomaticAuthenticate = true,
            //    Events = new DefaultAuthenticationEvents()
            //});
            var jwtSection = Configuration.GetSection(nameof(JwtOptions));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                ValidateIssuer = true,
                ValidIssuer = jwtSection[nameof(JwtOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtSection[nameof(JwtOptions.Audience)],

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            //var bearerOptions = new JwtBearerOptions()
            //{
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = false,
            //    AuthenticationScheme = "Automatic",
            //    TokenValidationParameters = tokenValidationParameters,
            //    Events = new Test()
            //};
            //app.UseJwtBearerAuthentication(bearerOptions);
            app.UseGlobalExceptionHandler(LogManager.GetCurrentClassLogger());
            app.UseMvc();
        }
    }

    public class Test : JwtBearerEvents
    {
        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            return base.AuthenticationFailed(context);
        }

        public override Task Challenge(JwtBearerChallengeContext context)
        {
            return base.Challenge(context);
        }

        public override Task MessageReceived(MessageReceivedContext context)
        {
            return base.MessageReceived(context);
        }

        public override Task TokenValidated(TokenValidatedContext context)
        {
            return base.TokenValidated(context);
        }
    }
}
