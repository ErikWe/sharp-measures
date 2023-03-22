namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Operator_Equality
{
    private static bool Target(Unhandled2 lhs, Unhandled2 rhs) => lhs == rhs;

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled2))]
    public void Valid_MatchEqualsMethod(Unhandled2 lhs, Unhandled2 rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
