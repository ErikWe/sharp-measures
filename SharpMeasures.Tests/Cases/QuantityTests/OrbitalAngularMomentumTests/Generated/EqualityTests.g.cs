#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.OrbitalAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class EqualityTests
{
    [Fact]
    public void Method_Null_ShouldBeInequal()
    {
        OrbitalAngularMomentum quantity = new(0);
        OrbitalAngularMomentum? nullQuantity = null;

        Assert.False(quantity.Equals(nullQuantity));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularMomentumDataset, OrbitalAngularMomentumDataset>))]
    public void Method(OrbitalAngularMomentum quantity1, OrbitalAngularMomentum quantity2)
    {
        Utility.QuantityTests.EqualityTests.Method_ShouldBeEqualIfEqualMagnitudeAndType(quantity1, quantity2);
    }

    [Fact]
    public void Operator_NullCases()
    {
        OrbitalAngularMomentum quantity = new(0);
        OrbitalAngularMomentum? nullQuantity1 = null;
        OrbitalAngularMomentum? nullQuantity2 = null;

        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(quantity, nullQuantity2, quantity == nullQuantity1, quantity != nullQuantity1);
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(nullQuantity1, quantity, nullQuantity1 == quantity, nullQuantity1 != quantity);
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(nullQuantity1, nullQuantity2, nullQuantity1 == nullQuantity2, nullQuantity1 != nullQuantity2);
    }
    
    [Theory]
    [ClassData(typeof(GenericDataset<OrbitalAngularMomentumDataset, OrbitalAngularMomentumDataset>))]
    public void Operator(OrbitalAngularMomentum quantity1, OrbitalAngularMomentum quantity2)
    {
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(quantity1, quantity2, quantity1 == quantity2, quantity1 != quantity2);
    }
}
