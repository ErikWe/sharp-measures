namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class IsFinite
{
    private static bool Target(Vector4 vector) => vector.IsFinite;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchAllComponentsAreFinite(Vector4 vector)
    {
        var expected = vector.X.IsFinite && vector.Y.IsFinite && vector.Z.IsFinite && vector.W.IsFinite;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
