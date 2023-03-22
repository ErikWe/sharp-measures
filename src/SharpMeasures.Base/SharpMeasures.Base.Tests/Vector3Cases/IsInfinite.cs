namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class IsInfinite
{
    private static bool Target(Vector3 vector) => vector.IsInfinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void MatchAnyComponentIsInfinite(Vector3 vector)
    {
        var expected = vector.X.IsInfinite || vector.Y.IsInfinite || vector.Z.IsInfinite;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
