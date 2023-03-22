namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Multiply_Unhandled4_Scalar
{
    private static Unhandled4 Target(Unhandled4 a, Scalar b) => Unhandled4.Multiply(a, b);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidScalar))]
    public void MatchInstanceMethod(Unhandled4 vector, Scalar factor)
    {
        var expected = vector.Multiply(factor);

        var actual = Target(vector, factor);

        Assert.Equal(expected, actual);
    }
}
