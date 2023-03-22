namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class FromDouble
{
    private static Scalar Target(double value) => Scalar.FromDouble(value);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarDouble))]
    public void Valid_MatchConstructor(double value)
    {
        Scalar expected = new(value);

        var actual = Target(value);

        Assert.Equal(expected, actual);
    }
}
