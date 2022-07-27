namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Conversions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Utility;
using SharpMeasures.Generators.Unresolved.Vectors;

using System;
using System.Linq;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

        context.AddSource($"{data.Vector.Name}_{data.Dimension}_Conversions.g.cs", SourceText.From(source, Encoding.UTF8));
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
        private UsingsCollector UsingsCollector { get; }

        private Composer(DataModel data)
        {
            Data = data;

            UsingsCollector = UsingsCollector.Delayed(Builder, Data.Vector.Namespace);
            UsingsCollector.AddUsing("SharpMeasures");

            if (Data.Vector.IsReferenceType)
            {
                UsingsCollector.AddUsing("System");
            }
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            UsingsCollector.MarkInsertionPoint();

            Builder.AppendLine(Data.Vector.ComposeDeclaration());

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);

            UsingsCollector.InsertUsings();
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            foreach (var convertibleVectorGroup in Data.Conversions.SelectMany(static (x) => x.VectorGroups))
            {
                if (convertibleVectorGroup.RegisteredMembersByDimension.TryGetValue(Data.Dimension, out var correspondingMember))
                {
                    ComposeInstanceConversion(correspondingMember, indentation);
                }
            }

            foreach (var convertibleVectorGroups in Data.Conversions)
            {
                if (convertibleVectorGroups.CastOperatorBehaviour is ConversionOperatorBehaviour.None)
                {
                    continue;
                }

                Action<IUnresolvedRegisteredVectorGroupMember, Indentation> composer = convertibleVectorGroups.CastOperatorBehaviour switch
                {
                    ConversionOperatorBehaviour.Explicit => ComposeExplicitOperatorConversion,
                    ConversionOperatorBehaviour.Implicit => ComposeImplicitOperatorConversion,
                    _ => throw new NotSupportedException("Invalid cast operation")
                };

                foreach (var convertibleVectorGroup in convertibleVectorGroups.VectorGroups)
                {
                    if (convertibleVectorGroup.RegisteredMembersByDimension.TryGetValue(Data.Dimension, out var correspondingMember))
                    {
                        composer(correspondingMember, indentation);
                    }
                }
            }
        }

        private void ComposeInstanceConversion(IUnresolvedRegisteredVectorGroupMember vectorGroupMember, Indentation indentation)
        {
            UsingsCollector.AddUsing(vectorGroupMember.Vector.Namespace);

            AppendDocumentation(indentation, Data.Documentation.AsDimensionallyEquivalent(vectorGroupMember));
            Builder.AppendLine($"{indentation}public {vectorGroupMember.Vector.Name} As{vectorGroupMember.Vector.Name} => new(Components);");
        }

        private void ComposeExplicitOperatorConversion(IUnresolvedRegisteredVectorGroupMember vector, Indentation indentation)
            => ComposeOperatorConversion(vector, indentation, "explicit");

        private void ComposeImplicitOperatorConversion(IUnresolvedRegisteredVectorGroupMember vector, Indentation indentation)
            => ComposeOperatorConversion(vector, indentation, "implicit");

        private void ComposeOperatorConversion(IUnresolvedRegisteredVectorGroupMember vector, Indentation indentation, string behaviour)
        {
            AppendDocumentation(indentation, Data.Documentation.CastToDimensionallyEquivalent(vector));
            
            if (Data.Vector.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public static {{behaviour}} operator {{vector.Vector.Name}}({{Data.Vector.Name}} a)
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull(a);

                    {{indentation.Increased}}return new(a.Components);
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public static {behaviour} operator {vector.Vector.Name}({Data.Vector.Name} a) => new(a.Components);");
            }
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }
}
