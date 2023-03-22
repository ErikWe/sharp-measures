namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class FromValueTuple
{
    private static Vector4 Target((Scalar, Scalar, Scalar, Scalar) components) => Vector4.FromValueTuple(components);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchConstructor(Scalar x, Scalar y, Scalar z, Scalar w)
    {
        Vector4 expected = new(x, y, z, w);

        var actual = Target((x, y, z, w));

        Assert.Equal(expected, actual);
    }
}
