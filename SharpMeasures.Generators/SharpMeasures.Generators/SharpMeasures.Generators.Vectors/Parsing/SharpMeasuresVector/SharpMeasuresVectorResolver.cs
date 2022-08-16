namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Linq;

internal interface ISharpMeasuresVectorResolutionDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeNotUnit(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeNotScalar(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceNotVector(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceVectorGroupLacksMatchingDimension(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? UnrecognizedDefaultUnit(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition);
}

internal interface ISharpMeasuresVectorResolutionContext : IProcessingContext
{
    public abstract IRawUnitPopulation UnitPopulation { get; }
    public abstract IRawScalarPopulation ScalarPopulation { get; }
    public abstract IUnresolvedVectorPopulationWithData VectorPopulation { get; }
}

internal class SharpMeasuresVectorResolver : IProcesser<ISharpMeasuresVectorResolutionContext, UnresolvedSharpMeasuresVectorDefinition, SharpMeasuresVectorDefinition>
{
    private ISharpMeasuresVectorResolutionDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorResolver(ISharpMeasuresVectorResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SharpMeasuresVectorDefinition> Process(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition)
    {
        if (context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorDefinition>(Diagnostics.TypeAlreadyUnit(context, definition));
        }

        if (context.ScalarPopulation.Scalars.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorDefinition>(Diagnostics.TypeAlreadyScalar(context, definition));
        }

        var processedUnit = ProcessUnit(context, definition);
        var allDiagnostics = processedUnit.Diagnostics;

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorDefinition>(allDiagnostics);
        }

        var processedScalar = ProcessScalar(context, definition);
        var processedDifference = ProcessDifference(context, definition);
        var processedDefaultUnitName = ProcessDefaultUnitName(context, definition, processedUnit.Result);

        allDiagnostics = allDiagnostics.Concat(processedScalar.Diagnostics).Concat(processedDifference.Diagnostics).Concat(processedDefaultUnitName.Diagnostics);

        SharpMeasuresVectorDefinition product = new(processedUnit.Result, processedScalar.NullableResult, definition.Dimension, definition.ImplementSum,
            definition.ImplementDifference, processedDifference.Result, processedDefaultUnitName.NullableResult, definition.DefaultUnitSymbol,
            definition.GenerateDocumentation, definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<IRawUnitType> ProcessUnit(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition)
    {
        if (context.UnitPopulation.Units.TryGetValue(definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<IRawUnitType>(Diagnostics.TypeNotUnit(context, definition));
        }

        return OptionalWithDiagnostics.Result(unit);
    }

    private IOptionalWithDiagnostics<IRawScalarType> ProcessScalar(ISharpMeasuresVectorResolutionContext context,
        UnresolvedSharpMeasuresVectorDefinition definition)
    {
        if (definition.Scalar is null)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<IRawScalarType>();
        }

        if (context.ScalarPopulation.Scalars.TryGetValue(definition.Scalar.Value, out var scalar) is false)
        {
            return OptionalWithDiagnostics.Empty<IRawScalarType>(Diagnostics.TypeNotScalar(context, definition));
        }

        return OptionalWithDiagnostics.Result(scalar);
    }

    private IResultWithDiagnostics<IRawVectorGroupType> ProcessDifference(ISharpMeasuresVectorResolutionContext context, UnresolvedSharpMeasuresVectorDefinition definition)
    {
        if (context.VectorPopulation.IndividualVectors.TryGetValue(definition.Difference, out var vector) is false)
        {
            if (context.VectorPopulation.VectorGroups.TryGetValue(definition.Difference, out var vectorGroup) is false)
            {
                var diagnostics = Diagnostics.DifferenceNotVector(context, definition);
                var selfType = context.VectorPopulation.IndividualVectors[context.Type.AsNamedType()];

                return ResultWithDiagnostics.Construct(selfType as IRawVectorGroupType, diagnostics);
            }

            if (context.VectorPopulation.VectorGroupMembersByGroup[vectorGroup.Type.AsNamedType()].VectorGroupMembersByDimension.ContainsKey(definition.Dimension) is false)
            {
                var diagnostics = Diagnostics.DifferenceVectorGroupLacksMatchingDimension(context, definition);
                var selfType = context.VectorPopulation.IndividualVectors[context.Type.AsNamedType()];

                return ResultWithDiagnostics.Construct(selfType as IRawVectorGroupType, diagnostics);
            }

            return ResultWithDiagnostics.Construct(vectorGroup);
        }

        return ResultWithDiagnostics.Construct(vector as IRawVectorGroupType);
    }

    private IResultWithDiagnostics<IRawUnitInstance?> ProcessDefaultUnitName(ISharpMeasuresVectorResolutionContext context,
        UnresolvedSharpMeasuresVectorDefinition definition, IRawUnitType unit)
    {
        if (definition.DefaultUnitName is null)
        {
            return ResultWithDiagnostics.Construct<IRawUnitInstance?>(null);
        }

        if (unit.UnitsByName.TryGetValue(definition.DefaultUnitName, out var unitInstance) is false)
        {
            return ResultWithDiagnostics.Construct<IRawUnitInstance?>(null, Diagnostics.UnrecognizedDefaultUnit(context, definition));
        }

        return ResultWithDiagnostics.Construct<IRawUnitInstance?>(unitInstance);
    }
}
