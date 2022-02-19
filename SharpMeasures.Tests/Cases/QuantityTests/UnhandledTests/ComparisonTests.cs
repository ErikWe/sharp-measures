namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.UnhandledTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Tests.Datasets.Unhandled;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset>))]
    public void Method(Unhandled lhs, Unhandled rhs)
    {
        Utility.QuantityTests.ComparisonTests.Method_ShouldMatchScalar(lhs, rhs);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset>))]
    public void Operator(Unhandled lhs, Unhandled rhs)
    {
        Utility.QuantityTests.ComparisonTests.Operators_ShouldMatchDouble(lhs, rhs, lhs > rhs, lhs >= rhs, lhs <= rhs, lhs < rhs);
    }
}
