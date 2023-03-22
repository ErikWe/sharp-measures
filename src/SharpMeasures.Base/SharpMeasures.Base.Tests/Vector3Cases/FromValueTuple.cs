namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class FromValueTuple
{
    private static Vector3 Target((Scalar, Scalar, Scalar) components) => Vector3.FromValueTuple(components);

    [Theory]
    [ClassData(typeof(Datasets.ValidScalarTuple))]
    public void MatchConstructor(Scalar x, Scalar y, Scalar z)
    {
        Vector3 expected = new(x, y, z);

        var actual = Target((x, y, z));

        Assert.Equal(expected, actual);
    }
}
