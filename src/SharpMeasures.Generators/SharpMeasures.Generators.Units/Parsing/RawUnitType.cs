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

internal sealed record class RawUnitType
{
    public DefinedType Type { get; }

    public RawSharpMeasuresUnitDefinition Definition { get; }

    public IEnumerable<RawDerivableUnitDefinition> UnitDerivations { get; }

    public RawFixedUnitInstanceDefinition? FixedUnitInstance { get; }
    public IEnumerable<RawUnitInstanceAliasDefinition> UnitInstanceAliases { get; }
    public IEnumerable<RawBiasedUnitInstanceDefinition> BiasedUnitInstances { get; }
    public IEnumerable<RawDerivedUnitInstanceDefinition> DerivedUnitInstances { get; }
    public IEnumerable<RawPrefixedUnitInstanceDefinition> PrefixedUnitInstances { get; }
    public IEnumerable<RawScaledUnitInstanceDefinition> ScaledUnitInstances { get; }

    public RawUnitType(DefinedType type, RawSharpMeasuresUnitDefinition definition, IEnumerable<RawDerivableUnitDefinition> unitDerivations, RawFixedUnitInstanceDefinition? fixedUnitInstance,
        IEnumerable<RawUnitInstanceAliasDefinition> unitInstanceAliases, IEnumerable<RawDerivedUnitInstanceDefinition> derivedUnitInstances, IEnumerable<RawBiasedUnitInstanceDefinition> biasedUnitInstances,
        IEnumerable<RawPrefixedUnitInstanceDefinition> prefixedUnitInstances, IEnumerable<RawScaledUnitInstanceDefinition> scaledUnitInstances)
    {
        Type = type;

        Definition = definition;

        UnitDerivations = unitDerivations.AsEquatable();

        FixedUnitInstance = fixedUnitInstance;
        UnitInstanceAliases = unitInstanceAliases.AsEquatable();
        DerivedUnitInstances = derivedUnitInstances.AsEquatable();
        BiasedUnitInstances = biasedUnitInstances.AsEquatable();
        PrefixedUnitInstances = prefixedUnitInstances.AsEquatable();
        ScaledUnitInstances = scaledUnitInstances.AsEquatable();
    }
}
