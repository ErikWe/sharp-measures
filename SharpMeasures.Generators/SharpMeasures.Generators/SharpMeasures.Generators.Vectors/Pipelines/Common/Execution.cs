namespace SharpMeasures.Generators.Vectors.Pipelines.Common;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.SourceBuilding;

using System;
using System.Collections.Generic;
using System.Text;

internal static class Execution
{
    public static void Execute(SourceProductionContext context, DataModel data)
    {
        string source = Composer.Compose(data);

        context.AddSource($"{data.Vector.Name}_{data.Dimension}_Common.g.cs", SourceText.From(source, Encoding.UTF8));
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

        private string UnitParameterName { get; }
        private SpecificTexts Texts { get; }

        private Composer(DataModel data)
        {
            Data = data;

            UsingsCollector = UsingsCollector.Delayed(Builder, Data.Vector.Namespace);
            UsingsCollector.AddUsings("SharpMeasures", "SharpMeasures.Maths", Data.Unit.Namespace);

            UnitParameterName = SourceBuildingUtility.ToParameterName(Data.Unit.Name);
            Texts = new(data.Dimension, data.Scalar, data.UnitQuantity, UnitParameterName);

            if (Data.Scalar is not null)
            {
                UsingsCollector.AddUsing(Data.Scalar.Value.Namespace);
            }

            if (Data.Unit.IsReferenceType || Data.Scalar?.IsReferenceType is true)
            {
                UsingsCollector.AddUsing("System");
            }

            if (Data.Dimension is 3)
            {
                UsingsCollector.AddUsing("System.Numerics");
            }
        }

        private void Compose()
        {
            StaticBuilding.AppendHeaderAndDirectives(Builder);

            NamespaceBuilding.AppendNamespace(Builder, Data.Vector.Namespace);

            UsingsCollector.MarkInsertionPoint();

            AppendDocumentation(new Indentation(0), Data.Documentation.Header());
            Builder.AppendLine(Data.Vector.ComposeDeclaration());

            InterfaceBuilding.AppendInterfaceImplementationOnNewLines(Builder, new Indentation(1), new string[]
            {
                $"IVector{Data.Dimension}"
            });

            BlockBuilding.AppendBlock(Builder, ComposeTypeBlock, originalIndentationLevel: 0);

            UsingsCollector.InsertUsings();
        }

        private string Retrieve()
        {
            return Builder.ToString();
        }

        private void ComposeTypeBlock(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.Zero());
            Builder.AppendLine($"{indentation}public static {Data.Vector.Name} Zero {{ get; }} = ({ConstantVectorTexts.Zeros(Data.Dimension)});");

            Builder.AppendLine();

            ComposeComponents(indentation);

            Builder.AppendLine();

            ComposeConstructors(indentation);

            Builder.AppendLine();

            ComposeMagnitude(indentation);

            Builder.AppendLine();

            ComposeInUnit(indentation);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.Normalize());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name} Normalize() => VectorMaths.Normalize(this);");

            Builder.AppendLine();
            
            if (Data.Dimension is 3)
            {
                AppendDocumentation(indentation, Data.Documentation.Transform());
                Builder.AppendLine($"{indentation}public {Data.Vector.Name} Transform(Matrix4x4 transform) => VectorMaths.Transform(this, transform);");
                
                Builder.AppendLine();
            }

            ComposeToString(indentation);

            Builder.AppendLine();

            ComposeDeconstruct(indentation);

            if (Data.Vector.IsRecord is false)
            {
                ComposeEquality(indentation);

                Builder.AppendLine();

                ComposeGetHashCode(indentation);

                Builder.AppendLine();
            }

            if (Data.Scalar is not null)
            {
                Builder.AppendLine();

                ComposeCastFromComponents(indentation);
            }

            if (Data.Unit.IsReferenceType)
            {
                Builder.AppendLine();

                ComposeComputationUtilityMethod(indentation);
            }
        }

        private void ComposeComponents(Indentation indentation)
        {
            if (Data.Scalar is null)
            {
                ComposeComponentsAsScalars(indentation);
            }
            else
            {
                ComposeComponentsAsTypes(indentation, Data.Scalar.Value);
            }
        }

        private void ComposeConstructors(Indentation indentation)
        {
            if (Data.Scalar is null)
            {
                ComposeConstructorsToScalars(indentation);
            }
            else
            {
                ComposeConstructorsToComponents(indentation);
            }

            Builder.AppendLine();
            ComposeCommonConstructors(indentation);
        }

        private void ComposeMagnitude(Indentation indentation)
        {
            if (Data.Scalar is null)
            {
                AppendDocumentation(indentation, Data.Documentation.ScalarMagnitude());
                Builder.AppendLine($"{indentation}public Scalar Magnitude() => ScalarMaths.Magnitude{Data.Dimension}(this);");
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.Magnitude());
                Builder.AppendLine($"{indentation}public {Data.Scalar.Value.Name} Magnitude() => ScalarMaths.Magnitude{Data.Dimension}(this);");

                AppendDocumentation(indentation, Data.Documentation.ScalarMagnitude());
                Builder.AppendLine($"{indentation}Scalar IVector{Data.Dimension}.Magnitude() => PureScalarMaths.Magnitude{Data.Dimension}(this);");
            }

            Builder.AppendLine();

            if (Data.SquaredScalar is null)
            {
                AppendDocumentation(indentation, Data.Documentation.ScalarSquaredMagnitude());
                Builder.AppendLine($"{indentation}public Scalar SquaredMagnitude() => ScalarMaths.SquaredMagnitude{Data.Dimension}(this);");
            }
            else
            {
                AppendDocumentation(indentation, Data.Documentation.SquaredMagnitude());
                Builder.AppendLine($"{indentation}public {Data.SquaredScalar} SquaredMagnitude() => SquaredScalarMaths.SquaredMagnitude{Data.Dimension}(this);");

                AppendDocumentation(indentation, Data.Documentation.ScalarSquaredMagnitude());
                Builder.AppendLine($"{indentation}Scalar IVector{Data.Dimension}.SquaredMagnitude() => PureScalarMaths.SquaredMagnitude{Data.Dimension}(this);");
            }
        }

        private void ComposeInUnit(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.InUnit());

            if (Data.Unit.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public Vector{{Data.Dimension}} InUnit({{Data.Unit.Name}} {{Data.UnitParameterName}})
                    {{indentation}}{
                    {{indentation.Increased}}ArgumentNullException.ThrowIfNull({{Data.UnitParameterName}});

                    {{indentation.Increased}}return new({{ComposeInUnitComputation()}});
                    {{indentation}}}
                    """);
            }
            else
            {
                Builder.AppendLine($"{indentation}public Vector{Data.Dimension} InUnit({Data.Unit.Name} {Data.UnitParameterName})");
                Builder.AppendLine($"{indentation.Increased}=> new({ComposeInUnitComputation()});");
            }
        }

        private void ComposeComponentsAsScalars(Indentation indentation)
        {
            for (int i = 0; i < Data.Dimension; i++)
            {
                AppendDocumentation(indentation, Data.Documentation.ComponentMagnitude(i));
                Builder.AppendLine($"{indentation}public Scalar {Texts.Upper.ComponentName(i)} {{ get; }}");
            }

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.Components());
            Builder.AppendLine($"{indentation}public Vector{Data.Dimension} Components => ({ConstantVectorTexts.Upper.Name(Data.Dimension)});");
        }

        private void ComposeComponentsAsTypes(Indentation indentation, NamedType scalar)
        {
            for (int i = 0; i < Data.Dimension; i++)
            {
                AppendDocumentation(indentation, Data.Documentation.Component(i));
                Builder.AppendLine($"{indentation}public {scalar.Name} {Texts.Upper.ComponentName(i)} {{ get; }}");
            }

            Builder.AppendLine();

            for (int i = 0; i < Data.Dimension; i++)
            {
                AppendDocumentation(indentation, Data.Documentation.ComponentMagnitude(i));
                Builder.AppendLine($"{indentation}Scalar IVector{Data.Dimension}.{Texts.Upper.ComponentName(i)} => {Texts.Upper.ComponentName(i)}.Magnitude;");
            }

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.Components());
            Builder.AppendLine($"{indentation}public Vector{Data.Dimension} Components => ({ConstantVectorTexts.Upper.Magnitude(Data.Dimension)});");
        }

        private void ComposeConstructorsToScalars(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.ScalarsConstructor());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name}({ConstantVectorTexts.Lower.Scalar(Data.Dimension)})");
            BlockBuilding.AppendBlock(Builder, ComposeConstructorBlock, indentation);
        }

        private void ComposeConstructorsToComponents(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.ComponentsConstructor());

            if (Data.Scalar!.Value.IsReferenceType)
            {
                Builder.AppendLine($$"""
                    {{indentation}}/// <exception cref="ArgumentNullException"/>
                    {{indentation}}public {{Data.Vector.Name}}({{Texts.Lower.Component}})
                    {{indentation}}{
                    """);

                for (int i = 0; i < Data.Dimension; i++)
                {
                    Builder.AppendLine($"{indentation.Increased}ArgumentNullException.ThrowIfNull({Texts.Lower.ComponentName(i)});");
                }

                Builder.AppendLine();

                ComposeConstructorBlock(indentation.Increased);

                Builder.AppendLine($$"""{{indentation}}}""");
            }
            else
            {
                Builder.AppendLine($"{indentation}public {Data.Vector.Name}({Texts.Lower.Component})");
                BlockBuilding.AppendBlock(Builder, ComposeConstructorBlock, indentation);
            }

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.ScalarsConstructor());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name}({ConstantVectorTexts.Lower.Scalar(Data.Dimension)})");
            Builder.AppendLine($"{indentation.Increased}: this({Texts.Lower.NewComponent}) {{ }}");
        }

        private void ComposeConstructorBlock(Indentation indentation)
        {
            for (int i = 0; i < Data.Dimension; i++)
            {
                Builder.AppendLine($"{indentation}{Texts.Upper.ComponentName(i)} = {Texts.Lower.ComponentName(i)};");
            }
        }

        private void ComposeCommonConstructors(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.VectorConstructor());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name}(Vector{Data.Dimension}) components)");
            Builder.AppendLine($"{indentation.Increased}: this({ConstantVectorTexts.Upper.ComponentsAccess(Data.Dimension)}) {{ }}");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.ScalarsAndUnitConstructor());
            
            if (Data.Unit.IsReferenceType)
            {
                Builder.AppendLine($"""{indentation}/// <exception cref="ArgumentNullException"/>""");
            }
            
            Builder.AppendLine($"{indentation}public {Data.Vector.Name}({ConstantVectorTexts.Lower.Scalar(Data.Dimension)}, {Data.Unit.Name} {UnitParameterName})");
            
            if (Data.Unit.IsReferenceType)
            {
                Builder.AppendLine($"{indentation.Increased}: this(ComputeRepresentedMagnitude({ConstantVectorTexts.Lower.Name(Data.Dimension)})) {{ }}");
            }
            else
            {
                Builder.AppendLine($"{indentation.Increased}: this({Texts.Lower.ScalarMultiplyUnit}) {{ }}");
            }

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.VectorAndUnitConstructor());
            Builder.AppendLine($"{indentation}public {Data.Vector.Name}(Vector{Data.Dimension} components, {Data.Unit.Name} {UnitParameterName})");
            Builder.AppendLine($"{indentation.Increased}: this({ConstantVectorTexts.Upper.ComponentsAccess(Data.Dimension)}, {UnitParameterName}) {{ }}");
        }

        private void ComposeToString(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.ToStringDocumentation());
            Builder.Append($@"{indentation}public override string ToString() => $""{{typeof({Data.Vector.Name})}}: [{{");

            if (Data.DefaultUnitName is not null)
            {
                Builder.Append($"InUnit({Data.Unit.Name}.{Data.DefaultUnitName}).Value");
            }
            else
            {
                Builder.Append("Magnitude.Value");
            }

            Builder.Append('}');

            if (Data.DefaultUnitSymbol is not null)
            {
                Builder.Append($" [{Data.DefaultUnitSymbol}]");
            }

            Builder.Append($"\"{Environment.NewLine}");
        }

        private void ComposeDeconstruct(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.Deconstruct());
            Builder.AppendLine($"{indentation}public void Deconstruct({Texts.DeconstructHeader})");
            BlockBuilding.AppendBlock(Builder, composeBlock, indentation);

            void composeBlock(Indentation indentation)
            {
                for (int i = 0; i < Data.Dimension; i++)
                {
                    Builder.AppendLine($"{indentation}{Texts.Lower.ComponentName(i)} = {Texts.Upper.ComponentName(i)};");
                }
            }
        }

        private string ComposeInUnitComputation()
        {
            StringBuilder source = new();

            IterativeBuilding.AppendEnumerable(source, Data.Scalar is null ? scalarComponent() : typeComponent(), ", ");

            return source.ToString();

            IEnumerable<string> scalarComponent()
            {
                for (int i = 0; i < Data.Dimension; i++)
                {
                    yield return $"{VectorTextBuilder.GetUpperCasedComponentName(i, Data.Dimension)}.Value / " +
                        $"{UnitParameterName}.{Data.UnitQuantity.Name}.Magnitude.Value";
                }
            }

            IEnumerable<string> typeComponent()
            {
                for (int i = 0; i < Data.Dimension; i++)
                {
                    yield return $"{VectorTextBuilder.GetUpperCasedComponentName(i, Data.Dimension)}.Magnitude.Value / " +
                        $"{UnitParameterName}.{Data.UnitQuantity.Name}.Magnitude.Value";
                }
            }
        }

        private void ComposeEquality(Indentation indentation)
        {
            if (Data.Vector.IsReferenceType)
            {
                ComposeReferenceTypeEquality(indentation);
            }
            else
            {
                ComposeValueTypeEquality(indentation);
            }
        }

        private void ComposeReferenceTypeEquality(Indentation indentation)
        {
            string virtualText = Data.Vector.IsSealed ? " virtual" : string.Empty;

            AppendDocumentation(indentation, Data.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($"""
                {indentation}public{virtualText} bool Equals({Data.Vector.Name}? other)
                {indentation.Increased}=> other is not null && {ConstructEqualityChecksText()};
                """);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Vector.Name);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator ==({Data.Vector.Name}? lhs, {Data.Vector.Name}? rhs) => lhs?.Equals(rhs) ?? rhs is null;");

            AppendDocumentation(indentation, Data.Documentation.InequalitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator !=({Data.Vector.Name}? lhs, {Data.Vector.Name}? rhs) => (lhs == rhs) is false;");
        }

        private void ComposeValueTypeEquality(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.EqualsSameTypeMethod());
            Builder.AppendLine($"{indentation}public bool Equals({Data.Vector.Name} other)");
            Builder.AppendLine($"{indentation.Increased}=> {ConstructEqualityChecksText()};");

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualsObjectMethod());
            StaticBuilding.AppendEqualsObjectMethod(Builder, indentation, Data.Vector.Name);

            Builder.AppendLine();

            AppendDocumentation(indentation, Data.Documentation.EqualitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator ==({Data.Vector.Name} lhs, {Data.Vector.Name} rhs) => lhs.Equals(rhs);");

            AppendDocumentation(indentation, Data.Documentation.InequalitySameTypeOperator());
            Builder.AppendLine($"{indentation}public static bool operator !=({Data.Vector.Name} lhs, {Data.Vector.Name} rhs) => (lhs == rhs) is false;");
        }

        private string ConstructEqualityChecksText()
        {
            StringBuilder equalityChecksText = new();
            IterativeBuilding.AppendEnumerable(equalityChecksText, equalityChecks(), " && ");
            return equalityChecksText.ToString();

            IEnumerable<string> equalityChecks()
            {
                if (Data.Scalar is null)
                {
                    for (int i = 0; i < Data.Dimension; i++)
                    {
                        yield return $"{Texts.Upper.ComponentName(i)}.Value == other.{Texts.Upper.ComponentName(i)}.Value";
                    }
                }
                else
                {
                    for (int i = 0; i < Data.Dimension; i++)
                    {
                        yield return $"{Texts.Upper.ComponentName(i)}.Magnitude.Value == other.{Texts.Upper.ComponentName(i)}.Magnitude.Value";
                    }
                }
            }
        }

        private void ComposeGetHashCode(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.GetHashCodeDocumentation());
            Builder.AppendLine($"{indentation}public override int GetHashCode()");
            Builder.AppendLine($"{indentation.Increased}=> ({ConstantVectorTexts.Upper.Name(Data.Dimension)}).GetHashCode();");
        }

        private void ComposeCastFromComponents(Indentation indentation)
        {
            AppendDocumentation(indentation, Data.Documentation.CastFromComponents());
            Builder.Append($$"""
                [SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achieved through a constructor")]
                public static implicit operator {{Data.Vector.Name}}(({{Texts.Upper.Component}}) components)
                """);

            if (Data.Scalar!.Value.IsReferenceType)
            {
                Builder.AppendLine($$"""{{indentation}}{""");

                for (int i = 0; i < Data.Dimension; i++)
                {
                    Builder.AppendLine($"{indentation.Increased}ArgumentNullException.ThrowIfNull(components.{Texts.Upper.ComponentName(i)});");
                }

                Builder.AppendLine();
                Builder.AppendLine($"{indentation.Increased}return new({ConstantVectorTexts.Upper.ComponentsAccess(Data.Dimension)});");
                Builder.AppendLine($$"""{{indentation}}}""");
            }
            else
            {
                Builder.AppendLine($" => new({ConstantVectorTexts.Upper.ComponentsAccess(Data.Dimension)});");
            }
        }

        private void ComposeComputationUtilityMethod(Indentation indentation)
        {
            Builder.AppendLine($$"""
                {{indentation}}/// <summary>Computes the magnitudes of the represented components based on magnitudes expressed in
                {{indentation}}/// a certain unit <paramref name="{{Data.UnitParameterName}}"/>.</summary>
                """);

            for (int i = 0; i < Data.Dimension; i++)
            {
                Builder.AppendLine($"""{indentation}/// <param name="{Texts.Lower.ComponentName(i)}">The magnitude of the {Texts.Upper.ComponentName(i)}-component, expressed in a certain unit <paramref name="{Data.UnitParameterName}"/>.</param>""");
            }

            Builder.AppendLine($$"""
                {{indentation}}/// <param name="{{Data.UnitParameterName}}">The {{Data.Unit.Name}} in which the magnitudes of the components are expressed.</param>
                {{indentation}}/// <exception cref="ArgumentNullException"/>
                {{indentation}}private static Scalar ComputeRepresentedMagnitude({{ConstantVectorTexts.Lower.Scalar(Data.Dimension)}}, {{Data.Unit.Name}} {{UnitParameterName}})
                {{indentation}}{
                """);

            for (int i = 0; i < Data.Dimension; i++)
            {
                Builder.AppendLine($"""{indentation.Increased}ArgumentNullException.ThrowIfNull({Texts.Lower.ComponentName(i)});""");
            }

            Builder.AppendLine($$"""
                
                {{indentation.Increased}}ArgumentNullException.ThrowIfNull({{UnitParameterName}});

                {{indentation.Increased}}return {{Texts.Lower.ScalarMultiplyUnit}};
                {{indentation}}}
                """);
        }

        private void AppendDocumentation(Indentation indentation, string text)
        {
            DocumentationBuilding.AppendDocumentation(Builder, indentation, text);
        }
    }

    private class SpecificTexts
    {
        public UpperTexts Upper { get; }
        public LowerTexts Lower { get; }

        public string DeconstructHeader => DeconstructHeaderBuilder.GetText(Dimension);

        private int Dimension { get; }

        private VectorTextBuilder DeconstructHeaderBuilder { get; }

        public SpecificTexts(int dimension, NamedType? scalar, NamedType unitQuantity, string unitParameterName)
        {
            Dimension = dimension;

            Upper = new(dimension, scalar);
            Lower = new(dimension, scalar, unitQuantity, unitParameterName);

            DeconstructHeaderBuilder = scalar is null
                ? ConstantVectorTexts.Builders.DeconstructScalarHeader
                : CommonTextBuilders.DeconstructComponents(scalar.Value.Name);
        }

        public class UpperTexts
        {
            public string ComponentName(int index) => VectorTextBuilder.GetUpperCasedComponentName(index, Dimension);

            public string Component => ComponentBuilder.GetText(Dimension);

            public int Dimension { get; }

            private VectorTextBuilder ComponentBuilder { get; }

            public UpperTexts(int dimension, NamedType? scalar)
            {
                Dimension = dimension;

                ComponentBuilder = scalar is null
                    ? ConstantVectorTexts.Builders.Upper.Scalar
                    : CommonTextBuilders.Upper.Component(scalar.Value.Name);
            }
        }

        public class LowerTexts
        {
            public string ComponentName(int index) => VectorTextBuilder.GetLowerCasedComponentName(index, Dimension);

            public string Component => ComponentBuilder.GetText(Dimension);
            public string NewComponent => NewComponentBuilder.GetText(Dimension);
            public string ScalarMultiplyUnit => ScalarMultiplyUnitBuilder.GetText(Dimension);

            private int Dimension { get; }

            private VectorTextBuilder ComponentBuilder { get; }
            private VectorTextBuilder NewComponentBuilder { get; }
            private VectorTextBuilder ScalarMultiplyUnitBuilder { get; }

            public LowerTexts(int dimension, NamedType? scalar, NamedType unitQuantity, string unitParameterName)
            {
                Dimension = dimension;

                ComponentBuilder = scalar is null
                    ? ConstantVectorTexts.Builders.Lower.Scalar
                    : CommonTextBuilders.Lower.Component(scalar.Value.Name);

                NewComponentBuilder = scalar is null
                    ? ConstantVectorTexts.Builders.Lower.NewScalar
                    : CommonTextBuilders.Lower.NewComponent(scalar.Value.Name);

                ScalarMultiplyUnitBuilder = CommonTextBuilders.Lower.ScalarMultiplyUnit(unitParameterName, unitQuantity.Name);
            }
        }
    }
}
