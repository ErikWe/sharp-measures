namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct SpecificAngularMomentum3
{
    /// <summary>Computes <see cref="SpecificAngularMomentum3"/> according to { <paramref name="angularMomentum"/> / <paramref name="mass"/> }.</summary>
    public static SpecificAngularMomentum3 From(AngularMomentum3 angularMomentum, Mass mass) => new(angularMomentum.Components / mass.Magnitude);
}
