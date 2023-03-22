namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Operator_Equality
{
    private static bool Target(Unhandled3 lhs, Unhandled3 rhs) => lhs == rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled3))]
    public void Valid_MatchEqualsMethod(Unhandled3 lhs, Unhandled3 rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
