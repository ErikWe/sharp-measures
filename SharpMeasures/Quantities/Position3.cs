namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Position3
{
    /// <summary>Computes final <see cref="Position3"/> according to { <paramref name="initialPosition"/> + <paramref name="displacement"/> }.</summary>
    public static Position3 From(Position3 initialPosition, Displacement3 displacement) => new(initialPosition.Components + displacement.Components);

    /// <summary>Computes final <see cref="Position3"/> according to { <see langword="this"/> + <paramref name="displacement"/> }.</summary>
    public Position3 Add(Displacement3 displacement) => Position3.From(this, displacement);
    /// <summary>Computes final <see cref="Position3"/> according to { <paramref name="initialPosition"/> + <paramref name="displacement"/> }.</summary>
    public static Position3 operator +(Position3 initialPosition, Displacement3 displacement) => initialPosition.Add(displacement);
}
