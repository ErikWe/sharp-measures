namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class IsZero
{
    private static bool Target(Scalar scalar) => scalar.IsZero;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void MatchToDoubleIsZero(Scalar scalar)
    {
        var expected = scalar.ToDouble() is 0;

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
