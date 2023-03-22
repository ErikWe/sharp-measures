namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Operator_Cast_FromDouble
{
    private static Scalar Target(double value) => (Scalar)value;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarDouble))]
    public void Valid_MatchFromDouble(double value)
    {
        var expected = Scalar.FromDouble(value);

        var actual = Target(value);

        Assert.Equal(expected, actual);
    }
}
