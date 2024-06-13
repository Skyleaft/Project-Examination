var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.API>("apiservice").AsHttp2Service();

var web = builder.AddProject<Projects.Web>("web")
    .WithEndpoint("https", endpoint => {
        endpoint.IsProxied = false;
    });

web.WithReference(api);

builder.Build().Run();
