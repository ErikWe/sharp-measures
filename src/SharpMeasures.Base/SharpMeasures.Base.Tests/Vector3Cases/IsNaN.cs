namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class IsNaN
{
    private static bool Target(Vector3 vector) => vector.IsNaN;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchAnyComponentIsNaN(Vector3 vector)
    {
        var expected = vector.X.IsNaN || vector.Y.IsNaN || vector.Z.IsNaN;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
