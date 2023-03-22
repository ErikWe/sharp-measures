namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Operator_Cast_FromValueTuple
{
    private static Vector3 Target((Scalar, Scalar, Scalar) components) => (Vector3)components;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchFromValueTuple(Scalar x, Scalar y, Scalar z)
    {
        var expected = Vector3.FromValueTuple((x, y, z));

        var actual = Target((x, y, z));

        Assert.Equal(expected, actual);
    }
}
