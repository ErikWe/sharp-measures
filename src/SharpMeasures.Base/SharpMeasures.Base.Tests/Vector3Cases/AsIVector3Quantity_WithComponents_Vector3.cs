namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class AsIVector3Quantity_WithComponents_Vector3
{
    private static Vector3 Target(Vector3 components)
    {
        return execute<Vector3>();

        TVector execute<TVector>() where TVector : IVector3Quantity<TVector> => TVector.WithComponents(components);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchOriginal(Vector3 components)
    {
        var actual = Target(components);

        Assert.Equal(components, actual);
    }
}
