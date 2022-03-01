namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Torque3
{
    /// <summary>Computes <see cref="Torque3"/> according to { <paramref name="displacement"/> × <paramref name="force"/> },
    /// where <paramref name="force"/> is the <see cref="Force3"/> being applied at a <see cref="Displacement3"/> <paramref name="displacement"/>.</summary>
    public static Torque3 From(Displacement3 displacement, Force3 force) => new(Maths.Vectors.Cross(displacement, force));
}
