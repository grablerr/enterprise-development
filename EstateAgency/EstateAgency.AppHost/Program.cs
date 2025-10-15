var builder = DistributedApplication.CreateBuilder(args);

var mySql = builder.AddMySql("mysql");

var mySqlDb = mySql.AddDatabase("RealEstateDb");

var api = builder.AddProject<Projects.EstateAgency_Api>("Api")
    .WithReference(mySqlDb, "DefaultConnection")
    .WaitFor(mySqlDb);

builder.Build().Run();