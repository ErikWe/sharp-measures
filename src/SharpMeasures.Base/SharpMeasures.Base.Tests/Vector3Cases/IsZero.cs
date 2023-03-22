namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class IsZero
{
    private static bool Target(Vector3 vector) => vector.IsZero;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchBothComponentsAreZero(Vector3 vector)
    {
        var expected = vector.X.IsZero && vector.Y.IsZero && vector.Z.IsZero;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
