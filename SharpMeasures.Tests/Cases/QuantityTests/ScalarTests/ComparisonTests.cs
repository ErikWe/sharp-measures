namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ScalarTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void Method(Scalar lhs, Scalar rhs)
    {
        Utility.QuantityTests.ComparisonTests.Method_ShouldMatchScalar(lhs, rhs);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void Operator(Scalar lhs, Scalar rhs)
    {
        Utility.QuantityTests.ComparisonTests.Operators_ShouldMatchDouble(lhs, rhs, lhs > rhs, lhs >= rhs, lhs <= rhs, lhs < rhs);
    }
}
