namespace SharpMeasures.Tests.Unhandled2Cases;

using Xunit;

public class AsIVector2Quantity_Components
{
    private static Vector2 Target(Unhandled2 vector)
    {
        return execute(vector);

        static Vector2 execute(IVector2Quantity vector) => vector.Components;
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2))]
    public void MatchComponents(Unhandled2 vector)
    {
        var expected = vector.Components;

        var actual = Target(vector);

        Assert.Equal(expected, actual);
    }
}
