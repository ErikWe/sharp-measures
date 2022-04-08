namespace ErikWe.SharpMeasures.SourceGenerators.Units.Attributes;

using ErikWe.SharpMeasures.SourceGenerators.Utility;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

internal readonly record struct ScaledUnitInstanceAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant, string From, double Scale)
    : IDependantAttribute, IAttributeParameters
{
    string IDependantAttribute.DependantOn => From;

    private static Dictionary<string, AttributeProperty<ScaledUnitInstanceAttributeParameters>> ConstructorArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<ScaledUnitInstanceAttributeParameters>> NamedArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<ScaledUnitInstanceAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            From,
            Scale
        };

        public static AttributeProperty<ScaledUnitInstanceAttributeParameters> Name { get; }
            = AttributeParameters.Name<ScaledUnitInstanceAttributeParameters>(static (parameters, name) => parameters with { Name = name });
        public static AttributeProperty<ScaledUnitInstanceAttributeParameters> Plural { get; }
            = AttributeParameters.Plural<ScaledUnitInstanceAttributeParameters>(static (parameters, plural) => parameters with { Plural = plural });
        public static AttributeProperty<ScaledUnitInstanceAttributeParameters> Symbol { get; }
            = AttributeParameters.Symbol<ScaledUnitInstanceAttributeParameters>(static (parameters, symbol) => parameters with { Symbol = symbol });
        public static AttributeProperty<ScaledUnitInstanceAttributeParameters> IsSIUnit { get; }
            = AttributeParameters.IsSIUnit<ScaledUnitInstanceAttributeParameters>(static (parameters, isSIUnit) => parameters with { IsSIUnit = isSIUnit });
        public static AttributeProperty<ScaledUnitInstanceAttributeParameters> IsConstant { get; }
            = AttributeParameters.IsConstant<ScaledUnitInstanceAttributeParameters>(static (parameters, isConstant) => parameters with { IsConstant = isConstant });
        public static AttributeProperty<ScaledUnitInstanceAttributeParameters> From { get; } = new("From", typeof(string),
            static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters);
        public static AttributeProperty<ScaledUnitInstanceAttributeParameters> Scale { get; } = new("Scale", typeof(double),
            static (parameters, obj) => obj is double scale ? parameters with { Scale = scale } : parameters);
    }

    public static ScaledUnitInstanceAttributeParameters? Parse(AttributeData attributeData)
    {
        ScaledUnitInstanceAttributeParameters values = new(string.Empty, string.Empty, string.Empty, false, false, string.Empty, 0);

        (bool success, values) = AttributeHelpers.ParseAttributeParameters(attributeData, values, ConstructorArguments, NamedArguments);

        return success ? values : null;
    }
}