namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class AsIVector4Quantity_Components
{
    private static Vector4 Target(Vector4 vector)
    {
        return execute(vector);

        static Vector4 execute(IVector4Quantity vector) => vector.Components;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchOriginal(Vector4 vector)
    {
        var actual = Target(vector);

        Assert.Equal(vector, actual);
    }
}
