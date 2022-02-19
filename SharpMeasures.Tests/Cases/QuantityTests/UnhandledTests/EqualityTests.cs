namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.UnhandledTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Tests.Datasets.Unhandled;

using Xunit;

public class EqualityTests
{
    [Fact]
    public void Method_Null_ShouldBeInequal()
    {
        Scalar scalar = Scalar.Zero;
        Scalar? nullScalar = null;

        Assert.False(scalar.Equals(nullScalar));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset>))]
    public void Method(Unhandled quantity1, Unhandled quantity2)
    {
        Utility.QuantityTests.EqualityTests.Method_ShouldBeEqualIfEqualMagnitudeAndType(quantity1, quantity2);
    }

    [Fact]
    public void Operator_NullCases()
    {
        Unhandled scalar = Unhandled.Zero;
        Unhandled? nullScalar1 = null;
        Unhandled? nullScalar2 = null;

        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(scalar, nullScalar1, scalar == nullScalar1, scalar != nullScalar1);
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(nullScalar1, scalar, nullScalar1 == scalar, nullScalar1 != scalar);
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(nullScalar1, nullScalar2, nullScalar1 == nullScalar2, nullScalar1 != nullScalar2);
    }
    
    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, UnhandledDataset>))]
    public void Operator(Unhandled quantity1, Unhandled quantity2)
    {
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(quantity1, quantity2, quantity1 == quantity2, quantity1 != quantity2);
    }
}
