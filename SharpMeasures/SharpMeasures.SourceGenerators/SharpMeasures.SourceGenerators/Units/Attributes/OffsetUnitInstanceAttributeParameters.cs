namespace ErikWe.SharpMeasures.SourceGenerators.Units.Attributes;

using ErikWe.SharpMeasures.SourceGenerators.Utility;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

internal readonly record struct OffsetUnitInstanceAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant, string From, double Offset)
    : IDependantAttribute, IAttributeParameters
{
    string IDependantAttribute.DependantOn => From;

    private static Dictionary<string, AttributeProperty<OffsetUnitInstanceAttributeParameters>> ConstructorArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<OffsetUnitInstanceAttributeParameters>> NamedArguments { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<OffsetUnitInstanceAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            From,
            Offset
        };

        public static AttributeProperty<OffsetUnitInstanceAttributeParameters> Name { get; }
            = AttributeParameters.Name<OffsetUnitInstanceAttributeParameters>(static (parameters, name) => parameters with { Name = name });
        public static AttributeProperty<OffsetUnitInstanceAttributeParameters> Plural { get; }
            = AttributeParameters.Plural<OffsetUnitInstanceAttributeParameters>(static (parameters, plural) => parameters with { Plural = plural });
        public static AttributeProperty<OffsetUnitInstanceAttributeParameters> Symbol { get; }
            = AttributeParameters.Symbol<OffsetUnitInstanceAttributeParameters>(static (parameters, symbol) => parameters with { Symbol = symbol });
        public static AttributeProperty<OffsetUnitInstanceAttributeParameters> IsSIUnit { get; }
            = AttributeParameters.IsSIUnit<OffsetUnitInstanceAttributeParameters>(static (parameters, isSIUnit) => parameters with { IsSIUnit = isSIUnit });
        public static AttributeProperty<OffsetUnitInstanceAttributeParameters> IsConstant { get; }
            = AttributeParameters.IsConstant<OffsetUnitInstanceAttributeParameters>(static (parameters, isConstant) => parameters with { IsConstant = isConstant });
        public static AttributeProperty<OffsetUnitInstanceAttributeParameters> From { get; } = new("From", typeof(string),
            static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters);
        public static AttributeProperty<OffsetUnitInstanceAttributeParameters> Offset { get; } = new("Offset", typeof(double),
            static (parameters, obj) => obj is double offset ? parameters with { Offset = offset } : parameters);
    }

    public static OffsetUnitInstanceAttributeParameters? Parse(AttributeData attributeData)
    {
        OffsetUnitInstanceAttributeParameters values = new(string.Empty, string.Empty, string.Empty, false, false, string.Empty, 0);

        (bool success, values) = AttributeHelpers.ParseAttributeParameters(attributeData, values, ConstructorArguments, NamedArguments);

        return success ? values : null;
    }
}