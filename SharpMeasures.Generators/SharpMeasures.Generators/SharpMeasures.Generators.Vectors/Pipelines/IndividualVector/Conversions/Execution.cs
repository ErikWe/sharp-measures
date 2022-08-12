namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Conversions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Utility;

using System;
using System.Linq;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

        context.AddSource($"{data.Vector.Name}_Conversions.g.cs", SourceText.From(source, Encoding.UTF8));
    }

    private class Composer
    {
        public static string Compose(DataModel data)
        {
            Composer composer = new(data);
            composer.Compose();
            return composer.Retrieve();
        }

        private StringBuilder Builder { get; } = new();

        private DataModel Data { get; }

        private Composer(DataModel data)
        {
            Data = data;
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            Builder.AppendLine(Data.Vector.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            foreach (var convertibleVectorGroup in Data.Conversions.SelectMany(static (x) => x.VectorGroups))
            {
                if (Data.VectorGroupPopulation.TryGetValue(convertibleVectorGroup.Type.AsNamedType(), out var resolvedVectorGroup))
                {
                    if (resolvedVectorGroup.MembersByDimension.TryGetValue(Data.Dimension, out var correspondingMember))
                    {
                        ComposeInstanceConversion(correspondingMember, indentation);
                    }
                }
            }

            foreach (var convertibleVectorGroups in Data.Conversions)
            {
                if (convertibleVectorGroups.CastOperatorBehaviour is ConversionOperatorBehaviour.None)
                {
                    continue;
                }

                Action<IUnresolvedVectorGroupMemberType, Indentation> composer = convertibleVectorGroups.CastOperatorBehaviour switch
                {
                    ConversionOperatorBehaviour.Explicit => ComposeExplicitOperatorConversion,
                    ConversionOperatorBehaviour.Implicit => ComposeImplicitOperatorConversion,
                    _ => throw new NotSupportedException("Invalid cast operation")
                };

                foreach (var convertibleVectorGroup in convertibleVectorGroups.VectorGroups)
                {
                    if (Data.VectorGroupPopulation.TryGetValue(convertibleVectorGroup.Type.AsNamedType(), out var resolvedVectorGroup))
                    {
                        if (resolvedVectorGroup.MembersByDimension.TryGetValue(Data.Dimension, out var correspondingMember))
                        {
                            composer(correspondingMember, indentation);
                        }
                    }
                }
            }
        }

        private void ComposeInstanceConversion(IUnresolvedVectorGroupMemberType vectorGroupMember, Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.Conversion(vectorGroupMember));
            Builder.AppendLine($"{indentation}public {vectorGroupMember.Type.FullyQualifiedName} As{vectorGroupMember.Type.Name} => new(Components);");
        }

        private void ComposeExplicitOperatorConversion(IUnresolvedVectorGroupMemberType vectorGroupMember, Indentation indentation)
            => ComposeOperatorConversion(vectorGroupMember, indentation, "explicit");

        private void ComposeImplicitOperatorConversion(IUnresolvedVectorGroupMemberType vectorGroupMember, Indentation indentation)
            => ComposeOperatorConversion(vectorGroupMember, indentation, "implicit");

        private void ComposeOperatorConversion(IUnresolvedVectorGroupMemberType vectorGroupMember, Indentation indentation, string behaviour)
        {
            AppendDocumentation(indentation, Data.Documentation.CastConversion(vectorGroupMember));
            
            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="global::System.ArgumentNullException"/>
                    {{indentation}}public static {{behaviour}} operator {{vectorGroupMember.Type.Name}}({{Data.Vector.FullyQualifiedName}} a)
                    {{indentation}}{
                    {{indentation.Increased}}global::System.ArgumentNullException.ThrowIfNull(a);

                    {{indentation.Increased}}return new(a.Components);
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public static {behaviour} operator {vectorGroupMember.Type.Name}({Data.Vector.FullyQualifiedName} a) => new(a.Components);");
            }
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
