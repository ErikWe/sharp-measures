namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Position3
{
    /// <summary>Computes final <see cref="Position3"/> according to { <paramref name="initialPosition"/> + <paramref name="displacement"/> }.</summary>
    public static OrbitalAngularVelocity3 From(Position3 initialPosition, Displacement3 displacement) => new(initialPosition.Components + displacement.Components);
}
