using System.Resources;
using Common.Core.Localization;
using ModuleC.Properties;
using ModuleC.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleC
{
    /// <summary>
    /// Модуль C
    /// </summary>
    public class ModuleCModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleCModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // регистрируем View для навигации по Регионам
            containerRegistry.RegisterForNavigation<TabCView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Добавим ресурс Локализации в "коллекцию ресурсов локализации"
            containerProvider.Resolve<ILocalizer>().AddResourceManager(new ResourceManager(typeof(Language)));

            // Зарегистрировать View к региону. Теперь при запуске ПО View будет показано
            _regionManager.RegisterViewWithRegion("RegionC", typeof(TabCView));
        }
    }
}