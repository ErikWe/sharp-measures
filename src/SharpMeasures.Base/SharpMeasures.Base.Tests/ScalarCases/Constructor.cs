namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Constructor
{
    private static Scalar Target(double value) => new(value);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarDouble))]
    public void Valid_MatchToDouble(double value)
    {
        var actual = Target(value).ToDouble();

        Assert.Equal(value, actual);
    }
}
