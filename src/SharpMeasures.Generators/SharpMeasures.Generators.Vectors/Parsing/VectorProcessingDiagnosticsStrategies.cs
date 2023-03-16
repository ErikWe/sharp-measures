namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Processing;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Processing;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal interface IVectorProcessingDiagnosticsStrategy
{
    public abstract IGroupTypeProcessingDiagnostics GroupTypeDiagnostics { get; }
    public abstract IGroupMemberTypeProcessingDiagnostics GroupMemberTypeDiagnostics { get; }
    public abstract IVectorTypeProcessingDiagnostics VectorTypeDiagnostics { get; }

    public abstract ISharpMeasuresVectorGroupProcessingDiagnostics SharpMeasuresVectorGroupDiagnostics { get; }
    public abstract ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics SpecializedSharpMeasuresVectorGroupDiagnostics { get; }
    public abstract ISharpMeasuresVectorGroupMemberProcessingDiagnostics SharpMeasuresVectorGroupMemberDiagnostics { get; }

    public abstract ISharpMeasuresVectorProcessingDiagnostics SharpMeasuresVectorDiagnostics { get; }
    public abstract ISpecializedSharpMeasuresVectorProcessingDiagnostics SpecializedSharpMeasuresVectorDiagnostics { get; }

    public abstract IQuantityOperationProcessingDiagnostics QuantityOperationDiagnostics { get; }
    public abstract IVectorOperationProcessingDiagnostics VectorOperationDiagnostics { get; }
    public abstract IQuantityProcessProcessingDiagnostics ProcessedQuantityDiagnostics { get; }
    public abstract IVectorConstantProcessingDiagnostics VectorConstantDiagnostics(NamedType unit);
    public abstract IVectorConstantProcessingDiagnostics VectorConstantDiagnosticsForUnknownUnit { get; }
    public abstract IConvertibleQuantityProcessingDiagnostics ConvertibleVectorDiagnostics { get; }

    public abstract IIncludeUnitsProcessingDiagnostics IncludeUnitsDiagnostics { get; }
    public abstract IExcludeUnitsProcessingDiagnostics ExcludeUnitsDiagnostics { get; }
}

internal static class VectorProcessingDiagnosticsStrategies
{
    public static IVectorProcessingDiagnosticsStrategy Default { get; } = new DefaultProcessingDiagnosticsStrategy();
    public static IVectorProcessingDiagnosticsStrategy EmptyDiagnostics { get; } = new EmptyProcessingDiagnosticsStrategy();

    private sealed class DefaultProcessingDiagnosticsStrategy : IVectorProcessingDiagnosticsStrategy
    {
        public IGroupTypeProcessingDiagnostics GroupTypeDiagnostics => GroupTypeProcessingDiagnostics.Instance;
        public IGroupMemberTypeProcessingDiagnostics GroupMemberTypeDiagnostics => GroupMemberTypeProcessingDiagnostics.Instance;
        public IVectorTypeProcessingDiagnostics VectorTypeDiagnostics => VectorTypeProcessingDiagnostics.Instance;

        public ISharpMeasuresVectorGroupProcessingDiagnostics SharpMeasuresVectorGroupDiagnostics => SharpMeasuresVectorGroupProcessingDiagnostics.Instance;
        public ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics SpecializedSharpMeasuresVectorGroupDiagnostics => SpecializedSharpMeasuresVectorGroupProcessingDiagnostics.Instance;
        public ISharpMeasuresVectorGroupMemberProcessingDiagnostics SharpMeasuresVectorGroupMemberDiagnostics => SharpMeasuresVectorGroupMemberProcessingDiagnostics.Instance;

        public ISharpMeasuresVectorProcessingDiagnostics SharpMeasuresVectorDiagnostics { get; } = SharpMeasuresVectorProcessingDiagnostics.Instance;
        public ISpecializedSharpMeasuresVectorProcessingDiagnostics SpecializedSharpMeasuresVectorDiagnostics { get; } = SpecializedSharpMeasuresVectorProcessingDiagnostics.Instance;

