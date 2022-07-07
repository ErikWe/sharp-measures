namespace SharpMeasures.Generators.Quantities.Refinement.UnitList;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.UnitInstances;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public readonly record struct RefinedUnitListDefinition
{
    public static IBuilder StartBuilder() => new Builder();

    public IReadOnlyCollection<IUnitInstance> UnitList => unitList;
    private ReadOnlyEquatableCollection<IUnitInstance> unitList { get; }

    public RefinedUnitListDefinition(IReadOnlyCollection<IUnitInstance> unitList)
    {
        this.unitList = unitList.AsReadOnlyEquatable();
    }

    public RefinedUnitListDefinition() : this(EquatableCollection<IUnitInstance>.Empty) { }

    [SuppressMessage("Design", "CA1034", Justification = "Builder")]
    public interface IBuilder
    {
        public abstract void AddUnit(IUnitInstance unit);
        public abstract RefinedUnitListDefinition Finalize();
    }

    private class Builder : IBuilder
    {
        private List<IUnitInstance> UnitListBuilder { get; }

        public Builder()
        {
            UnitListBuilder = new();
        }

        public void AddUnit(IUnitInstance unit) => UnitListBuilder.Add(unit);
        public RefinedUnitListDefinition Finalize() => new(UnitListBuilder);
    }
}
