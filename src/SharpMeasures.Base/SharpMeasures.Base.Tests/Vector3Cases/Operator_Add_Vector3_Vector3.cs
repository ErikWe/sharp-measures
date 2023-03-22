namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Operator_Add_Vector3_Vector3
{
    private static Vector3 Target(Vector3 a, Vector3 b) => a + b;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3_ValidVector3))]
    public void MatchMethod(Vector3 a, Vector3 b)
    {
        var expected = Vector3.Add(a, b);

        var actual = Target(a, b);

        Assert.Equal(expected, actual);
    }
}
