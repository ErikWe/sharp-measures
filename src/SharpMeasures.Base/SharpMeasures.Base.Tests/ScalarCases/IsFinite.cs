namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class IsFinite
{
    private static bool Target(Scalar scalar) => scalar.IsFinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchDoubleIsFinite(Scalar scalar)
    {
        var expected = double.IsFinite(scalar);

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
