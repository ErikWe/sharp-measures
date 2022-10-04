namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Processing;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Processing;

internal interface IScalarProcessingDiagnosticsStrategy
{
    public abstract IScalarTypeProcessingDiagnostics ScalarTypeDiagnostics { get; }

    public abstract ISharpMeasuresScalarProcessingDiagnostics SharpMeasuresScalarDiagnostics { get; }
    public abstract ISpecializedSharpMeasuresScalarProcessingDiagnostics SpecializedSharpMeasuresScalarDiagnostics { get; }

    public abstract IQuantityOperationProcessingDiagnostics QuantityOperationDiagnostics { get; }
    public abstract IQuantityProcessProcessingDiagnostics QuantityProcessDiagnostics { get; }
    public abstract IScalarConstantProcessingDiagnostics ScalarConstantDiagnostics(NamedType unit);
    public abstract IScalarConstantProcessingDiagnostics ScalarConstantDiagnosticsForUnknownUnit { get; }
    public abstract IConvertibleQuantityProcessingDiagnostics ConvertibleScalarDiagnostics { get; }

    public abstract IIncludeUnitBasesProcessingDiagnostics IncludeUnitBasesDiagnostics { get; }
    public abstract IExcludeUnitBasesProcessingDiagnostics ExcludeUnitBasesDiagnostics { get; }
    public abstract IIncludeUnitsProcessingDiagnostics IncludeUnitsDiagnostics { get; }
    public abstract IExcludeUnitsProcessingDiagnostics ExcludeUnitsDiagnostics { get; }
}

internal static class ScalarProcessingDiagnosticsStrategies
{
    public static IScalarProcessingDiagnosticsStrategy Default { get; } = new DefaultProcessingDiagnosticsStrategy();
    public static IScalarProcessingDiagnosticsStrategy EmptyDiagnostics { get; } = new EmptyProcessingDiagnosticsStrategy();

    private class DefaultProcessingDiagnosticsStrategy : IScalarProcessingDiagnosticsStrategy
    {
        public IScalarTypeProcessingDiagnostics ScalarTypeDiagnostics => ScalarTypeProcessingDiagnostics.Instance;

        public ISharpMeasuresScalarProcessingDiagnostics SharpMeasuresScalarDiagnostics => SharpMeasuresScalarProcessingDiagnostics.Instance;
        public ISpecializedSharpMeasuresScalarProcessingDiagnostics SpecializedSharpMeasuresScalarDiagnostics => SpecializedSharpMeasuresScalarProcessingDiagnostics.Instance;

        public IQuantityOperationProcessingDiagnostics QuantityOperationDiagnostics => QuantityOperationProcessingDiagnostics.Instance;
        public IQuantityProcessProcessingDiagnostics QuantityProcessDiagnostics => QuantityProcessProcessingDiagnostics.Instance;
        public IScalarConstantProcessingDiagnostics ScalarConstantDiagnostics(NamedType unit) => new ScalarConstantProcessingDiagnostics(unit);
        public IScalarConstantProcessingDiagnostics ScalarConstantDiagnosticsForUnknownUnit { get; } = new ScalarConstantProcessingDiagnostics();
        public IConvertibleQuantityProcessingDiagnostics ConvertibleScalarDiagnostics => ConvertibleScalarProcessingDiagnostics.Instance;

        public IIncludeUnitBasesProcessingDiagnostics IncludeUnitBasesDiagnostics => IncludeUnitBasesProcessingDiagnostics.Instance;
        public IExcludeUnitBasesProcessingDiagnostics ExcludeUnitBasesDiagnostics => ExcludeUnitBasesProcessingDiagnostics.Instance;
        public IIncludeUnitsProcessingDiagnostics IncludeUnitsDiagnostics => IncludeUnitsProcessingDiagnostics.Instance;
        public IExcludeUnitsProcessingDiagnostics ExcludeUnitsDiagnostics => ExcludeUnitsProcessingDiagnostics.Instance;
    }

    private class EmptyProcessingDiagnosticsStrategy : IScalarProcessingDiagnosticsStrategy
    {
        public IScalarTypeProcessingDiagnostics ScalarTypeDiagnostics => EmptyScalarTypeProcessingDiagnostics.Instance;

        public ISharpMeasuresScalarProcessingDiagnostics SharpMeasuresScalarDiagnostics => EmptySharpMeasuresScalarProcessingDiagnostics.Instance;
        public ISpecializedSharpMeasuresScalarProcessingDiagnostics SpecializedSharpMeasuresScalarDiagnostics => EmptySpecializedSharpMeasuresScalarProcessingDiagnostics.Instance;

        public IQuantityOperationProcessingDiagnostics QuantityOperationDiagnostics => EmptyQuantityOperationProcessingDiagnostics.Instance;
        public IQuantityProcessProcessingDiagnostics QuantityProcessDiagnostics => EmptyQuantityProcessProcessingDiagnostics.Instance;
        public IScalarConstantProcessingDiagnostics ScalarConstantDiagnostics(NamedType _) => ScalarConstantDiagnosticsForUnknownUnit;
        public IScalarConstantProcessingDiagnostics ScalarConstantDiagnosticsForUnknownUnit => EmptyScalarConstantProcessingDiagnostics.Instance;
        public IConvertibleQuantityProcessingDiagnostics ConvertibleScalarDiagnostics => EmptyConvertibleQuantityProcessingDiagnostics.Instance;

        public IIncludeUnitBasesProcessingDiagnostics IncludeUnitBasesDiagnostics => EmptyIncludeUnitBasesProcessingDiagnostics.Instance;
        public IExcludeUnitBasesProcessingDiagnostics ExcludeUnitBasesDiagnostics => EmptyExcludeUnitBasesProcessingDiagnostics.Instance;
        public IIncludeUnitsProcessingDiagnostics IncludeUnitsDiagnostics => EmptyIncludeUnitsProcessingDiagnostics.Instance;
        public IExcludeUnitsProcessingDiagnostics ExcludeUnitsDiagnostics => EmptyExcludeUnitsProcessingDiagnostics.Instance;
    }
}
