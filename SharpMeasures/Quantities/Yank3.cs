namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Yank3
{
    /// <summary>Computes average <see cref="Yank3"/> according to { <paramref name="force"/> / <paramref name="time"/> },
    /// where <paramref name="force"/> is the change in <see cref="Force3"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Yank3 From(Force3 force, Time time) => new(force.Components / time.Magnitude);
}
