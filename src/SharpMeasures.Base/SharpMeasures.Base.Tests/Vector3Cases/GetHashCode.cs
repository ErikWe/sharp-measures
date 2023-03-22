namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class GetHashCode
{
    private static int Target(Vector3 vector) => vector.GetHashCode();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void Equal_Match(Vector3 vector)
    {
        Vector3 other = new(vector.X, vector.Y, vector.Z);

        var expected = Target(other);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
