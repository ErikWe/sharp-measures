namespace SharpMeasures.Tests.ScalarCases;

using Xunit;

public class Operator_Inequality
{
    private static bool Target(Scalar lhs, Scalar rhs) => lhs != rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalar_ValidScalar))]
    public void Valid_OppositeOfEqualsMethod(Scalar lhs, Scalar rhs)
    {
        var expected = lhs.Equals(rhs) is false;

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
