namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class Equals_Object
{
    private static bool Target(Unhandled3 vector, object? other) => vector.Equals(other);

    [Fact]
    public void Null_False()
    {
        var vector = Datasets.GetValidUnhandled3();
        object? other = null;

        var actual = Target(vector, other);

        Assert.False(actual);
    }

    [Fact]
    public void DifferentType_False()
    {
        var vector = Datasets.GetValidUnhandled3();
        var other = string.Empty;

        var actual = Target(vector, other);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled3))]
    public void SameType_MatchUnhandled2Equals(Unhandled3 vector, Unhandled3 other)
    {
        var expected = vector.Equals(other);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
