namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class FromValueTuple
{
    private static Vector2 Target((Scalar, Scalar) components) => Vector2.FromValueTuple(components);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchConstructor(Scalar x, Scalar y)
    {
        Vector2 expected = new(x, y);

        var actual = Target((x, y));

        Assert.Equal(expected, actual);
    }
}
