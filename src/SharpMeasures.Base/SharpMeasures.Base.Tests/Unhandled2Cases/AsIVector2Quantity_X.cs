namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class AsIVector2Quantity_X
{
    private static Scalar Target(Unhandled2 vector)
    {
        return execute(vector);

        static Scalar execute(IVector2Quantity vector) => vector.X;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchXMagnitude(Unhandled2 vector)
    {
        var expected = vector.X.Magnitude;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
