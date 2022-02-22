#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.KineticEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<KineticEnergyDataset, KineticEnergyDataset>))]
    public void Method(KineticEnergy lhs, KineticEnergy rhs)
    {
        Utility.QuantityTests.ComparisonTests.Method_ShouldMatchScalar(lhs, rhs);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<KineticEnergyDataset,KineticEnergyDataset>))]
    public void Operator(KineticEnergy lhs, KineticEnergy rhs)
    {
        Utility.QuantityTests.ComparisonTests.Operators_ShouldMatchDouble(lhs, rhs, lhs > rhs, lhs >= rhs, lhs <= rhs, lhs < rhs);
    }
}
