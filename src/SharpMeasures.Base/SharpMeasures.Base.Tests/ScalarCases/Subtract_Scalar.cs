namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Subtract_Scalar
{
    private static Scalar Target(Scalar scalar, Scalar subtrahend) => scalar.Subtract(subtrahend);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void MatchToDoubleSubtraction(Scalar scalar, Scalar subtrahend)
    {
        var expected = scalar.ToDouble() - subtrahend.ToDouble();

        var actual = Target(scalar, subtrahend).ToDouble();

        Assert.Equal(expected, actual);
    }
}
