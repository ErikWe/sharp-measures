namespace SharpMeasures.Tests.Unhandled3Cases;

using System;

using Xunit;

public class Add_TVector
{
    private static Unhandled3 Target<TVector>(Unhandled3 vector, TVector addend) where TVector : IVector3Quantity => vector.Add(addend);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled3();
        var addend = Datasets.GetNullVector3Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector3Quantity>(vector, addend);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled3_ValidUnhandled3))]
    public void MatchComponentsAdd(Unhandled3 vector, Unhandled3 addend)
    {
        var expected = vector.Components.Add(addend.Components);

        var actual = Target(vector, addend).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled3 vector, TVector addend) where TException : Exception where TVector : IVector3Quantity
    {
        var exception = Record.Exception(() => Target(vector, addend));

        Assert.IsType<TException>(exception);
    }
}
