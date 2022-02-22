#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TemperatureTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class EqualityTests
{
    [Fact]
    public void Method_Null_ShouldBeInequal()
    {
        Temperature quantity = new(0);
        Temperature? nullQuantity = null;

        Assert.False(quantity.Equals(nullQuantity));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TemperatureDataset, TemperatureDataset>))]
    public void Method(Temperature quantity1, Temperature quantity2)
    {
        Utility.QuantityTests.EqualityTests.Method_ShouldBeEqualIfEqualMagnitudeAndType(quantity1, quantity2);
    }

    [Fact]
    public void Operator_NullCases()
    {
        Temperature quantity = new(0);
        Temperature? nullQuantity1 = null;
        Temperature? nullQuantity2 = null;

        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(quantity, nullQuantity2, quantity == nullQuantity1, quantity != nullQuantity1);
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(nullQuantity1, quantity, nullQuantity1 == quantity, nullQuantity1 != quantity);
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(nullQuantity1, nullQuantity2, nullQuantity1 == nullQuantity2, nullQuantity1 != nullQuantity2);
    }
    
    [Theory]
    [ClassData(typeof(GenericDataset<TemperatureDataset, TemperatureDataset>))]
    public void Operator(Temperature quantity1, Temperature quantity2)
    {
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(quantity1, quantity2, quantity1 == quantity2, quantity1 != quantity2);
    }
}
