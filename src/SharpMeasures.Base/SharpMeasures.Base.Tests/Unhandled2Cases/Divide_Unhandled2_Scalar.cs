namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Divide_Unhandled2_Scalar
{
    private static Unhandled2 Target(Unhandled2 a, Scalar b) => Unhandled2.Divide(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidScalar))]
    public void MatchInstanceMethod(Unhandled2 vector, Scalar factor)
    {
        var expected = vector.DivideBy(factor);

        var actual = Target(vector, factor);

        Assert.Equal(expected, actual);
    }
}
