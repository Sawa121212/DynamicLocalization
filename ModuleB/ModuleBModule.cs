using System.Resources;
using Common.Core.Localization;
using ModuleB.Properties;
using ModuleB.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleB
{
    /// <summary>
    /// Модуль B
    /// </summary>
    public class ModuleBModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleBModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Регистрируем View для навигации по Регионам
            containerRegistry.RegisterForNavigation<TabBView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Добавим ресурс Локализации в "коллекцию ресурсов локализации"
            containerProvider.Resolve<ILocalizer>().AddResourceManager(new ResourceManager(typeof(Language)));

            // Зарегистрировать View к региону. Теперь при запуске ПО View будет показано
            _regionManager.RegisterViewWithRegion("RegionB", typeof(TabBView));
        }
    }
}