namespace SharpMeasures.Tests.Vector2Cases;

using Xunit;

public class X
{
    private static Scalar Target(Vector2 vector) => vector.X;

    [Theory]
    [ClassData(typeof(Datasets.ValidVector2))]
    public void NoException(Vector2 vector)
    {
        var exception = Record.Exception(() => Target(vector));

        Assert.Null(exception);
    }
}
