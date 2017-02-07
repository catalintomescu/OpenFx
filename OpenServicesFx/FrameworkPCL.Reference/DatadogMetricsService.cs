using System;
using StatsdClient;

namespace CTOnline.OpenServicesFx.Reference
{
    public class DatadogMetricsService : IMetricsService
    {
        #region IMetricsService Members

        public IDisposable MeasureDuration(string metricName, string[] tags = null)
        {
            return DogStatsd.StartTimer(metricName, tags: tags);
        }

        public void Increment(string statName, int value = 1, string[] tags = null)
        {
            DogStatsd.Increment(statName, value: value, tags: tags);
        }

        public void Decrement(string statName, int value = 1, string[] tags = null)
        {
            DogStatsd.Decrement(statName, value: value, tags: tags);
        }

        #endregion
    }
}
