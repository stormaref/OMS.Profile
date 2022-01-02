using OMS.Profile.Application.Common.Settings;
using Serilog;

namespace OMS.Profile.API.Common.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigSeq(this WebApplicationBuilder builder)
    {
        var seqUrl = builder.Configuration.GetValue<string>("Seq");
        builder.Host.UseSerilog((ctx, lc) => lc
            .MinimumLevel.Debug()
            .Enrich.WithProperty("Service", "Profile")
            .WriteTo.Seq(seqUrl));
    }
    
    public static void AddSettings<T>(this WebApplicationBuilder builder) where T : class
    {
        builder.Services.Configure<T>(
            builder.Configuration.GetSection(typeof(T).Name));
    }
}