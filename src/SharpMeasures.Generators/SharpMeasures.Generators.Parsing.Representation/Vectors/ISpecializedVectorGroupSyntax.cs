namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
public interface ISpecializedVectorGroupSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the original group, of which this group is a specialized form.</summary>
    public abstract Location Original { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should inherit the operations defined by the original quantity. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location InheritOperations { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should inherit the conversions defined by the original quantity. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location InheritConversions { get; }

    /// <summary>The <see cref="Location"/> of the argument for how the conversion operator from the original quantity to this quantity should be implemented. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ForwardsCastOperatorBehaviour { get; }

    /// <summary>The <see cref="Location"/> of the argument for how the conversion operator from this quantity to the original quantity should be implemented. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location BackwardsCastOperatorBehaviour { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should support addition of two instances. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ImplementSum { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the quantity should support subtraction of two instances. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ImplementDifference { get; }
}
