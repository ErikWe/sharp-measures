namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class IsNaN
{
    private static bool Target(Vector4 vector) => vector.IsNaN;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void MatchAnyComponentIsNaN(Vector4 vector)
    {
        var expected = vector.X.IsNaN || vector.Y.IsNaN || vector.Z.IsNaN || vector.W.IsNaN;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
