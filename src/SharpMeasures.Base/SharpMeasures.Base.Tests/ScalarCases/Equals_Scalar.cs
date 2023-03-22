namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Equals_Scalar
{
    private static bool Target(Scalar scalar, Scalar other) => scalar.Equals(other);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void Valid_MatchToDoubleEquals(Scalar scalar, Scalar other)
    {
        var expected = scalar.ToDouble().Equals(other.ToDouble());

        var actual = Target(scalar, other);

        Assert.Equal(expected, actual);
    }
}
