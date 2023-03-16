namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Validation;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Validation;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal interface IVectorValidationDiagnosticsStrategy
{
    public abstract ISharpMeasuresVectorGroupValidationDiagnostics SharpMeasuresVectorGroupDiagnostics { get; }
    public abstract ISpecializedSharpMeasuresVectorGroupValidationDiagnostics SpecializedSharpMeasuresVectorGroupDiagnostics { get; }
    public abstract ISharpMeasuresVectorGroupMemberValidationDiagnostics SharpMeasuresVectorGroupMemberDiagnostics { get; }

    public abstract ISharpMeasuresVectorValidationDiagnostics SharpMeasuresVectorDiagnostics { get; }
    public abstract ISpecializedSharpMeasuresVectorValidationDiagnostics SpecializedSharpMeasuresVectorDiagnostics { get; }

    public abstract IQuantityOperationValidationDiagnostics QuantityOperationDiagnostics { get; }
    public abstract IVectorOperationValidationDiagnostics VectorOperationDiagnostics { get; }
    public abstract IVectorConstantValidationDiagnostics VectorConstantDiagnostics { get; }
    public abstract IConvertibleVectorFilteringDiagnostics ConvertibleVectorDiagnostics { get; }

    public abstract IIncludeUnitsFilteringDiagnostics IncludeUnitsDiagnostics { get; }
    public abstract IExcludeUnitsFilteringDiagnostics ExcludeUnitsDiagnostics { get; }
}

internal static class VectorValidationDiagnosticsStrategies
{
    public static IVectorValidationDiagnosticsStrategy Default { get; } = new DefaultValidationDiagnosticsStrategy();
    public static IVectorValidationDiagnosticsStrategy EmptyDiagnostics { get; } = new EmptyValidationDiagnosticsStrategy();

    private sealed class DefaultValidationDiagnosticsStrategy : IVectorValidationDiagnosticsStrategy
    {
        public ISharpMeasuresVectorGroupValidationDiagnostics SharpMeasuresVectorGroupDiagnostics => SharpMeasuresVectorGroupValidationDiagnostics.Instance;
        public ISpecializedSharpMeasuresVectorGroupValidationDiagnostics SpecializedSharpMeasuresVectorGroupDiagnostics => SpecializedSharpMeasuresVectorGroupValidationDiagnostics.Instance;
        public ISharpMeasuresVectorGroupMemberValidationDiagnostics SharpMeasuresVectorGroupMemberDiagnostics => SharpMeasuresVectorGroupMemberValidationDiagnostics.Instance;

        public ISharpMeasuresVectorValidationDiagnostics SharpMeasuresVectorDiagnostics => SharpMeasuresVectorValidationDiagnostics.Instance;
        public ISpecializedSharpMeasuresVectorValidationDiagnostics SpecializedSharpMeasuresVectorDiagnostics => SpecializedSharpMeasuresVectorValidationDiagnostics.Instance;

        public IQuantityOperationValidationDiagnostics QuantityOperationDiagnostics => QuantityOperationValidationDiagnostics.Instance;
        public IVectorOperationValidationDiagnostics VectorOperationDiagnostics => VectorOperationValidationDiagnostics.Instance;
        public IVectorConstantValidationDiagnostics VectorConstantDiagnostics => VectorConstantValidationDiagnostics.Instance;
        public IConvertibleVectorFilteringDiagnostics ConvertibleVectorDiagnostics => ConvertibleVectorFilteringDiagnostics.Instance;

        public IIncludeUnitsFilteringDiagnostics IncludeUnitsDiagnostics => IncludeUnitsFilteringDiagnostics.Instance;
        public IExcludeUnitsFilteringDiagnostics ExcludeUnitsDiagnostics => ExcludeUnitsFilteringDiagnostics.Instance;
    }

    private sealed class EmptyValidationDiagnosticsStrategy : IVectorValidationDiagnosticsStrategy
    {
        public ISharpMeasuresVectorGroupValidationDiagnostics SharpMeasuresVectorGroupDiagnostics => EmptySharpMeasuresVectorGroupValidationDiagnostics.Instance;
        public ISpecializedSharpMeasuresVectorGroupValidationDiagnostics SpecializedSharpMeasuresVectorGroupDiagnostics => EmptySpecializedSharpMeasuresVectorGroupValidationDiagnostics.Instance;
        public ISharpMeasuresVectorGroupMemberValidationDiagnostics SharpMeasuresVectorGroupMemberDiagnostics => EmptySharpMeasuresVectorGroupMemberValidationDiagnostics.Instance;

        public ISharpMeasuresVectorValidationDiagnostics SharpMeasuresVectorDiagnostics => EmptySharpMeasuresVectorValidationDiagnostics.Instance;
        public ISpecializedSharpMeasuresVectorValidationDiagnostics SpecializedSharpMeasuresVectorDiagnostics => EmptySpecializedSharpMeasuresVectorValidationDiagnostics.Instance;

        public IQuantityOperationValidationDiagnostics QuantityOperationDiagnostics => EmptyQuantityOperationValidationDiagnostics.Instance;
        public IVectorOperationValidationDiagnostics VectorOperationDiagnostics => EmptyVectorOperationValidationDiagnostics.Instance;
        public IVectorConstantValidationDiagnostics VectorConstantDiagnostics => EmptyVectorConstantValidationDiagnostics.Instance;
        public IConvertibleVectorFilteringDiagnostics ConvertibleVectorDiagnostics => EmptyConvertibleVectorFilteringDiagnostics.Instance;

        public IIncludeUnitsFilteringDiagnostics IncludeUnitsDiagnostics => EmptyIncludeUnitsFilteringDiagnostics.Instance;
        public IExcludeUnitsFilteringDiagnostics ExcludeUnitsDiagnostics => EmptyExcludeUnitsFilteringDiagnostics.Instance;
    }
}
