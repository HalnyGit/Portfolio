using Microsoft.Extensions.DependencyInjection;
using Portfolio;
using Portfolio.Data.CsvReader;
using Portfolio.Data.CsvReader.Extensions;
using Portfolio.Data.DataProviders;
using Portfolio.Entities;
using Portfolio.FileManager;
using Portfolio.Repositories;
using Portfolio.Repositories.EventHandlers;
using Portfolio.UserCommunication;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Bond>, ListRepository<Bond>>();
services.AddSingleton<IBondsProvider, BondsProvider>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IFileManager, FileManager>();
services.AddSingleton<EventHandlers>();
services.AddSingleton<MarketStats>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();
app.Run();
