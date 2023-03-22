namespace SharpMeasures.Tests.Vector4Cases;

using Xunit;

public class Z
{
    private static Scalar Target(Vector4 vector) => vector.Z;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector4))]
    public void NoException(Vector4 vector)
    {
        var exception = Record.Exception(() => Target(vector));

        Assert.Null(exception);
    }
}
