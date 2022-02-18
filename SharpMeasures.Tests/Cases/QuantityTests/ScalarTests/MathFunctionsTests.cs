namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ScalarTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;

using Xunit;

public class MathFunctionsTests
{
    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Absolute(Scalar scalar)
    {
        Utility.QuantityTests.MathFunctionsTests.Absolute_ShouldMatchSystem(scalar, scalar.Absolute());
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Floor(Scalar scalar)
    {
        Utility.QuantityTests.MathFunctionsTests.Floor_ShouldMatchSystem(scalar, scalar.Floor());
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Ceiling(Scalar scalar)
    {
        Utility.QuantityTests.MathFunctionsTests.Ceiling_ShouldMatchSystem(scalar, scalar.Ceiling());
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Round(Scalar scalar)
    {
        Utility.QuantityTests.MathFunctionsTests.Round_ShouldMatchSystem(scalar, scalar.Round());
    }
}
