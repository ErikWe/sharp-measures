namespace SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Documentation;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.FixedUnit;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Unit { get; }
    public DefinedType Quantity { get; }

    public bool BiasTerm { get; }

    public FixedUnitDefinition? FixedUnit { get; }

    public IEnumerable<UnitAliasDefinition> UnitAliases => unitAliases;
    public IEnumerable<DerivedUnitDefinition> DerivedUnits => derivedUnits;
    public IEnumerable<BiasedUnitDefinition> BiasedUnits => biasedUnits;
    public IEnumerable<PrefixedUnitDefinition> PrefixedUnits => prefixedUnits;
    public IEnumerable<ScaledUnitDefinition> ScaledUnits => scaledUnits;

    public IDocumentationStrategy Documentation { get; }

    private EquatableEnumerable<UnitAliasDefinition> unitAliases { get; }
    private EquatableEnumerable<DerivedUnitDefinition> derivedUnits { get; }
    private EquatableEnumerable<BiasedUnitDefinition> biasedUnits { get; }
    private EquatableEnumerable<PrefixedUnitDefinition> prefixedUnits { get; }
    private EquatableEnumerable<ScaledUnitDefinition> scaledUnits { get; }

    public DataModel(DefinedType unit, DefinedType quantity, bool biasTerm, FixedUnitDefinition? fixedUnit,
        IEnumerable<UnitAliasDefinition> unitAliases, IEnumerable<DerivedUnitDefinition> derivedUnits, IEnumerable<BiasedUnitDefinition> biasedUnits,
        IEnumerable<PrefixedUnitDefinition> prefixedUnits, IEnumerable<ScaledUnitDefinition> scaledUnits, IDocumentationStrategy documentation)
    {
        Unit = unit;
        Quantity = quantity;

        BiasTerm = biasTerm;

        FixedUnit = fixedUnit;

        this.unitAliases = unitAliases.AsEquatable();
        this.derivedUnits = derivedUnits.AsEquatable();
        this.biasedUnits = biasedUnits.AsEquatable();
        this.prefixedUnits = prefixedUnits.AsEquatable();
        this.scaledUnits = scaledUnits.AsEquatable();

        Documentation = documentation;
    }
}
