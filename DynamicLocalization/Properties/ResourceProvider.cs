using System.Globalization;
using Common.Core.Localization;
using ModuleA.Properties;

namespace DynamicLocalization.Properties;

/// <summary>
/// Переключатель Языка в ПО
/// </summary>
public class ResourceProvider : IResourceProvider
{
    public ResourceProvider()
    {
        // Тут нужно сделать загрузчик настроек и задать начальный язык ПО
    }

    public void ChangeResources()
    {
        // задать начальный язык ПО
        Language.Culture = new CultureInfo("ru");
    }
}