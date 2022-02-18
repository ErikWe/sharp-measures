namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ScalarTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;
using ErikWe.SharpMeasures.Tests.Datasets.Double;
using ErikWe.SharpMeasures.Tests.Datasets.Scalar;

using Xunit;

public class MathPowersTests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ScalarDataset, DoubleDataset>))]
    public void Power(Scalar scalar, double exponent)
    {
        Utility.QuantityTests.MathPowersTests.Power_ShouldMatchSystem(scalar, exponent, scalar.Power(exponent));
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Invert(Scalar scalar)
    {
        Utility.QuantityTests.MathPowersTests.Invert_ShouldMatchDefinition(scalar, scalar.Invert());
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Square(Scalar scalar)
    {
        Utility.QuantityTests.MathPowersTests.Square_ShouldMatchPower2(scalar, scalar.Square());
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void Cube(Scalar scalar)
    {
        Utility.QuantityTests.MathPowersTests.Cube_ShouldMatchPower3(scalar, scalar.Cube());
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void SquareRoot(Scalar scalar)
    {
        Utility.QuantityTests.MathPowersTests.SquareRoot_ShouldMatchSystem(scalar, scalar.SquareRoot());
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CubeRoot(Scalar scalar)
    {
        Utility.QuantityTests.MathPowersTests.CubeRoot_ShouldMatchSystem(scalar, scalar.CubeRoot());
    }
}
