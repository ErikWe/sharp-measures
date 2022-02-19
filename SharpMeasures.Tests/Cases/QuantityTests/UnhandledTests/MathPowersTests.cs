namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.UnhandledTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class MathPowersTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<UnhandledDataset, DoubleDataset>))]
    public void Power(Unhandled quantity, double exponent)
    {
        Utility.QuantityTests.MathPowersTests.Power_ShouldMatchSystem(quantity, exponent, quantity.Power(exponent));
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void Invert(Unhandled quantity)
    {
        Utility.QuantityTests.MathPowersTests.Invert_ShouldMatchDefinition(quantity, quantity.Invert());
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void Square(Unhandled quantity)
    {
        Utility.QuantityTests.MathPowersTests.Square_ShouldMatchPower2(quantity, quantity.Square());
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void Cube(Unhandled quantity)
    {
        Utility.QuantityTests.MathPowersTests.Cube_ShouldMatchPower3(quantity, quantity.Cube());
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void SquareRoot(Unhandled quantity)
    {
        Utility.QuantityTests.MathPowersTests.SquareRoot_ShouldMatchSystem(quantity, quantity.SquareRoot());
    }

    [Theory]
    [ClassData(typeof(UnhandledDataset))]
    public void CubeRoot(Unhandled quantity)
    {
        Utility.QuantityTests.MathPowersTests.CubeRoot_ShouldMatchSystem(quantity, quantity.CubeRoot());
    }
}
