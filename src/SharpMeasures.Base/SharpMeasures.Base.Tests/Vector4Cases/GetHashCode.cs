namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class GetHashCode
{
    private static int Target(Vector4 vector) => vector.GetHashCode();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void Equal_Match(Vector4 vector)
    {
        Vector4 other = new(vector.X, vector.Y, vector.Z, vector.W);

        var expected = Target(other);

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
