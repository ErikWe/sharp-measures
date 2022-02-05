namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct MomentOfInertia
{
    /// <summary>Computes <see cref="MomentOfInertia"/> according to { <see cref="MomentOfInertia"/>
    /// = <paramref name="angularMomentum"/> / <paramref name="angularSpeed"/> }.</summary>
    public static MomentOfInertia From(AngularMomentum angularMomentum, AngularSpeed angularSpeed) => new(angularMomentum.Magnitude / angularSpeed.Magnitude);
}
