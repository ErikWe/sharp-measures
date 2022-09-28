namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

internal sealed class DefaultGroupDocumentation : IGroupDocumentationStrategy, IEquatable<DefaultGroupDocumentation>
{
    private DefinedType Type { get; }
    private NamedType Unit { get; }

    private NamedType? Scalar { get; }

    private IReadOnlyDictionary<int, NamedType> MembersByDimension { get; }

    private string UnitParameterName { get; }

    private IUnitInstance? ExampleScalarUnitBaseInstance { get; }

    private bool HasComponent => Scalar is not null;

    public DefaultGroupDocumentation(GroupDataModel model)
    {
        Type = model.Group.Type;
        Unit = model.Group.Unit;

        Scalar = model.Group.Scalar;

        MembersByDimension = model.Group.MembersByDimension;

        UnitParameterName = SourceBuildingUtility.ToParameterName(Unit.Name);

        ExampleScalarUnitBaseInstance = GetExampleScalarBase(model);
    }

    private static IUnitInstance? GetExampleScalarBase(GroupDataModel model)
    {
        if (model.Group.Scalar is not null && model.ScalarPopulation.Scalars.TryGetValue(model.Group.Scalar.Value, out var scalar) && model.UnitPopulation.Units.TryGetValue(scalar.Unit, out var scalarUnit))
        {
            foreach (var includedBaseName in scalar.IncludedUnitBaseInstanceNames)
            {
                if (scalarUnit.UnitInstancesByName.TryGetValue(includedBaseName, out var includedBase))
                {
                    return includedBase;
                }
            }
        }

        return null;
    }

    public string Header() => HasComponent switch
    {
        true => ComponentedHeader(),
        false => UncomponentedHeader()
    };

    private string ComponentedHeader() => $"""
        /// <summary>Root of a group of vector quantities, each composed of {ScalarReference} and expressed in {UnitReference}.</summary>
        """;

    private string UncomponentedHeader() => $"""
        /// <summary>Root of a group of vector quantities, each expressed in {UnitReference}.</summary>
        """;

    public string ScalarFactoryMethod(int dimension)
    {
        var memberReference = MemberReference(dimension);

        var commonText = $$"""
            /// <summary>Constructs a new {{memberReference}} representing { {{Texts.ParameterTupleBuilder.GetText(dimension)}} }, expressed in an arbitrary unit.</summary>
            {{Texts.GetScalarFactoryMethodBuilder(memberReference, UnitParameterName).GetText(dimension)}}
            /// <param name="{{UnitParameterName}}">The {{UnitReference}} in which the magnitudes of the components are expressed.</param>
            """;

        if (Scalar is not null && ExampleScalarUnitBaseInstance is not null)
        {
            commonText = $"""
                {commonText}
                /// <remarks>A {memberReference} may also be constructed as demonstrated below.
                /// <code>{memberReference} x = ({ConstantVectorTexts.SampleValues(dimension)}) * <see cref="{Scalar.Value.FullyQualifiedName}.One{ExampleScalarUnitBaseInstance.Name}"/>;</code>
                /// </remarks>
                """;
        }

        return commonText;
    }

    public string VectorFactoryMethod(int dimension)
    {
        var memberReference = MemberReference(dimension);

        var commonText = $$"""
            /// <summary>Constructs a new {{memberReference}} representing { <paramref name="components"/> [<paramref name="{{UnitParameterName}}"/>] }.</summary>
            /// <param name="components">The magnitudes of the components of the constructed {{memberReference}}, expressed in <paramref name="{{UnitParameterName}}"/>.</param>
            /// <param name="{{UnitParameterName}}">The {{UnitReference}} in which <paramref name="components"/> is expressed.</param>
            """;

        if (Scalar is not null && ExampleScalarUnitBaseInstance is not null)
        {
            commonText = $"""
                {commonText}
                /// <remarks>A {memberReference} may also be constructed as demonstrated below.
                /// <code>{memberReference} x = ({ConstantVectorTexts.SampleValues(dimension)}) * <see cref="{Scalar.Value.FullyQualifiedName}.One{ExampleScalarUnitBaseInstance.Name}"/>;</code>
                /// </remarks>
                """;
        }

        return commonText;
    }

    public string ComponentsFactoryMethod(int dimension) => $$"""
        /// <summary>Constructs a new {{MemberReference(dimension)}} representing { {{Texts.ParameterTupleBuilder.GetText(dimension)}} }.</summary>
        {{Texts.GetComponentsConstructorBuilder(MemberReference(dimension)).GetText(dimension)}}
        """;

    private string UnitReference => $"""<see cref="{Unit.FullyQualifiedName}"/>""";
    private string ScalarReference => $"""<see cref="{Scalar?.FullyQualifiedName}"/>""";
    private string MemberReference(int dimension) => $"""<see cref="{MembersByDimension[dimension].FullyQualifiedName}"/>""";

    private static class Texts
    {
        public static VectorTextBuilder ParameterTupleBuilder { get; } = GetParameterTupleBuilder();

        public static VectorTextBuilder GetScalarFactoryMethodBuilder(string memberReference, string unitParameterName)
        {
            return new VectorTextBuilder(scalarsAndUnitConstructorComponent, Environment.NewLine);

            string scalarsAndUnitConstructorComponent(int componentIndex, int dimension)
            {
                string componentLowerCase = VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);
                string componentUpperCase = VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension);

                return $"""/// <param name="{componentLowerCase}">The magnitude of the {componentUpperCase}-component of the constructed {memberReference}, expressed in <paramref name="{unitParameterName}"/>.</param>""";
            }
        }

        public static VectorTextBuilder GetComponentsConstructorBuilder(string memberReference)
        {
            return new VectorTextBuilder(componentsConstructorComponent, Environment.NewLine);

            string componentsConstructorComponent(int componentIndex, int dimension)
            {
                string componentLowerCase = VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);
                string componentUpperCase = VectorTextBuilder.GetUpperCasedComponentName(componentIndex, dimension);

                return $"""/// <param name="{componentLowerCase}">The {componentUpperCase}-component of the constructed {memberReference}.</param>""";
            }
        }

        private static VectorTextBuilder GetParameterTupleBuilder()
        {
            return new VectorTextBuilder(parameterTupleComponent, ", ");

            static string parameterTupleComponent(int componentIndex, int dimension)
            {
                string componentName = VectorTextBuilder.GetLowerCasedComponentName(componentIndex, dimension);

                return $"""<paramref name="{componentName}"/>""";
            }
        }
    }

    public bool Equals(DefaultGroupDocumentation? other) => other is not null && other.Type == Type && other.Unit == Unit && other.Scalar == Scalar && other.MembersByDimension == MembersByDimension;
    public override bool Equals(object? obj) => obj is DefaultGroupDocumentation other && Equals(other);
    
    public static bool operator ==(DefaultGroupDocumentation? lhs, DefaultGroupDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DefaultGroupDocumentation? lhs, DefaultGroupDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (Type, Unit, Scalar, MembersByDimension).GetHashCode();
}
