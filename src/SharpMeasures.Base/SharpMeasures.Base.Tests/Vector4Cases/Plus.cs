namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Plus
{
    private static Vector4 Target(Vector4 vector) => vector.Plus();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void EqualOriginal(Vector4 vector)
    {
        var actual = Target(vector);

        Assert.Equal(vector, actual);
    }
}
