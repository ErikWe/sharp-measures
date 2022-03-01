namespace ErikWe.SharpMeasures.Tests.Datasets;

using ErikWe.SharpMeasures.Units;

using System.Collections;
using System.Collections.Generic;

public class MetricPrefixDataset : IEnumerable<object?[]>
{
    public IEnumerator<object?[]> GetEnumerator()
    {
        yield return new object?[] { MetricPrefix.Yotta };
        yield return new object?[] { MetricPrefix.Zetta };
        yield return new object?[] { MetricPrefix.Exa };
        yield return new object?[] { MetricPrefix.Peta };
        yield return new object?[] { MetricPrefix.Tera };
        yield return new object?[] { MetricPrefix.Giga };
        yield return new object?[] { MetricPrefix.Mega };
        yield return new object?[] { MetricPrefix.Kilo };
        yield return new object?[] { MetricPrefix.Hecto };
        yield return new object?[] { MetricPrefix.Deca };
        yield return new object?[] { MetricPrefix.Identity };
        yield return new object?[] { MetricPrefix.Deci };
        yield return new object?[] { MetricPrefix.Centi };
        yield return new object?[] { MetricPrefix.Milli };
        yield return new object?[] { MetricPrefix.Micro };
        yield return new object?[] { MetricPrefix.Nano };
        yield return new object?[] { MetricPrefix.Pico };
        yield return new object?[] { MetricPrefix.Femto };
        yield return new object?[] { MetricPrefix.Atto };
        yield return new object?[] { MetricPrefix.Zepto };
        yield return new object?[] { MetricPrefix.Yocto };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}