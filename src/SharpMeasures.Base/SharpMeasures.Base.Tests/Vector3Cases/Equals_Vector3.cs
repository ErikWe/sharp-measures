namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Equals_Vector3
{
    private static bool Target(Vector3 vector, Vector3 other) => vector.Equals(other);

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void Valid_MatchComponentsEquals(Vector3 vector, Vector3 other)
    {
        var expected = vector.X.Equals(other.X) && vector.Y.Equals(other.Y) && vector.Z.Equals(other.Z);

        var actual = Target(vector, other);

        Assert.Equal(expected, actual);
    }
}
