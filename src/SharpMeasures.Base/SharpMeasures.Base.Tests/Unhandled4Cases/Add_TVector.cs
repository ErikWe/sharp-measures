namespace SharpMeasures.Tests.Unhandled4Cases;

using System;

using Xunit;

public class Add_TVector
{
    private static Unhandled4 Target<TVector>(Unhandled4 vector, TVector addend) where TVector : IVector4Quantity => vector.Add(addend);

    [Fact]
    public void Null_ArgumentNullException()
    {
        var vector = Datasets.GetValidUnhandled4();
        var addend = Datasets.GetNullVector4Quantity();

        AnyError_TException<ArgumentNullException, ReferenceVector4Quantity>(vector, addend);
    }

    [Theory]
    [ClassData(typeof(Datasets.ValidUnhandled4_ValidUnhandled4))]
    public void MatchComponentsAdd(Unhandled4 vector, Unhandled4 addend)
    {
        var expected = vector.Components.Add(addend.Components);

        var actual = Target(vector, addend).Components;

        Assert.Equal(expected, actual);
    }

    private static void AnyError_TException<TException, TVector>(Unhandled4 vector, TVector addend) where TException : Exception where TVector : IVector4Quantity
    {
        var exception = Record.Exception(() => Target(vector, addend));

        Assert.IsType<TException>(exception);
    }
}
