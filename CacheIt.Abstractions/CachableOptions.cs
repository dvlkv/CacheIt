using System;
using System.Collections.Generic;

namespace CacheIt.Abstractions
{
    public class CachableOptions<TEntryOptions> where TEntryOptions : new()
    {
        private TEntryOptions _defaultEntryOptions;
        private Dictionary<Type, TEntryOptions> _customEntryOptions;

        public CachableOptions()
        {
            _customEntryOptions = new Dictionary<Type, TEntryOptions>();
            _defaultEntryOptions = new TEntryOptions();
        }

        public TEntryOptions GetFor(Type t)
        {
            return _customEntryOptions.ContainsKey(t) ? _customEntryOptions[t] : _defaultEntryOptions;
        }

        public void ConfigureAll(Action<TEntryOptions> configurator)
        {
            configurator(_defaultEntryOptions);
        }

        public void Configure<T>(Action<TEntryOptions> configurator)
        {
            var optionsForType = new TEntryOptions();
            configurator(optionsForType);

            _customEntryOptions[typeof(T)] = optionsForType;
        }
    }
}