namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
public interface IQuantityOperationSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the quantity that is the result of the operation.</summary>
    public abstract Location Result { get; }

    /// <summary>The <see cref="Location"/> of the argument for the other quantity in the operation.</summary>
    public abstract Location Other { get; }

    /// <summary>The <see cref="Location"/> of the argument for the operator that is applied to the quantities.</summary>
    public abstract Location OperatorType { get; }

    /// <summary>The <see cref="Location"/> of the argument for the position of the implementing quantity in the operation. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location Position { get; }

    /// <summary>The <see cref="Location"/> of the argument for whether the mirrored operation is also implemented. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location MirrorMode { get; }

    /// <summary>The <see cref="Location"/> of the argument for how the operation is implemented. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location Implementation { get; }

    /// <summary>The <see cref="Location"/> of the argument for how the mirrored operation is implemented, if it is implemented. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location MirroredImplementation { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the instance method, if implemented. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location MethodName { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the static method, if implemented. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location StaticMethodName { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the mirrored instance method, if implemented. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location MirroredMethodName { get; }

    /// <summary>The <see cref="Location"/> of the argument for the name of the mirrored static method, if implemented. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location MirroredStaticMethodName { get; }
}
