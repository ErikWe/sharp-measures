namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Plus
{
    private static Scalar Target(Scalar scalar) => scalar.Plus();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void EqualOriginal(Scalar scalar)
    {
        var actual = Target(scalar);

        Assert.Equal(scalar, actual);
    }
}
