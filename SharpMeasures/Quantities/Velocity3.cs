namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Velocity3
{
    /// <summary>Computes average <see cref="Velocity3"/> according to { <paramref name="displacement"/> / <paramref name="time"/> },
    /// where <paramref name="displacement"/> is the <see cref="Displacement3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Velocity3 From(Displacement3 displacement, Time time) => new(displacement.Components / time.Magnitude);
}
