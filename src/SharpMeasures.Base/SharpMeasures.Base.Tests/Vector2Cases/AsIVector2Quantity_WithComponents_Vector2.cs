namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class AsIVector2Quantity_WithComponents_Vector2
{
    private static Vector2 Target(Vector2 components)
    {
        return execute<Vector2>();

        TVector execute<TVector>() where TVector : IVector2Quantity<TVector> => TVector.WithComponents(components);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void MatchOriginal(Vector2 components)
    {
        var actual = Target(components);

        Assert.Equal(components, actual);
    }
}
