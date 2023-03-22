namespace SharpMeasures.Tests;

using System;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed record class ReferenceVector2Quantity : IVector2Quantity<ReferenceVector2Quantity>
{
    Scalar IVector2Quantity.X => throw new NotImplementedException();
    Scalar IVector2Quantity.Y => throw new NotImplementedException();

    Vector2 IVector2Quantity.Components => throw new NotImplementedException();

    static ReferenceVector2Quantity IVector2Quantity<ReferenceVector2Quantity>.WithComponents(Scalar x, Scalar y) => throw new NotImplementedException();
    static ReferenceVector2Quantity IVector2Quantity<ReferenceVector2Quantity>.WithComponents(Vector2 components) => throw new NotImplementedException();

    Scalar IVectorQuantity.Magnitude() => throw new NotImplementedException();
    Scalar IVectorQuantity.SquaredMagnitude() => throw new NotImplementedException();
}
