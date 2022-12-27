using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Common.Core.Localization;
using DynamicLocalization.Properties;
using DynamicLocalization.Views;
using ModuleA;
using ModuleB;
using ModuleC;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using IResourceProvider = Common.Core.Localization.IResourceProvider;

namespace DynamicLocalization;

public class App : PrismApplication
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        base.Initialize();
    }


    protected override Window CreateShell()
    {
        return Container.Resolve<ShellView>();
    }

    /// <summary>
    /// Регистрация служб приложения
    /// </summary>
    /// <param name="containerRegistry"></param>
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .RegisterSingleton<ILocalizer, Localizer>()
            .RegisterSingleton<IResourceProvider, ResourceProvider>(Assembly.GetExecutingAssembly().FullName)
            ;
    }

    /// <summary>
    /// Регистрация модулей приложения
    /// </summary>
    /// <param name="moduleCatalog"></param>
    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        moduleCatalog
            .AddModule<ModuleAModule>()
            .AddModule<ModuleBModule>()
            .AddModule<ModuleCModule>();

        base.ConfigureModuleCatalog(moduleCatalog);
    }

    /// <summary>
    /// ViewModel Locator. Мы работаем с View, а не с ViewModel!
    /// Ищем ViewModel для View в той же папке, где и View лежит.
    /// </summary>
    protected override void ConfigureViewModelLocator()
    {
        base.ConfigureViewModelLocator();

        ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
        {
            var viewName = viewType.FullName;
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;

            var viewModelName = string.Format(
                viewName != null && viewName.EndsWith("View", StringComparison.OrdinalIgnoreCase)
                    ? "{0}Model, {1}"
                    : "{0}ViewModel, {1}",
                viewName, viewAssemblyName);
            return Type.GetType(viewModelName);
        });

        ViewModelLocationProvider.SetDefaultViewModelFactory(type => Container.Resolve(type));
    }
}