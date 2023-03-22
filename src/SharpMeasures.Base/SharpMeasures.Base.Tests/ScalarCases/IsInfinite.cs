namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class IsInfinite
{
    private static bool Target(Scalar scalar) => scalar.IsInfinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchDoubleIsInfinity(Scalar scalar)
    {
        var expected = double.IsInfinity(scalar);

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
