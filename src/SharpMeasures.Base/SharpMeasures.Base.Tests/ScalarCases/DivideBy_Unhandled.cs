namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class DivideBy_Unhandled
{
    private static Unhandled Target(Scalar scalar, Unhandled divisor) => scalar.DivideBy(divisor);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidUnhandled))]
    public void MatchToDoubleDivision(Scalar scalar, Unhandled divisor)
    {
        var expected = scalar.ToDouble() / divisor.Magnitude.ToDouble();

        var actual = Target(scalar, divisor).Magnitude.ToDouble();

        Assert.Equal(expected, actual);
    }
}
