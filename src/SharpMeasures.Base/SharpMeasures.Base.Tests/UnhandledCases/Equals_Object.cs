namespace SharpMeasures.Tests.UnhandledCases;

using Xunit;

public class Equals_Object
{
    private static bool Target(Unhandled unhandled, object? other) => unhandled.Equals(other);

    [Fact]
    public void Null_False()
    {
        var unhandled = Datasets.GetValidUnhandled();
        object? other = null;

        var actual = Target(unhandled, other);

        Assert.False(actual);
    }

    [Fact]
    public void DifferentType_False()
    {
        var unhandled = Datasets.GetValidUnhandled();
        var other = string.Empty;

        var actual = Target(unhandled, other);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled_ValidUnhandled))]
    public void SameType_MatchUnhandledEquals(Unhandled unhandled, Unhandled other)
    {
        var expected = unhandled.Equals(other);

        var actual = Target(unhandled, other);

        Assert.Equal(expected, actual);
    }
}
