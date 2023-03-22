namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Equals_Vector4
{
    private static bool Target(Vector4 vector, Vector4 other) => vector.Equals(other);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4_ValidVector4))]
    public void Valid_MatchComponentsEquals(Vector4 vector, Vector4 other)
    {
        var expected = vector.X.Equals(other.X) && vector.Y.Equals(other.Y) && vector.Z.Equals(other.Z) && vector.W.Equals(other.W);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
