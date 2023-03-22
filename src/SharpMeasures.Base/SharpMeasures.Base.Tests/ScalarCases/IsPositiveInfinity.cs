namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class IsPositiveInfinity
{
    private static bool Target(Scalar scalar) => scalar.IsPositiveInfinity;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchDoubleIsPositiveInfinity(Scalar scalar)
    {
        var expected = double.IsPositiveInfinity(scalar);

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
