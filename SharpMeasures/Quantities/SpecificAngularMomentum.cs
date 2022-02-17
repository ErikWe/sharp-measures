namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct SpecificAngularMomentum
{
    /// <summary>Computes <see cref="SpecificAngularMomentum"/> according to { <paramref name="angularMomentum"/> / <paramref name="mass"/> }.</summary>
    public static SpecificAngularMomentum From(AngularMomentum angularMomentum, Mass mass) => new(angularMomentum.Magnitude / mass.Magnitude);
}
