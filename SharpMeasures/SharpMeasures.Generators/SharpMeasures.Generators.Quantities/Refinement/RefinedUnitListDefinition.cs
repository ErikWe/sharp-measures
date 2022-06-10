namespace SharpMeasures.Generators.Quantities.Refinement;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public readonly record struct RefinedUnitListDefinition
{
    public static IBuilder StartBuilder() => new Builder(new RefinedUnitListDefinition());

    public ReadOnlyEquatableCollection<UnitInstance> UnitList { get; }

    private EquatableCollection<UnitInstance> UnitListBuilder { get; }

    public RefinedUnitListDefinition(ICollection<UnitInstance> unitList)
    {
        UnitListBuilder = new(unitList);

        UnitList = new(UnitListBuilder);
    }

    public RefinedUnitListDefinition() : this(EquatableCollection<UnitInstance>.Empty) { }

    [SuppressMessage("Design", "CA1034", Justification = "Builder")]
    public interface IBuilder
    {
        public abstract RefinedUnitListDefinition Target { get; }

        public abstract void AddUnit(UnitInstance unit);
    }

    private class Builder : IBuilder
    {
        public RefinedUnitListDefinition Target { get; }

        public Builder(RefinedUnitListDefinition target)
        {
            Target = target;
        }

        public void AddUnit(UnitInstance unit) => Target.UnitListBuilder.Add(unit);
    }
}
