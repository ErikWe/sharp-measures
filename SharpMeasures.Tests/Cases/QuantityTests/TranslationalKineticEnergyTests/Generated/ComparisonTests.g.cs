#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TranslationalKineticEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ComparisonTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<TranslationalKineticEnergyDataset, TranslationalKineticEnergyDataset>))]
    public void Method(TranslationalKineticEnergy lhs, TranslationalKineticEnergy rhs)
    {
        Utility.QuantityTests.ComparisonTests.Method_ShouldMatchScalar(lhs, rhs);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TranslationalKineticEnergyDataset,TranslationalKineticEnergyDataset>))]
    public void Operator(TranslationalKineticEnergy lhs, TranslationalKineticEnergy rhs)
    {
        Utility.QuantityTests.ComparisonTests.Operators_ShouldMatchDouble(lhs, rhs, lhs > rhs, lhs >= rhs, lhs <= rhs, lhs < rhs);
    }
}
