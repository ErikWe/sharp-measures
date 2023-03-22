namespace SharpMeasures.Tests.Unhandled4Cases;

using Xunit;

public class AsIVector4Quantity_WithComponents_Vector4
{
    private static Unhandled4 Target(Vector4 components)
    {
        return execute<Unhandled4>();

        TVector execute<TVector>() where TVector : IVector4Quantity<TVector> => TVector.WithComponents(components);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchConstructor(Scalar x, Scalar y, Scalar z, Scalar w)
    {
        Unhandled4 expected = new(x, y, z, w);

        var actual = Target((x, y, z, w));

        Assert.Equal(expected, actual);
    }
}
