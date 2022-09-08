#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.RotationalKineticEnergyTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class EqualityTests
{
    [Fact]
    public void Method_Null_ShouldBeInequal()
    {
        RotationalKineticEnergy quantity = new(0);
        RotationalKineticEnergy? nullQuantity = null;

        Assert.False(quantity.Equals(nullQuantity));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<RotationalKineticEnergyDataset, RotationalKineticEnergyDataset>))]
    public void Method(RotationalKineticEnergy quantity1, RotationalKineticEnergy quantity2)
    {
        Utility.QuantityTests.EqualityTests.Method_ShouldBeEqualIfEqualMagnitudeAndType(quantity1, quantity2);
    }

    [Fact]
    public void Operator_NullCases()
    {
        RotationalKineticEnergy quantity = new(0);
        RotationalKineticEnergy? nullQuantity1 = null;
        RotationalKineticEnergy? nullQuantity2 = null;

        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(quantity, nullQuantity2, quantity == nullQuantity1, quantity != nullQuantity1);
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(nullQuantity1, quantity, nullQuantity1 == quantity, nullQuantity1 != quantity);
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(nullQuantity1, nullQuantity2, nullQuantity1 == nullQuantity2, nullQuantity1 != nullQuantity2);
    }
    
    [Theory]
    [ClassData(typeof(GenericDataset<RotationalKineticEnergyDataset, RotationalKineticEnergyDataset>))]
    public void Operator(RotationalKineticEnergy quantity1, RotationalKineticEnergy quantity2)
    {
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(quantity1, quantity2, quantity1 == quantity2, quantity1 != quantity2);
    }
}