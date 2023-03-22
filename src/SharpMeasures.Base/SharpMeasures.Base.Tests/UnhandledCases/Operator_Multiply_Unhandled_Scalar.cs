namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Operator_Multiply_Unhandled_Scalar
{
    private static Unhandled Target(Unhandled x, Scalar y) => x * y;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchMethod(Unhandled x, Scalar y)
    {
        var expected = Unhandled.Multiply(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
