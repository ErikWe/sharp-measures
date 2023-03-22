namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Equals_Unhandled3_Unhandled3
{
    private static bool Target(Unhandled3 lhs, Unhandled3 rhs) => Unhandled3.Equals(lhs, rhs);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled3))]
    public void Valid_MatchInstanceMethod(Unhandled3 lhs, Unhandled3 rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
