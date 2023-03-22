namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Equals_Object
{
    private static bool Target(Vector2 vector, object? other) => vector.Equals(other);

    [Fact]
    public void Null_False()
    {
        var vector = Datasets.GetValidVector2();
        object? other = null;

        var actual = Target(vector, other);

        Assert.False(actual);
    }

    [Fact]
    public void DifferentType_False()
    {
        var vector = Datasets.GetValidVector2();
        var other = string.Empty;

        var actual = Target(vector, other);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2_ValidVector2))]
    public void SameType_MatchVector2Equals(Vector2 vector, Vector2 other)
    {
        var expected = vector.Equals(other);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
