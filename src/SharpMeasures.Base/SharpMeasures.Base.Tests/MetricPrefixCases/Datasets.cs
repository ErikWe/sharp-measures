namespace SharpMeasures.Tests.MetricPrefixCases;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

internal static class Datasets
{
    public static MetricPrefix GetNullMetricPrefix() => null!;
    public static MetricPrefix GetValidMetricPrefix() => MetricPrefixValues.Valid.First();

    public static class ExponentInt32Values
    {
        public static IEnumerable<int> Invalid => new[]
        {
            int.MaxValue
        };

        public static IEnumerable<int> Valid => new[]
        {
            0,
            1,
            -1,
            100,
            int.MinValue
        };
    }

    public static class MetricPrefixValues
    {
        public static IEnumerable<MetricPrefix> Valid
        {
            get
            {
                foreach (var exponent in ExponentInt32Values.Valid)
                {
                    yield return MetricPrefix.TenToThePower(exponent);
                    yield return MetricPrefix.ThousandToThePower(exponent);
                }
            }
        }
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class InvalidExponentInt32 : ATestDataset<int>
    {
        protected override IEnumerable<int> GetSamples() => ExponentInt32Values.Invalid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidExponentInt32 : ATestDataset<int>
    {
        protected override IEnumerable<int> GetSamples() => ExponentInt32Values.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidMetricPrefix : ATestDataset<MetricPrefix>
    {
        protected override IEnumerable<MetricPrefix> GetSamples() => MetricPrefixValues.Valid;
    }

    [SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
    public sealed class ValidMetricPrefix_ValidMetricPrefix : ATestDataset<(MetricPrefix, MetricPrefix)>
    {
        protected override IEnumerable<(MetricPrefix, MetricPrefix)> GetSamples() => Permutate(MetricPrefixValues.Valid);
    }
}
