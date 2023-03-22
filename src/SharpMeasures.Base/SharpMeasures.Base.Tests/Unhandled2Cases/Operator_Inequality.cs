namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Operator_Inequality
{
    private static bool Target(Unhandled2 lhs, Unhandled2 rhs) => lhs != rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled2))]
    public void Valid_OppositeOfEqualsMethod(Unhandled2 lhs, Unhandled2 rhs)
    {
        var expected = lhs.Equals(rhs) is false;

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
