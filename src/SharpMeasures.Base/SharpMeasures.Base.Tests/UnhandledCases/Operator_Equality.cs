namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Operator_Equality
{
    private static bool Target(Unhandled lhs, Unhandled rhs) => lhs == rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled))]
    public void Valid_MatchEqualsMethod(Unhandled lhs, Unhandled rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
