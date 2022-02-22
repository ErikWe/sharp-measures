#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularSpeedTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularSpeedDataset, OrbitalAngularSpeedDataset>))]
    public void Method(OrbitalAngularSpeed lhs, OrbitalAngularSpeed rhs)
    {
        Utility.QuantityTests.ComparisonTests.Method_ShouldMatchScalar(lhs, rhs);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularSpeedDataset,OrbitalAngularSpeedDataset>))]
    public void Operator(OrbitalAngularSpeed lhs, OrbitalAngularSpeed rhs)
    {
        Utility.QuantityTests.ComparisonTests.Operators_ShouldMatchDouble(lhs, rhs, lhs > rhs, lhs >= rhs, lhs <= rhs, lhs < rhs);
    }
}
