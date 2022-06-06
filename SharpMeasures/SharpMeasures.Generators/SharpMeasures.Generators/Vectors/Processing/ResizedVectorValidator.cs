namespace SharpMeasures.Generators.Vectors.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Processing.Diagnostics;

using System;

internal class ResizedVectorValidatorContext : IValidatorContext
{
    public DefinedType Type { get; }

    public UnitPopulation UnitPopulation { get; }
    public ScalarPopulation ScalarPopulation { get; }
    public VectorPopulation VectorPopulation { get; }

    public ResizedVectorValidatorContext(DefinedType type, UnitPopulation unitPopulation, ScalarPopulation scalarPopulation, VectorPopulation vectorPopulation)
    {
        Type = type;

        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
        VectorPopulation = vectorPopulation;
    }
}

internal class ResizedVectorValidator : IValidator<ResizedVectorValidatorContext, ResizedVector>
{
    public static ResizedVectorValidator Instance { get; } = new();

    private ResizedVectorValidator() { }

    public IValidityWithDiagnostics CheckValidity(ResizedVectorValidatorContext context, ResizedVector definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        if (context.UnitPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            var diagnostics = ResizedVectorDiagnostics.TypeAlreadyUnit(definition.Locations.Attribute, context.Type.AsNamedType());
            return ValidityWithDiagnostics.Invalid(diagnostics);
        }

        if (context.ScalarPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            var diagnostics = ResizedVectorDiagnostics.TypeAlreadyScalar(definition.Locations.Attribute, context.Type.AsNamedType());
            return ValidityWithDiagnostics.Invalid(diagnostics);
        }

        if (context.VectorPopulation.VectorGroups.ContainsKey(context.Type.AsNamedType()))
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (context.VectorPopulation.UnresolvedVectors.ContainsKey(context.Type.AsNamedType()))
        {
            return ValidityWithDiagnostics.Invalid(ResizedVectorDiagnostics.UnresolvedVectorGroup(definition));
        }

        if (context.VectorPopulation.DuplicateDimensionVectors.ContainsKey(context.Type.AsNamedType()))
        {
            return ValidityWithDiagnostics.Invalid(ResizedVectorDiagnostics.DuplicateDimension(definition));
        }

        return ValidityWithDiagnostics.Invalid(ResizedVectorDiagnostics.TypeNotVector(definition));
    }
}
