namespace SharpMeasures.Generators.Scalars.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Processing.Diagnostics;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Linq;

internal class GeneratedScalarProcessingContext : IProcessingContext
{
    public DefinedType Type { get; }

    public UnitPopulation UnitPopulation { get; }
    public ScalarPopulation ScalarPopulation { get; }
    public VectorPopulation VectorPopulation { get; }

    public GeneratedScalarProcessingContext(DefinedType type, UnitPopulation unitPopulation, ScalarPopulation scalarPopulation,
        VectorPopulation vectorPopulation)
    {
        Type = type;

        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}

internal class GeneratedScalarProcesser : IProcesser<GeneratedScalarProcessingContext, GeneratedScalar, ProcessedGeneratedScalar>
{
    public static GeneratedScalarProcesser Instance { get; } = new();

    private GeneratedScalarProcesser() { }

    public IOptionalWithDiagnostics<ProcessedGeneratedScalar> Process(GeneratedScalarProcessingContext context, GeneratedScalar input)
    {
        if (context.UnitPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            var diagnostics = GeneratedScalarDiagnostics.TypeAlreadyUnit(input.Locations.Attribute, context.Type.AsNamedType());
            return OptionalWithDiagnostics.Empty<ProcessedGeneratedScalar>(diagnostics);
        }

        var processedUnit = ProcessUnit(context, input);
        var allDiagnostics = processedUnit.Diagnostics;

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<ProcessedGeneratedScalar>(allDiagnostics);
        }

        var processedVector = ProcessVector(context, input);
        allDiagnostics = allDiagnostics.Concat(processedVector.Diagnostics);

        if (processedVector.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<ProcessedGeneratedScalar>(allDiagnostics);
        }

        var processedDefaultUnitName = ProcessDefaultUnitName(input, processedUnit.Result);

        var processedReciprocal = ProcessPowerQuantity(context, input.Reciprocal, input.Locations.Reciprocal);
        var processedSquare = ProcessPowerQuantity(context, input.Square, input.Locations.Square);
        var processedCube = ProcessPowerQuantity(context, input.Cube, input.Locations.Cube);
        var processedSquareRoot = ProcessPowerQuantity(context, input.SquareRoot, input.Locations.SquareRoot);
        var processedCubeRoot = ProcessPowerQuantity(context, input.CubeRoot, input.Locations.CubeRoot);

        allDiagnostics = allDiagnostics.Concat(processedDefaultUnitName.Diagnostics).Concat(processedReciprocal.Diagnostics).Concat(processedSquare.Diagnostics)
            .Concat(processedCube.Diagnostics).Concat(processedSquareRoot.Diagnostics).Concat(processedCubeRoot.Diagnostics);

        ProcessedGeneratedScalar product = new(processedUnit.Result, processedVector.Result, input.Biased, processedDefaultUnitName.Result, input.DefaultUnitSymbol,
            processedReciprocal.Result, processedSquare.Result, processedCube.Result, processedSquareRoot.Result, processedCubeRoot.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<UnitInterface> ProcessUnit(GeneratedScalarProcessingContext context, GeneratedScalar input)
    {
        if (context.UnitPopulation.TryGetValue(input.Unit, out UnitInterface unit) is false)
        {
            return OptionalWithDiagnostics.Empty<UnitInterface>(GeneratedScalarDiagnostics.TypeNotUnit(input));
        }

        if (input.Biased && unit.SupportsBiasedQuantities is false)
        {
            return OptionalWithDiagnostics.Empty<UnitInterface>(GeneratedScalarDiagnostics.UnitNotSupportingBiasedQuantities(input));
        }

        return OptionalWithDiagnostics.Result(unit);
    }

    private static IOptionalWithDiagnostics<VectorCollection> ProcessVector(GeneratedScalarProcessingContext context, GeneratedScalar input)
    {
        if (input.Vector is null)
        {
            return OptionalWithDiagnostics.Result(VectorCollection.Empty);
        }

        if (context.VectorPopulation.VectorGroups.TryGetValue(input.Vector.Value, out ResizedVectorGroup group))
        {
            return OptionalWithDiagnostics.Result(VectorCollection.FromGroup(group));
        }

        if (context.VectorPopulation.UnresolvedVectors.TryGetValue(input.Vector.Value, out VectorInterface unresolvedVector))
        {
            return OptionalWithDiagnostics.Result(new VectorCollection(unresolvedVector));
        }

        return OptionalWithDiagnostics.Empty<VectorCollection>(GeneratedScalarDiagnostics.TypeNotVector(input));
    }

    private static IResultWithDiagnostics<string?> ProcessDefaultUnitName(GeneratedScalar input, UnitInterface unit)
    {
        if (input.DefaultUnitName is null)
        {
            return ResultWithDiagnostics.Construct<string?>(null);
        }

        if (unit.UnitsByName.ContainsKey(input.DefaultUnitName) is false)
        {
            return ResultWithDiagnostics.Construct<string?>(null, GeneratedScalarDiagnostics.UnrecognizedUnit(input, unit));
        }

        return ResultWithDiagnostics.Construct<string?>(input.DefaultUnitName);
    }

    private static IResultWithDiagnostics<ScalarInterface?> ProcessPowerQuantity(GeneratedScalarProcessingContext context, NamedType? quantity, MinimalLocation? location)
    {
        if (quantity is null)
        {
            return ResultWithDiagnostics.Construct<ScalarInterface?>(null);
        }

        if (context.ScalarPopulation.TryGetValue(quantity.Value, out ScalarInterface scalar) is false)
        {
            return ResultWithDiagnostics.Construct<ScalarInterface?>(null, GeneratedScalarDiagnostics.TypeNotScalar(location, quantity.Value));
        }

        return ResultWithDiagnostics.Construct<ScalarInterface?>(scalar);
    }
}
