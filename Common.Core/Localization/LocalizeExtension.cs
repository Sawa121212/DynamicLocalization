﻿using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;

namespace Common.Core.Localization
{
    /// <summary>
    /// Binding an expression by key
    /// Привязка выражения по ключу
    /// </summary>
    public class LocalizeExtension : MarkupExtension
    {
        public LocalizeExtension(string key)
        {
            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var keyToUse = Key;
            if (!string.IsNullOrWhiteSpace(Context))
                keyToUse = $"{Context}/{Key}";

            var binding = new ReflectionBindingExtension($"[{keyToUse}]")
            {
                Mode = BindingMode.OneWay,
                Source = Localizer.Instance,
            };

            return binding.ProvideValue(serviceProvider);
        }

        public string Key { get; set; }

        public string Context { get; set; }
    }
}