namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Operator_Inequality
{
    private static bool Target(Unhandled4 lhs, Unhandled4 rhs) => lhs != rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled4))]
    public void Valid_OppositeOfEqualsMethod(Unhandled4 lhs, Unhandled4 rhs)
    {
        var expected = lhs.Equals(rhs) is false;

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
