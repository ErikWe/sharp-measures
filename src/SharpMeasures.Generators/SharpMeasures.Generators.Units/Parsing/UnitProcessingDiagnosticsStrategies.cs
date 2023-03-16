namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Processing;
using SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

internal interface IUnitProcessingDiagnosticsStrategy
{
    public abstract ISharpMeasuresUnitProcessingDiagnostics SharpMeasuresUnitDiagnostics { get; }

    public abstract IFixedUnitInstanceProcessingDiagnostics FixedUnitInstanceDiagnostics { get; }
    public abstract IDerivableUnitProcessingDiagnostics DerivableUnitDiagnostics { get; }

    public abstract IUnitInstanceAliasProcessingDiagnostics UnitInstanceAliasDiagnostics { get; }
    public abstract IDerivedUnitInstanceProcessingDiagnostics DerivedUnitInstanceDiagnostics { get; }
    public abstract IBiasedUnitInstanceProcessingDiagnostics BiasedUnitInstanceDiagnostics { get; }
    public abstract IPrefixedUnitInstanceProcessingDiagnostics PrefixedUnitInstanceDiagnostics { get; }
    public abstract IScaledUnitInstanceProcessingDiagnostics ScaledUnitInstanceDiagnostics { get; }
}

internal static class UnitProcessingDiagnosticsStrategies
{
    public static IUnitProcessingDiagnosticsStrategy Default { get; } = new DefaultProcessingDiagnosticsStrategy();
    public static IUnitProcessingDiagnosticsStrategy EmptyDiagnostics { get; } = new EmptyProcessingDiagnosticsStrategy();

    private sealed class DefaultProcessingDiagnosticsStrategy : IUnitProcessingDiagnosticsStrategy
    {
        public ISharpMeasuresUnitProcessingDiagnostics SharpMeasuresUnitDiagnostics => SharpMeasuresUnitProcessingDiagnostics.Instance;

        public IFixedUnitInstanceProcessingDiagnostics FixedUnitInstanceDiagnostics => FixedUnitInstanceProcessingDiagnostics.Instance;
        public IDerivableUnitProcessingDiagnostics DerivableUnitDiagnostics => DerivableUnitProcessingDiagnostics.Instance;

        public IUnitInstanceAliasProcessingDiagnostics UnitInstanceAliasDiagnostics => UnitInstanceAliasProcessingDiagnostics.Instance;
        public IDerivedUnitInstanceProcessingDiagnostics DerivedUnitInstanceDiagnostics => DerivedUnitInstanceProcessingDiagnostics.Instance;
        public IBiasedUnitInstanceProcessingDiagnostics BiasedUnitInstanceDiagnostics => BiasedUnitInstanceProcessingDiagnostics.Instance;
        public IPrefixedUnitInstanceProcessingDiagnostics PrefixedUnitInstanceDiagnostics => PrefixedUnitInstanceProcessingDiagnostics.Instance;
        public IScaledUnitInstanceProcessingDiagnostics ScaledUnitInstanceDiagnostics => ScaledUnitInstanceProcessingDiagnostics.Instance;
    }

    private sealed class EmptyProcessingDiagnosticsStrategy : IUnitProcessingDiagnosticsStrategy
    {
        public ISharpMeasuresUnitProcessingDiagnostics SharpMeasuresUnitDiagnostics => EmptySharpMeasuresUnitProcessingDiagnostics.Instance;

        public IFixedUnitInstanceProcessingDiagnostics FixedUnitInstanceDiagnostics => EmptyFixedUnitInstanceProcessingDiagnostics.Instance;
        public IDerivableUnitProcessingDiagnostics DerivableUnitDiagnostics => EmptyDerivableUnitProcessingDiagnostics.Instance;

        public IUnitInstanceAliasProcessingDiagnostics UnitInstanceAliasDiagnostics => EmptyUnitInstanceAliasProcessingDiagnostics.Instance;
        public IDerivedUnitInstanceProcessingDiagnostics DerivedUnitInstanceDiagnostics => EmptyDerivedUnitInstanceProcessingDiagnostics.Instance;
        public IBiasedUnitInstanceProcessingDiagnostics BiasedUnitInstanceDiagnostics => EmptyBiasedUnitInstanceProcessingDiagnostics.Instance;
        public IPrefixedUnitInstanceProcessingDiagnostics PrefixedUnitInstanceDiagnostics => EmptyPrefixedUnitInstanceProcessingDiagnostics.Instance;
        public IScaledUnitInstanceProcessingDiagnostics ScaledUnitInstanceDiagnostics => EmptyScaledUnitInstanceProcessingDiagnostics.Instance;
    }
}
