namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class Operator_Cast_FromValueTuple
{
    private static Vector2 Target((Scalar, Scalar) components) => (Vector2)components;

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchFromValueTuple(Scalar x, Scalar y)
    {
        var expected = Vector2.FromValueTuple((x, y));

        var actual = Target((x, y));

        Assert.Equal(expected, actual);
    }
}
