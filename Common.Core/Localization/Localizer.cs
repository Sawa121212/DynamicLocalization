﻿using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace Common.Core.Localization
{
    /// <summary>
    /// Локализатор
    /// </summary>
    public class Localizer : ILocalizer, INotifyPropertyChanged
    {
        public readonly string FALLBACK_LANGUAGE = LanguagesEnum.ru.ToString();

        private const string IndexerName = "Item";
        private const string IndexerArrayName = "Item[]";
        private List<ResourceManager>? _resources;
        private static List<ResourceManager>? _resourceManagers = new();
        public event PropertyChangedEventHandler? PropertyChanged;

        public Localizer()
        {
            LoadLanguage();
        }

        public void LoadLanguage()
        {
            if (_resourceManagers != null)
                _resources = new List<ResourceManager>(_resourceManagers);
            InvalidateEvents();
        }

        public void EditLn(string language)
        {
            Instance.ChangeLanguage(language);
        }

        /// <summary>
        /// Добавит Ресурс в коллекцию
        /// </summary>
        /// <param name="resourceManager"></param>
        public void AddResourceManager(ResourceManager resourceManager)
        {
            _resourceManagers?.Add(resourceManager);
        }

        /// <summary>
        /// Изменить локализацию
        /// </summary>
        /// <param name="language"></param>
        public void ChangeLanguage(string language)
        {
            if (string.IsNullOrEmpty(language))
            {
                language = FALLBACK_LANGUAGE;
            }

            CultureInfo.CurrentUICulture = new CultureInfo(language);
            LoadLanguage();
        }

        public string TryUseSystemLanguageFallbackEnglish()
        {
            // todo 
            var curLang = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

            ChangeLanguage(curLang);
            return curLang;
        }

        /// <summary>
        /// Получить Выражение из Ресурсов по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        public string this[string key]
        {
            get
            {
                if (_resources == null || !_resources.Any())
                    return "<ERROR! LANGUAGE Resources is empty>";

                var row = GetExpression(key);

                if (string.IsNullOrEmpty(row))
                    return $"<ERROR! Not found key \"{key}\">";

                var ret = row?.Replace(@"\\n", "\n");
                if (string.IsNullOrEmpty(ret))
                {
                    ret = $"Localize:{key}";
                }

                return ret;
            }
        }

        /// <summary>
        /// выражение слов
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetExpression(string key)
        {
            if (_resources == null) return string.Empty;
            foreach (var resource in _resources)
            {
                var row = resource?.GetString(key);
                if (!string.IsNullOrEmpty(row))
                    return row;
            }

            return string.Empty;
        }

        public void InvalidateEvents()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(IndexerArrayName));
        }

        /// <summary>
        /// Экземпляр
        /// </summary>
        public static Localizer Instance { get; set; } = new Localizer();
    }
}