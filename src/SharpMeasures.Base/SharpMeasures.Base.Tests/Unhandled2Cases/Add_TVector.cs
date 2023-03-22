namespace SharpMeasures.Tests.Unhandled2Cases;

using System;

using Xunit;

public class Add_TVector
{
    private static Unhandled2 Target<TVector>(Unhandled2 vector, TVector addend) where TVector : IVector2Quantity => vector.Add(addend);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled2();
        var addend = Datasets.GetNullVector2Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector2Quantity>(vector, addend);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled2_ValidUnhandled2))]
    public void MatchComponentsAdd(Unhandled2 vector, Unhandled2 addend)
    {
        var expected = vector.Components.Add(addend.Components);

        var actual = Target(vector, addend).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled2 vector, TVector addend) where TException : Exception where TVector : IVector2Quantity
    {
        var exception = Record.Exception(() => Target(vector, addend));

        Assert.IsType<TException>(exception);
    }
}
