using System;
using System.Reflection;
using Application;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApplication.Views;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AvaloniaApplication;

public partial class App : Avalonia.Application
{
    private IServiceProvider _services;

    public override void Initialize()
    {
        var builder = Host.CreateApplicationBuilder();

        builder.Configuration.AddJsonFile("appsettings.json");

        builder.Services.AddMediatRCommands();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        builder.Services.AddRepositories(builder.Configuration);
        builder.Services.AddViewModels();
        builder.Services.AddViews();

        _services = builder.Services.BuildServiceProvider();

        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            using var scope = _services.CreateScope();
            desktop.MainWindow = scope.ServiceProvider.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
