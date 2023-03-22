namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Operator_Plus
{
    private static Vector3 Target(Vector3 vector) => +vector;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchMethod(Vector3 vector)
    {
        var expected = vector.Plus();

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
