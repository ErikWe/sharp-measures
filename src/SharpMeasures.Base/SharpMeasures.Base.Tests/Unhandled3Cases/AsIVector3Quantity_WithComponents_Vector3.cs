namespace SharpMeasures.Tests.Unhandled3Cases;

using Xunit;

public class AsIVector3Quantity_WithComponents_Vector3
{
    private static Unhandled3 Target(Vector3 components)
    {
        return execute<Unhandled3>();

        TVector execute<TVector>() where TVector : IVector3Quantity<TVector> => TVector.WithComponents(components);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchConstructor(Scalar x, Scalar y, Scalar z)
    {
        Unhandled3 expected = new(x, y, z);

        var actual = Target((x, y, z));

        Assert.Equal(expected, actual);
    }
}
