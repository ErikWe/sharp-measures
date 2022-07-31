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
        if (context.VectorPopulation.VectorGroupMembers.TryGetValue(definition.Vector, out var vectorGroupMember) is false)
        {
            return OptionalWithDiagnostics.Empty<RegisterVectorGroupMemberDefinition>(Diagnostics.TypeNotVectorGroupMember(context, definition));
        }

        if (vectorGroupMember.Definition.VectorGroup != context.VectorGroup.Type.AsNamedType())
        {
            return OptionalWithDiagnostics.Empty<RegisterVectorGroupMemberDefinition>(Diagnostics.VectorNotMemberOfVectorGroup(context, definition));
        }

        RegisterVectorGroupMemberDefinition product = new(vectorGroupMember, definition.Dimension, definition.Locations);

        return OptionalWithDiagnostics.Result(product);
    }
}
