namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class AsIVector2Quantity_WithComponents_Scalar_Scalar
{
    private static Unhandled2 Target(Scalar x, Scalar y)
    {
        return execute<Unhandled2>();

        TVector execute<TVector>() where TVector : IVector2Quantity<TVector> => TVector.WithComponents(x, y);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchConstructor(Scalar x, Scalar y)
    {
        Unhandled2 expected = new(x, y);

        var actual = Target(x, y);

        Assert.Equal(expected, actual);
    }
}
