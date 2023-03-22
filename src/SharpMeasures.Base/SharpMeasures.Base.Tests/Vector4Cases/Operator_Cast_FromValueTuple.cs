namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Operator_Cast_FromValueTuple
{
    private static Vector4 Target((Scalar, Scalar, Scalar, Scalar) components) => (Vector4)components;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchFromValueTuple(Scalar x, Scalar y, Scalar z, Scalar w)
    {
        var expected = Vector4.FromValueTuple((x, y, z, w));

        var actual = Target((x, y, z, w));

        Assert.Equal(expected, actual);
    }
}
