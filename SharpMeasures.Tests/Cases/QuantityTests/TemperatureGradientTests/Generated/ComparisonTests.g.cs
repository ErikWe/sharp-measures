#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureGradientTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<TemperatureGradientDataset, TemperatureGradientDataset>))]
    public void Method(TemperatureGradient lhs, TemperatureGradient rhs)
    {
        Utility.QuantityTests.ComparisonTests.Method_ShouldMatchScalar(lhs, rhs);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TemperatureGradientDataset,TemperatureGradientDataset>))]
    public void Operator(TemperatureGradient lhs, TemperatureGradient rhs)
    {
        Utility.QuantityTests.ComparisonTests.Operators_ShouldMatchDouble(lhs, rhs, lhs > rhs, lhs >= rhs, lhs <= rhs, lhs < rhs);
    }
}
