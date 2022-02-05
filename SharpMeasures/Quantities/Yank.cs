namespace ErikWe.SharpMeasures.Quantities;

public readonly partial record struct Yank
{
    /// <summary>Computes average <see cref="Yank"/> according to { <see cref="Yank"/> = <paramref name="force"/> / <paramref name="time"/> },
    /// where <paramref name="force"/> is the change in <see cref="Force"/> over some <see cref="Time"/> <paramref name="time"/>.</summary>
    public static Yank From(Force force, Time time) => new(force.Magnitude / time.Magnitude);
}
