namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class IsFinite
{
    private static bool Target(Vector3 vector) => vector.IsFinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchAllComponentsAreFinite(Vector3 vector)
    {
        var expected = vector.X.IsFinite && vector.Y.IsFinite && vector.Z.IsFinite;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
