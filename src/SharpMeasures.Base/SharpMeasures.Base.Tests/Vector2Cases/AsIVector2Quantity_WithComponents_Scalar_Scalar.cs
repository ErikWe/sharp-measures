namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class AsIVector2Quantity_WithComponents_Scalar_Scalar
{
    private static Vector2 Target(Scalar x, Scalar y)
    {
        return execute<Vector2>();

        TVector execute<TVector>() where TVector : IVector2Quantity<TVector> => TVector.WithComponents(x, y);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchConstructor(Scalar x, Scalar y)
    {
        Vector2 expected = new(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
