using System;

namespace CTOnline.OpenServicesFx
{
    public interface IMetricsService
    {
        IDisposable MeasureDuration(string metricName, string[] tags = null);
        void Increment(string statName, int value = 1, string[] tags = null);
        void Decrement(string statName, int value = 1, string[] tags = null);
    }
}
