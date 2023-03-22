namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class AsIVector4Quantity_WithComponents_Vector4
{
    private static Vector4 Target(Vector4 components)
    {
        return execute<Vector4>();

        TVector execute<TVector>() where TVector : IVector4Quantity<TVector> => TVector.WithComponents(components);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchOriginal(Vector4 components)
    {
        var actual = Target(components);

        Assert.Equal(components, actual);
    }
}
