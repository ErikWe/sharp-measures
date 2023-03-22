namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Add_Scalar_Scalar
{
    private static Scalar Target(Scalar x, Scalar y) => Scalar.Add(x, y);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void MatchInstanceMethod(Scalar x, Scalar y)
    {
        var expected = x.Add(y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
