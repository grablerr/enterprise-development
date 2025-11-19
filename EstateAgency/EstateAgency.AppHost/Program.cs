var builder = DistributedApplication.CreateBuilder(args);

var mySql = builder.AddMySql("mysql");

var mySqlDb = mySql.AddDatabase("RealEstateDb");

var api = builder.AddProject<Projects.EstateAgency_Api>("Api")
    .WithReference(mySqlDb, "DefaultConnection")
    .WaitFor(mySqlDb);

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);

var rabbitMq = builder.AddRabbitMQ("RabbitMQ", username, password)
    .WithManagementPlugin();

builder.AddProject<Projects.EstateAgency_RabbitMqConsumer>("RabbitMqConsumer")
    .WithReference(rabbitMq)
    .WithReference(mySqlDb, "DefaultConnection")
    .WaitFor(mySqlDb)
    .WaitFor(rabbitMq);

builder.AddProject<Projects.EstateAgency_RabbitMqProducer>("RabbitMqProducer")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WaitFor(api)
    .WaitFor(mySqlDb)
    .WithEnvironment("RabbitMQPublishDelayMs", "100");

builder.Build().Run();