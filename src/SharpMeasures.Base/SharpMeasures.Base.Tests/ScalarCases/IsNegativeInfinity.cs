namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class IsNegativeInfinity
{
    private static bool Target(Scalar scalar) => scalar.IsNegativeInfinity;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchDoubleIsNegativeInfinity(Scalar scalar)
    {
        var expected = double.IsNegativeInfinity(scalar);

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
