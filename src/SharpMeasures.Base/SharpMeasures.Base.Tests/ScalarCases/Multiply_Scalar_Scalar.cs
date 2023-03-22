namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Multiply_Scalar_Scalar
{
    private static Scalar Target(Scalar x, Scalar y) => Scalar.Multiply(x, y);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void MatchInstanceMethod(Scalar x, Scalar y)
    {
        var expected = x.Multiply(y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
