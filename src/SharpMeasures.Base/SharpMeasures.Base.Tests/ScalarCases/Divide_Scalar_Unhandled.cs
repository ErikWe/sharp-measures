namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Divide_Scalar_Unhandled
{
    private static Unhandled Target(Scalar x, Unhandled y) => Scalar.Divide(x, y);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidUnhandled))]
    public void MatchInstanceMethod(Scalar x, Unhandled y)
    {
        var expected = x.DivideBy(y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
