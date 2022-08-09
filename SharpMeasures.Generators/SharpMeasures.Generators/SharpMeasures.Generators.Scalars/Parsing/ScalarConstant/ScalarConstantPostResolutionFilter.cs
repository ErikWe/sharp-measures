namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System;
using System.Collections.Generic;

internal interface IScalarConstantPostResolutionFilterDiagnostics : IQuantityConstantPostResolutionFilterDiagnostics<ScalarConstantDefinition, ScalarConstantLocations>
{
    public abstract Diagnostic? ConstantNameReservedByUnitBase(IScalarConstantPostResolutionFilterContext context, ScalarConstantDefinition definition);
    public abstract Diagnostic? ConstantMultiplesNameReservedByUnitBase(IScalarConstantPostResolutionFilterContext context, ScalarConstantDefinition definition);
}

internal interface IScalarConstantPostResolutionFilterContext : IQuantityConstantPostResolutionFilterContext
{
    public abstract HashSet<string> IncludedBases { get; }
}

internal class ScalarConstantPostResolutionFilter : QuantityConstantPostResolutionFilter<IScalarConstantPostResolutionFilterContext, ScalarConstantDefinition, ScalarConstantLocations>
{
    private IScalarConstantPostResolutionFilterDiagnostics Diagnostics { get; }

    public ScalarConstantPostResolutionFilter(IScalarConstantPostResolutionFilterDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics CheckValidity(IScalarConstantPostResolutionFilterContext context, ScalarConstantDefinition definition)
    {
        return CheckNameValidity(context, definition);
    }

    protected override IValidityWithDiagnostics CheckNameValidity(IScalarConstantPostResolutionFilterContext context, ScalarConstantDefinition definition)
    {
        if (definition.Name.StartsWith("One", StringComparison.InvariantCulture) && context.IncludedBases.Contains(definition.Name.Substring(3)))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.ConstantNameReservedByUnitBase(context, definition));
        }

        if (definition.GenerateMultiplesProperty && definition.Multiples!.StartsWith("One", StringComparison.InvariantCulture) && context.IncludedBases.Contains(definition.Multiples.Substring(3)))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.ConstantMultiplesNameReservedByUnitBase(context, definition));
        }

        return base.CheckNameValidity(context, definition);
    }
}
