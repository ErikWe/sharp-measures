namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Plus
{
    private static Vector3 Target(Vector3 vector) => vector.Plus();

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void EqualOriginal(Vector3 vector)
    {
        var actual = Target(vector);

        Assert.Equal(vector, actual);
    }
}
