namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Operator_LessThan
{
    private static bool Target(Scalar lhs, Scalar rhs) => lhs < rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void Valid_MatchToDoubleLessThan(Scalar lhs, Scalar rhs)
    {
        var expected = lhs.ToDouble() < rhs.ToDouble();

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
