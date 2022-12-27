using System.Resources;
using Common.Core.Localization;
using ModuleA.Properties;
using ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleA
{
    /// <summary>
    /// Модуль A
    /// </summary>
    public class ModuleAModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleAModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // регистрируем View для навигации по Регионам
            containerRegistry.RegisterForNavigation<TabAView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Добавим ресурс Локализации в "коллекцию ресурсов локализации"
            containerProvider.Resolve<ILocalizer>().AddResourceManager(new ResourceManager(typeof(Language)));

            // Зарегистрировать View к региону. Теперь при запуске ПО View будет показано
            _regionManager.RegisterViewWithRegion("RegionA", typeof(TabAView));
        }
    }
}