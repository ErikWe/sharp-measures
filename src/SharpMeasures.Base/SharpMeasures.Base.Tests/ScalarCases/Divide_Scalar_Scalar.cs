namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Divide_Scalar_Scalar
{
    private static Scalar Target(Scalar x, Scalar y) => Scalar.Divide(x, y);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void MatchInstanceMethod(Scalar x, Scalar y)
    {
        var expected = x.DivideBy(y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
