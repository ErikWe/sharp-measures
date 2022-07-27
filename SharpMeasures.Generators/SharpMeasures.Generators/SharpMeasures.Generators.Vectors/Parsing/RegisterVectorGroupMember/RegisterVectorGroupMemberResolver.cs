namespace SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Vectors;

internal interface IRegisterVectorGroupMemberResolutionDiagnostics
{
    public abstract Diagnostic? TypeNotVectorGroupMember(IRegisterVectorGroupMemberResolutionContext context, UnresolvedRegisterVectorGroupMemberDefinition definition);
    public abstract Diagnostic? VectorNotMemberOfVectorGroup(IRegisterVectorGroupMemberResolutionContext context, UnresolvedRegisterVectorGroupMemberDefinition definition);
}

internal interface IRegisterVectorGroupMemberResolutionContext : IProcessingContext
{
    public abstract IUnresolvedVectorGroupType VectorGroup { get; }
    public abstract IUnresolvedVectorPopulation VectorPopulation { get; }
}

internal class RegisterVectorGroupMemberResolver : AProcesser<IRegisterVectorGroupMemberResolutionContext, UnresolvedRegisterVectorGroupMemberDefinition, RegisterVectorGroupMemberDefinition>
{
    private IRegisterVectorGroupMemberResolutionDiagnostics Diagnostics { get; }

    public RegisterVectorGroupMemberResolver(IRegisterVectorGroupMemberResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<RegisterVectorGroupMemberDefinition> Process(IRegisterVectorGroupMemberResolutionContext context,
        UnresolvedRegisterVectorGroupMemberDefinition definition)
    {
        if (context.VectorPopulation.VectorGroups.TryGetValue(definition.Vector, out var vectorGroup) is false)
        {
            return OptionalWithDiagnostics.Empty<RegisterVectorGroupMemberDefinition>(Diagnostics.TypeNotVectorGroupMember(context, definition));
        }

        if (vectorGroup.Type != context.VectorGroup.Type)
        {
            return OptionalWithDiagnostics.Empty<RegisterVectorGroupMemberDefinition>(Diagnostics.VectorNotMemberOfVectorGroup(context, definition));
        }

        RegisterVectorGroupMemberDefinition product = new(context.VectorPopulation.IndividualVectors[definition.Vector], definition.Dimension, definition.Locations);

        return OptionalWithDiagnostics.Result(product);
    }
}
