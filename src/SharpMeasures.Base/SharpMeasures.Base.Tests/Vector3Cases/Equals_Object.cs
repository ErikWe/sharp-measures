namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Equals_Object
{
    private static bool Target(Vector3 vector, object? other) => vector.Equals(other);

    [Fact]
    public void Null_False()
    {
        var vector = Datasets.GetValidVector3();
        object? other = null;

        var actual = Target(vector, other);

        Assert.False(actual);
    }

    [Fact]
    public void DifferentType_False()
    {
        var vector = Datasets.GetValidVector3();
        var other = string.Empty;

        var actual = Target(vector, other);

        Assert.False(actual);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void SameType_MatchVector2Equals(Vector3 vector, Vector3 other)
    {
        var expected = vector.Equals(other);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
