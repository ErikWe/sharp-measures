namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class IsNegative
{
    private static bool Target(Scalar scalar) => scalar.IsNegative;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchDoubleIsNegative(Scalar scalar)
    {
        var expected = double.IsNegative(scalar);

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
