namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class Equals_Object
{
    private static bool Target(Unhandled2 vector, object? other) => vector.Equals(other);

    [Fact]
    public void Null_False()
    {
        var vector = Datasets.GetValidUnhandled2();
        object? other = null;

        var actual = Target(vector, other);

        Assert.False(actual);
    }

    [Fact]
    public void DifferentType_False()
    {
        var vector = Datasets.GetValidUnhandled2();
        var other = string.Empty;

        var actual = Target(vector, other);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled2))]
    public void SameType_MatchUnhandled2Equals(Unhandled2 vector, Unhandled2 other)
    {
        var expected = vector.Equals(other);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
