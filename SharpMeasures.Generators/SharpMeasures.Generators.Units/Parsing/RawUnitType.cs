namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using System.Collections.Generic;

internal record class RawUnitType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public RawSharpMeasuresUnitDefinition Definition { get; }

    public IEnumerable<RawDerivableUnitDefinition> UnitDerivations => unitDerivations;

    public RawFixedUnitInstanceDefinition? FixedUnitInstance { get; }
    public IEnumerable<RawUnitInstanceAliasDefinition> UnitInstanceAliases => unitInstanceAliases;
    public IEnumerable<RawBiasedUnitInstanceDefinition> BiasedUnitInstances => biasedUnitInstances;
    public IEnumerable<RawDerivedUnitInstanceDefinition> DerivedUnitInstances => derivedUnitInstances;
    public IEnumerable<RawPrefixedUnitInstanceDefinition> PrefixedUnitInstances => prefixedUnitInstances;
    public IEnumerable<RawScaledUnitInstanceDefinition> ScaledUnitInstances => scaledUnitInstances;

    private EquatableEnumerable<RawDerivableUnitDefinition> unitDerivations { get; }

    private EquatableEnumerable<RawUnitInstanceAliasDefinition> unitInstanceAliases { get; }
    private EquatableEnumerable<RawDerivedUnitInstanceDefinition> derivedUnitInstances { get; }
    private EquatableEnumerable<RawBiasedUnitInstanceDefinition> biasedUnitInstances { get; }
    private EquatableEnumerable<RawPrefixedUnitInstanceDefinition> prefixedUnitInstances { get; }
    private EquatableEnumerable<RawScaledUnitInstanceDefinition> scaledUnitInstances { get; }

    public RawUnitType(DefinedType type, MinimalLocation unitLocation, RawSharpMeasuresUnitDefinition definition, IEnumerable<RawDerivableUnitDefinition> unitDerivations, RawFixedUnitInstanceDefinition? fixedUnitInstance,
        IEnumerable<RawUnitInstanceAliasDefinition> unitInstanceAliases, IEnumerable<RawDerivedUnitInstanceDefinition> derivedUnitInstances, IEnumerable<RawBiasedUnitInstanceDefinition> biasedUnitInstances,
        IEnumerable<RawPrefixedUnitInstanceDefinition> prefixedUnitInstances, IEnumerable<RawScaledUnitInstanceDefinition> scaledUnitInstances)
    {
        Type = type;
        TypeLocation = unitLocation;

        Definition = definition;

        this.unitDerivations = unitDerivations.AsEquatable();

        FixedUnitInstance = fixedUnitInstance;
        this.unitInstanceAliases = unitInstanceAliases.AsEquatable();
        this.derivedUnitInstances = derivedUnitInstances.AsEquatable();
        this.biasedUnitInstances = biasedUnitInstances.AsEquatable();
        this.prefixedUnitInstances = prefixedUnitInstances.AsEquatable();
        this.scaledUnitInstances = scaledUnitInstances.AsEquatable();
    }
}
