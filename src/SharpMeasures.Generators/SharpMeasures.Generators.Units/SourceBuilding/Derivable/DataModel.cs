namespace SharpMeasures.Generators.Units.SourceBuilding.Derivable;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Unit { get; }
    public NamedType Quantity { get; }

    public IUnitPopulation UnitPopulation { get; }

    public IReadOnlyCollection<DerivableUnitDefinition> Derivations { get; }

    public SourceBuildingContext SourceBuildingContext { get; }

    public DataModel(DefinedType unit, NamedType quantity, IUnitPopulation unitPopulation, IReadOnlyCollection<DerivableUnitDefinition> derivations, SourceBuildingContext sourceBuildingContext)
    {
        Unit = unit;
        Quantity = quantity;

        UnitPopulation = unitPopulation;

        Derivations = derivations.AsReadOnlyEquatable();

        SourceBuildingContext = sourceBuildingContext;
    }
}
