namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class AsIVector2Quantity_WithComponents_Vector2
{
    private static Unhandled2 Target(Vector2 components)
    {
        return execute<Unhandled2>();

        TVector execute<TVector>() where TVector : IVector2Quantity<TVector> => TVector.WithComponents(components);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchConstructor(Scalar x, Scalar y)
    {
        Unhandled2 expected = new(x, y);

        var actual = Target((x, y));

        Assert.Equal(expected, actual);
    }
}
