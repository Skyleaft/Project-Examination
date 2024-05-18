var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.API>("api");
var web = builder.AddProject<Projects.Web>("web");

api.WithReference(web);
builder.Build().Run();