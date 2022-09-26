namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Validation;
using SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

internal interface IUnitValidationDiagnosticsStrategy
{
    public abstract ISharpMeasuresUnitValidationDiagnostics SharpMeasuresUnitDiagnostics { get; }

    public abstract IDerivableUnitValidationDiagnostics DerivableUnitDiagnostics { get; }

    public abstract IUnitInstanceAliasValidationDiagnostics UnitInstanceAliasDiagnostics { get; }
    public abstract IDerivedUnitInstanceValidationDiagnostics DerivedUnitInstanceDiagnostics { get; }
    public abstract IBiasedUnitInstanceValidationDiagnostics BiasedUnitInstanceDiagnostics { get; }
    public abstract IPrefixedUnitInstanceValidationDiagnostics PrefixedUnitInstanceDiagnostics { get; }
    public abstract IScaledUnitInstanceValidationDiagnostics ScaledUnitInstanceDiagnostics { get; }
}

internal static class UnitValidationDiagnosticsStrategies
{
    public static IUnitValidationDiagnosticsStrategy Default { get; } = new DefaultValidationDiagnosticsStrategy();
    public static IUnitValidationDiagnosticsStrategy EmptyDiagnostics { get; } = new EmptyValidationDiagnosticsStrategy();

    private class DefaultValidationDiagnosticsStrategy : IUnitValidationDiagnosticsStrategy
    {
        public ISharpMeasuresUnitValidationDiagnostics SharpMeasuresUnitDiagnostics => SharpMeasuresUnitValidationDiagnostics.Instance;

        public IDerivableUnitValidationDiagnostics DerivableUnitDiagnostics => DerivableUnitValidationDiagnostics.Instance;

        public IUnitInstanceAliasValidationDiagnostics UnitInstanceAliasDiagnostics => UnitInstanceAliasValidationDiagnostics.Instance;
        public IDerivedUnitInstanceValidationDiagnostics DerivedUnitInstanceDiagnostics => DerivedUnitInstanceValidationDiagnostics.Instance;
        public IBiasedUnitInstanceValidationDiagnostics BiasedUnitInstanceDiagnostics => BiasedUnitInstanceValidationDiagnostics.Instance;
        public IPrefixedUnitInstanceValidationDiagnostics PrefixedUnitInstanceDiagnostics => PrefixedUnitInstanceValidationDiagnostics.Instance;
        public IScaledUnitInstanceValidationDiagnostics ScaledUnitInstanceDiagnostics => ScaledUnitInstanceValidationDiagnostics.Instance;
    }

    private class EmptyValidationDiagnosticsStrategy : IUnitValidationDiagnosticsStrategy
    {
        public ISharpMeasuresUnitValidationDiagnostics SharpMeasuresUnitDiagnostics => EmptySharpMeasuresUnitValidationDiagnostics.Instance;

        public IDerivableUnitValidationDiagnostics DerivableUnitDiagnostics => EmptyDerivableUnitValidationDiagnostics.Instance;

        public IUnitInstanceAliasValidationDiagnostics UnitInstanceAliasDiagnostics => EmptyUnitInstanceAliasValidationDiagnostics.Instance;
        public IDerivedUnitInstanceValidationDiagnostics DerivedUnitInstanceDiagnostics => EmptyDerivedUnitInstanceValidationDiagnostics.Instance;
        public IBiasedUnitInstanceValidationDiagnostics BiasedUnitInstanceDiagnostics => EmptyBiasedUnitInstanceValidationDiagnostics.Instance;
        public IPrefixedUnitInstanceValidationDiagnostics PrefixedUnitInstanceDiagnostics => EmptyPrefixedUnitInstanceValidationDiagnostics.Instance;
        public IScaledUnitInstanceValidationDiagnostics ScaledUnitInstanceDiagnostics => EmptyScaledUnitInstanceValidationDiagnostics.Instance;
    }
}
