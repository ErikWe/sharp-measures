namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class IsZero
{
    private static bool Target(Vector4 vector) => vector.IsZero;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchAllComponentsAreZero(Vector4 vector)
    {
        var expected = vector.X.IsZero && vector.Y.IsZero && vector.Z.IsZero && vector.W.IsZero;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
