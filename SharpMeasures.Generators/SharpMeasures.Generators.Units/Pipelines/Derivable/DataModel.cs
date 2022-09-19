namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Documentation;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Unit { get; }
    public NamedType Quantity { get; }

    public IUnitPopulation UnitPopulation { get; }

    public IReadOnlyCollection<DerivableUnitDefinition> Derivations { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType unit, NamedType quantity, IUnitPopulation unitPopulation, IReadOnlyCollection<DerivableUnitDefinition> derivations, IDocumentationStrategy documentation)
    {
        Unit = unit;
        Quantity = quantity;

        UnitPopulation = unitPopulation;

        Derivations = derivations.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
