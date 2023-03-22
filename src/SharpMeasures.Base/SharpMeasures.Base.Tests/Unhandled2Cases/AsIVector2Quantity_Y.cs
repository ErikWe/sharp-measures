namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class AsIVector2Quantity_Y
{
    private static Scalar Target(Unhandled2 vector)
    {
        return execute(vector);

        static Scalar execute(IVector2Quantity vector) => vector.Y;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchYMagnitude(Unhandled2 vector)
    {
        var expected = vector.Y.Magnitude;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
