namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Divide_Unhandled3_Scalar
{
    private static Unhandled3 Target(Unhandled3 a, Scalar b) => Unhandled3.Divide(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidScalar))]
    public void MatchInstanceMethod(Unhandled3 vector, Scalar factor)
    {
        var expected = vector.DivideBy(factor);

        var actual = Target(vector, factor);

        Assert.Equal(expected, actual);
    }
}
