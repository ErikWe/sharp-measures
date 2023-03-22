namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Operator_GreaterThanOrEqual
{
    private static bool Target(Scalar lhs, Scalar rhs) => lhs >= rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void Valid_MatchToDoubleGreaterThanOrEqual(Scalar lhs, Scalar rhs)
    {
        var expected = lhs.ToDouble() >= rhs.ToDouble();

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
