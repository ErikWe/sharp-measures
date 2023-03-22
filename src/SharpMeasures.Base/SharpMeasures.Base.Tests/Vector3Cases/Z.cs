namespace SharpMeasures.Tests.Vector3Cases;

using Xunit;

public class Z
{
    private static Scalar Target(Vector3 vector) => vector.Z;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector3))]
    public void NoException(Vector3 vector)
    {
        var exception = Record.Exception(() => Target(vector));

        Assert.Null(exception);
    }
}