        public IQuantityOperationProcessingDiagnostics QuantityOperationDiagnostics => QuantityOperationProcessingDiagnostics.Instance;
        public IVectorOperationProcessingDiagnostics VectorOperationDiagnostics => VectorOperationProcessingDiagnostics.Instance;
        public IQuantityProcessProcessingDiagnostics ProcessedQuantityDiagnostics => QuantityProcessProcessingDiagnostics.Instance;
        public IVectorConstantProcessingDiagnostics VectorConstantDiagnostics(NamedType unit) => new VectorConstantProcessingDiagnostics(unit);
        public IVectorConstantProcessingDiagnostics VectorConstantDiagnosticsForUnknownUnit { get; } = new VectorConstantProcessingDiagnostics();
        public IConvertibleQuantityProcessingDiagnostics ConvertibleVectorDiagnostics => ConvertibleVectorProcessingDiagnostics.Instance;

        public IIncludeUnitsProcessingDiagnostics IncludeUnitsDiagnostics => IncludeUnitsProcessingDiagnostics.Instance;
        public IExcludeUnitsProcessingDiagnostics ExcludeUnitsDiagnostics => ExcludeUnitsProcessingDiagnostics.Instance;
    }

    private sealed class EmptyProcessingDiagnosticsStrategy : IVectorProcessingDiagnosticsStrategy
    {
        public IGroupTypeProcessingDiagnostics GroupTypeDiagnostics => EmptyGroupTypeProcessingDiagnostics.Instance;
        public IGroupMemberTypeProcessingDiagnostics GroupMemberTypeDiagnostics => EmptyGroupMemberTypeProcessingDiagnostics.Instance;
        public IVectorTypeProcessingDiagnostics VectorTypeDiagnostics => EmptyVectorTypeProcessingDiagnostics.Instance;

        public ISharpMeasuresVectorGroupProcessingDiagnostics SharpMeasuresVectorGroupDiagnostics => EmptySharpMeasuresVectorGroupProcessingDiagnostics.Instance;
        public ISpecializedSharpMeasuresVectorGroupProcessingDiagnostics SpecializedSharpMeasuresVectorGroupDiagnostics => EmptySpecializedSharpMeasuresVectorGroupProcessingDiagnostics.Instance;
        public ISharpMeasuresVectorGroupMemberProcessingDiagnostics SharpMeasuresVectorGroupMemberDiagnostics => EmptySharpMeasuresVectorGroupMemberProcessingDiagnostics.Instance;

        public ISharpMeasuresVectorProcessingDiagnostics SharpMeasuresVectorDiagnostics => EmptySharpMeasuresVectorProcessingDiagnostics.Instance;
        public ISpecializedSharpMeasuresVectorProcessingDiagnostics SpecializedSharpMeasuresVectorDiagnostics => EmptySpecializedSharpMeasuresVectorProcessingDiagnostics.Instance;

        public IQuantityOperationProcessingDiagnostics QuantityOperationDiagnostics => EmptyQuantityOperationProcessingDiagnostics.Instance;
        public IVectorOperationProcessingDiagnostics VectorOperationDiagnostics => EmptyVectorOperationProcessingDiagnostics.Instance;
        public IQuantityProcessProcessingDiagnostics ProcessedQuantityDiagnostics => EmptyQuantityProcessProcessingDiagnostics.Instance;
        public IVectorConstantProcessingDiagnostics VectorConstantDiagnostics(NamedType _) => VectorConstantDiagnosticsForUnknownUnit;
        public IVectorConstantProcessingDiagnostics VectorConstantDiagnosticsForUnknownUnit => EmptyVectorConstantProcessingDiagnostics.Instance;
        public IConvertibleQuantityProcessingDiagnostics ConvertibleVectorDiagnostics => EmptyConvertibleQuantityProcessingDiagnostics.Instance;

        public IIncludeUnitsProcessingDiagnostics IncludeUnitsDiagnostics => EmptyIncludeUnitsProcessingDiagnostics.Instance;
        public IExcludeUnitsProcessingDiagnostics ExcludeUnitsDiagnostics => EmptyExcludeUnitsProcessingDiagnostics.Instance;
    }
}
