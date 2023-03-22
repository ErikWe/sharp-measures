namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Equals_Unhandled2_Unhandled2
{
    private static bool Target(Unhandled2 lhs, Unhandled2 rhs) => Unhandled2.Equals(lhs, rhs);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled2))]
    public void Valid_MatchInstanceMethod(Unhandled2 lhs, Unhandled2 rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
