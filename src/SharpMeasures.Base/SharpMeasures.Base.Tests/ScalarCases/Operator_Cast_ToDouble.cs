namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Operator_Cast_ToDouble
{
    private static double Target(Scalar scalar) => (double)scalar;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar))]
    public void Valid_MatchToDouble(Scalar scalar)
    {
        var expected = scalar.ToDouble();

        var actual = Target(scalar);

        Assert.Equal(expected, actual);
    }
}
