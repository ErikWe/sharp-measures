namespace SharpMeasures.Generators.Quantities.Refinement;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public readonly record struct RefinedUnitListDefinition
{
    public static IBuilder StartBuilder() => new Builder();

    public ReadOnlyEquatableCollection<UnitInstance> UnitList { get; }

    public RefinedUnitListDefinition(IReadOnlyCollection<UnitInstance> unitList)
    {
        UnitList = unitList.AsReadOnlyEquatable();
    }

    public RefinedUnitListDefinition() : this(EquatableCollection<UnitInstance>.Empty) { }

    [SuppressMessage("Design", "CA1034", Justification = "Builder")]
    public interface IBuilder
    {
        public abstract void AddUnit(UnitInstance unit);
        public abstract RefinedUnitListDefinition Finalize();
    }

    private class Builder : IBuilder
    {
        private List<UnitInstance> UnitListBuilder { get; }

        public Builder()
        {
            UnitListBuilder = new();
        }

        public void AddUnit(UnitInstance unit) => UnitListBuilder.Add(unit);
        public RefinedUnitListDefinition Finalize() => new(UnitListBuilder);
    }
}
