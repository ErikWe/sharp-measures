namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ScalarTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

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
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void Method(Scalar scalar1, Scalar scalar2)
    {
        Utility.QuantityTests.EqualityTests.Method_ShouldBeEqualIfEqualMagnitudeAndType(scalar1, scalar2);
    }

    [Fact]
    public void Operator_NullCases()
    {
        Scalar scalar = Scalar.Zero;
        Scalar? nullScalar1 = null;
        Scalar? nullScalar2 = null;

        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(scalar, nullScalar1, scalar == nullScalar1, scalar != nullScalar1);
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(nullScalar1, scalar, nullScalar1 == scalar, nullScalar1 != scalar);
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(nullScalar1, nullScalar2, nullScalar1 == nullScalar2, nullScalar1 != nullScalar2);
    }
    
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, ScalarDataset>))]
    public void Operator(Scalar scalar1, Scalar scalar2)
    {
        Utility.QuantityTests.EqualityTests.Operator_ShouldMatchMethodOrEqualIfBothNull(scalar1, scalar2, scalar1 == scalar2, scalar1 != scalar2);
    }
}
