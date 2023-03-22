namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Equals_Unhandled_Unhandled
{
    private static bool Target(Unhandled lhs, Unhandled rhs) => Unhandled.Equals(lhs, rhs);

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled))]
    public void Valid_MatchInstanceMethod(Unhandled lhs, Unhandled rhs)
    {
        var expected = lhs.Equals(rhs);

        var actual = Target(lhs, rhs);

        Assert.Equal(expected, actual);
    }
}
