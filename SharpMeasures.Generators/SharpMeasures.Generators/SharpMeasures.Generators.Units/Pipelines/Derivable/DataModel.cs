namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Documentation;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Unit { get; }
    public NamedType Quantity { get; }

    public IReadOnlyCollection<DerivableUnitDefinition> Derivations => derivations;

    public IDocumentationStrategy Documentation { get; }

    private ReadOnlyEquatableCollection<DerivableUnitDefinition> derivations { get; }

    public DataModel(DefinedType unit, NamedType quantity, IReadOnlyCollection<DerivableUnitDefinition> derivations, IDocumentationStrategy documentation)
    {
        Unit = unit;
        Quantity = quantity;

        this.derivations = derivations.AsReadOnlyEquatable();

        Documentation = documentation;
    }
}
