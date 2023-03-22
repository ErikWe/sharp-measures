namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class IsPositive
{
    private static bool Target(Scalar scalar) => scalar.IsPositive;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchDoubleIsPositive(Scalar scalar)
    {
        var expected = double.IsPositive(scalar);

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
