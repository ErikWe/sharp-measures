#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.MomentOfInertiaTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class EqualityTests
{
    [Fact]
    public void Method_Null_ShouldBeInequal()
    {
        MomentOfInertia quantity = new(0);
        MomentOfInertia? nullQuantity = null;

        Assert.False(quantity.Equals(nullQuantity));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<MomentOfInertiaDataset, MomentOfInertiaDataset>))]
    public void Method(MomentOfInertia quantity1, MomentOfInertia quantity2)
    {
        Utility.QuantityTests.EqualityTests.Method_ShouldBeEqualIfEqualMagnitudeAndType(quantity1, quantity2);
    }

    [Fact]
    public void Operator_NullCases()
    {
        MomentOfInertia quantity = new(0);
        MomentOfInertia? nullQuantity1 = null;
        MomentOfInertia? nullQuantity2 = null;

        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(quantity, nullQuantity2, quantity == nullQuantity1, quantity != nullQuantity1);
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(nullQuantity1, quantity, nullQuantity1 == quantity, nullQuantity1 != quantity);
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(nullQuantity1, nullQuantity2, nullQuantity1 == nullQuantity2, nullQuantity1 != nullQuantity2);
    }
    
    [Theory]
    [ClassData(typeof(GenericDataset<MomentOfInertiaDataset, MomentOfInertiaDataset>))]
    public void Operator(MomentOfInertia quantity1, MomentOfInertia quantity2)
    {
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(quantity1, quantity2, quantity1 == quantity2, quantity1 != quantity2);
    }
}
