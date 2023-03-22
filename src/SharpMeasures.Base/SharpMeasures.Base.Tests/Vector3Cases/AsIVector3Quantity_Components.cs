namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class AsIVector3Quantity_Components
{
    private static Vector3 Target(Vector3 vector)
    {
        return execute(vector);

        static Vector3 execute(IVector3Quantity vector) => vector.Components;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchOriginal(Vector3 vector)
    {
        var actual = Target(vector);

        Assert.Equal(vector, actual);
    }
}
