namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Operator_Inequality
{
    private static bool Target(Unhandled lhs, Unhandled rhs) => lhs != rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled))]
    public void Valid_OppositeOfEqualsMethod(Unhandled lhs, Unhandled rhs)
    {
        var expected = lhs.Equals(rhs) is false;

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
