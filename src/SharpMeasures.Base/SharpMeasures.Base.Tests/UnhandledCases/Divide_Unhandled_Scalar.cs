namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Divide_Unhandled_Scalar
{
    private static Unhandled Target(Unhandled x, Scalar y) => Unhandled.Divide(x, y);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidScalar))]
    public void MatchInstanceMethod(Unhandled x, Scalar y)
    {
        var expected = x.DivideBy(y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
