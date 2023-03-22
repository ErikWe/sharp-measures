namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Equals_Object
{
    private static bool Target(Vector4 vector, object? other) => vector.Equals(other);

    [Fact]
    public void Null_False()
    {
        var vector = Datasets.GetValidVector4();
        object? other = null;

        var actual = Target(vector, other);

        Assert.False(actual);
    }

    [Fact]
    public void DifferentType_False()
    {
        var vector = Datasets.GetValidVector4();
        var other = string.Empty;

        var actual = Target(vector, other);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void SameType_MatchVector2Equals(Vector4 vector, Vector4 other)
    {
        var expected = vector.Equals(other);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
