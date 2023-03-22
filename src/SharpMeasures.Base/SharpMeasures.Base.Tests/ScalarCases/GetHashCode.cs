namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class GetHashCode
{
    private static int Target(Scalar scalar) => scalar.GetHashCode();

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void Equal_Match(Scalar scalar)
    {
        Scalar other = new(scalar.ToDouble());

        var expected = Target(other);

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
