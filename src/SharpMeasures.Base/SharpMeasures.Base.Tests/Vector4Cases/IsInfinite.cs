namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class IsInfinite
{
    private static bool Target(Vector4 vector) => vector.IsInfinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchAnyComponentIsInfinite(Vector4 vector)
    {
        var expected = vector.X.IsInfinite || vector.Y.IsInfinite || vector.Z.IsInfinite || vector.W.IsInfinite;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
