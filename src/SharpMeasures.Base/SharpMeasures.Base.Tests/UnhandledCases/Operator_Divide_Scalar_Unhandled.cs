namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Operator_Divide_Scalar_Unhandled
{
    private static Unhandled Target(Scalar x, Unhandled y) => x / y;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchMethod(Unhandled y, Scalar x)
    {
        var expected = Scalar.Divide(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
