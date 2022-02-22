#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.LinearDensityTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<LinearDensityDataset, LinearDensityDataset>))]
    public void Method(LinearDensity lhs, LinearDensity rhs)
    {
        Utility.QuantityTests.ComparisonTests.Method_ShouldMatchScalar(lhs, rhs);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<LinearDensityDataset,LinearDensityDataset>))]
    public void Operator(LinearDensity lhs, LinearDensity rhs)
    {
        Utility.QuantityTests.ComparisonTests.Operators_ShouldMatchDouble(lhs, rhs, lhs > rhs, lhs >= rhs, lhs <= rhs, lhs < rhs);
    }
}
