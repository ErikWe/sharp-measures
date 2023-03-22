namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class Equals_Unhandled4_Unhandled4
{
    private static bool Target(Unhandled4 lhs, Unhandled4 rhs) => Unhandled4.Equals(lhs, rhs);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled4))]
    public void Valid_MatchInstanceMethod(Unhandled4 lhs, Unhandled4 rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
