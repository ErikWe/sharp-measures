namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Operator_Plus
{
    private static Vector4 Target(Vector4 vector) => +vector;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchMethod(Vector4 vector)
    {
        var expected = vector.Plus();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
