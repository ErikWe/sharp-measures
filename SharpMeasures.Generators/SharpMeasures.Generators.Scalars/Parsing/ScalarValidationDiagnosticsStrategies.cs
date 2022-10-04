namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Validation;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Validation;

internal interface IScalarValidationDiagnosticsStrategy
{
    public abstract ISharpMeasuresScalarValidationDiagnostics SharpMeasuresScalarDiagnostics { get; }
    public abstract ISpecializedSharpMeasuresScalarValidationDiagnostics SpecializedSharpMeasuresScalarDiagnostics { get; }

    public abstract IQuantityOperationValidationDiagnostics QuantityOperationDiagnostics { get; }
    public abstract IScalarConstantValidationDiagnostics ScalarConstantDiagnostics { get; }
    public abstract IConvertibleScalarFilteringDiagnostics ConvertibleScalarDiagnostics { get; }

    public abstract IIncludeUnitBasesFilteringDiagnostics IncludeUnitBasesDiagnostics { get; }
    public abstract IExcludeUnitBasesFilteringDiagnostics ExcludeUnitBasesDiagnostics { get; }
    public abstract IIncludeUnitsFilteringDiagnostics IncludeUnitsDiagnostics { get; }
    public abstract IExcludeUnitsFilteringDiagnostics ExcludeUnitsDiagnostics { get; }
}

internal static class ScalarValidationDiagnosticsStrategies
{
    public static IScalarValidationDiagnosticsStrategy Default { get; } = new DefaultValidationDiagnosticsStrategy();
    public static IScalarValidationDiagnosticsStrategy EmptyDiagnostics { get; } = new EmptyValidationDiagnosticsStrategy();

    private class DefaultValidationDiagnosticsStrategy : IScalarValidationDiagnosticsStrategy
    {
        public ISharpMeasuresScalarValidationDiagnostics SharpMeasuresScalarDiagnostics => SharpMeasuresScalarValidationDiagnostics.Instance;
        public ISpecializedSharpMeasuresScalarValidationDiagnostics SpecializedSharpMeasuresScalarDiagnostics => SpecializedSharpMeasuresScalarValidationDiagnostics.Instance;

        public IQuantityOperationValidationDiagnostics QuantityOperationDiagnostics => QuantityOperationValidationDiagnostics.Instance;
        public IScalarConstantValidationDiagnostics ScalarConstantDiagnostics => ScalarConstantValidationDiagnostics.Instance;
        public IConvertibleScalarFilteringDiagnostics ConvertibleScalarDiagnostics => ConvertibleScalarFilteringDiagnostics.Instance;

        public IIncludeUnitBasesFilteringDiagnostics IncludeUnitBasesDiagnostics => IncludeUnitBasesFilteringDiagnostics.Instance;
        public IExcludeUnitBasesFilteringDiagnostics ExcludeUnitBasesDiagnostics => ExcludeUnitBasesFilteringDiagnostics.Instance;
        public IIncludeUnitsFilteringDiagnostics IncludeUnitsDiagnostics => IncludeUnitsFilteringDiagnostics.Instance;
        public IExcludeUnitsFilteringDiagnostics ExcludeUnitsDiagnostics => ExcludeUnitsFilteringDiagnostics.Instance;
    }

    private class EmptyValidationDiagnosticsStrategy : IScalarValidationDiagnosticsStrategy
    {
        public ISharpMeasuresScalarValidationDiagnostics SharpMeasuresScalarDiagnostics => EmptySharpMeasuresScalarValidationDiagnostics.Instance;
        public ISpecializedSharpMeasuresScalarValidationDiagnostics SpecializedSharpMeasuresScalarDiagnostics => SpecializedSharpMeasuresScalarValidationDiagnostics.Instance;

        public IQuantityOperationValidationDiagnostics QuantityOperationDiagnostics => EmptyQuantityOperationValidationDiagnostics.Instance;
        public IScalarConstantValidationDiagnostics ScalarConstantDiagnostics => EmptyScalarConstantValidationDiagnostics.Instance;
        public IConvertibleScalarFilteringDiagnostics ConvertibleScalarDiagnostics => EmptyConvertibleScalarFilteringDiagnostics.Instance;

        public IIncludeUnitBasesFilteringDiagnostics IncludeUnitBasesDiagnostics => EmptyIncludeUnitBasesFilteringDiagnostics.Instance;
        public IExcludeUnitBasesFilteringDiagnostics ExcludeUnitBasesDiagnostics => EmptyExcludeUnitBasesFilteringDiagnostics.Instance;
        public IIncludeUnitsFilteringDiagnostics IncludeUnitsDiagnostics => EmptyIncludeUnitsFilteringDiagnostics.Instance;
        public IExcludeUnitsFilteringDiagnostics ExcludeUnitsDiagnostics => EmptyExcludeUnitsFilteringDiagnostics.Instance;
    }
}
