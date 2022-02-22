#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<AccelerationDataset, AccelerationDataset>))]
    public void Method(Acceleration lhs, Acceleration rhs)
    {
        Utility.QuantityTests.ComparisonTests.Method_ShouldMatchScalar(lhs, rhs);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<AccelerationDataset,AccelerationDataset>))]
    public void Operator(Acceleration lhs, Acceleration rhs)
    {
        Utility.QuantityTests.ComparisonTests.Operators_ShouldMatchDouble(lhs, rhs, lhs > rhs, lhs >= rhs, lhs <= rhs, lhs < rhs);
    }
}
