#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularMomentumDataset, OrbitalAngularMomentumDataset>))]
    public void Method(OrbitalAngularMomentum lhs, OrbitalAngularMomentum rhs)
    {
        Utility.QuantityTests.ComparisonTests.Method_ShouldMatchScalar(lhs, rhs);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularMomentumDataset,OrbitalAngularMomentumDataset>))]
    public void Operator(OrbitalAngularMomentum lhs, OrbitalAngularMomentum rhs)
    {
        Utility.QuantityTests.ComparisonTests.Operators_ShouldMatchDouble(lhs, rhs, lhs > rhs, lhs >= rhs, lhs <= rhs, lhs < rhs);
    }
}
