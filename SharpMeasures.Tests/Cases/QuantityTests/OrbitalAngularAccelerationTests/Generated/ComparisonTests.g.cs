#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularAccelerationDataset, OrbitalAngularAccelerationDataset>))]
    public void Method(OrbitalAngularAcceleration lhs, OrbitalAngularAcceleration rhs)
    {
        Utility.QuantityTests.ComparisonTests.Method_ShouldMatchScalar(lhs, rhs);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularAccelerationDataset,OrbitalAngularAccelerationDataset>))]
    public void Operator(OrbitalAngularAcceleration lhs, OrbitalAngularAcceleration rhs)
    {
        Utility.QuantityTests.ComparisonTests.Operators_ShouldMatchDouble(lhs, rhs, lhs > rhs, lhs >= rhs, lhs <= rhs, lhs < rhs);
    }
}
