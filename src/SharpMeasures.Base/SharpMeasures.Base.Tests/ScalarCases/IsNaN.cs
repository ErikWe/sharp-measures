namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class IsNaN
{
    private static bool Target(Scalar scalar) => scalar.IsNaN;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchDoubleIsNaN(Scalar scalar)
    {
        var expected = double.IsNaN(scalar);

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
