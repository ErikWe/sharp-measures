namespace SharpMeasures.Tests;

using System;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed record class ReferenceVector4Quantity : IVector4Quantity<ReferenceVector4Quantity>
{
    Scalar IVector4Quantity.X => throw new NotImplementedException();
    Scalar IVector4Quantity.Y => throw new NotImplementedException();
    Scalar IVector4Quantity.Z => throw new NotImplementedException();
    Scalar IVector4Quantity.W => throw new NotImplementedException();

    Vector4 IVector4Quantity.Components => throw new NotImplementedException();

    static ReferenceVector4Quantity IVector4Quantity<ReferenceVector4Quantity>.WithComponents(Scalar x, Scalar y, Scalar z, Scalar w) => throw new NotImplementedException();
    static ReferenceVector4Quantity IVector4Quantity<ReferenceVector4Quantity>.WithComponents(Vector4 components) => throw new NotImplementedException();

    Scalar IVectorQuantity.Magnitude() => throw new NotImplementedException();
    Scalar IVectorQuantity.SquaredMagnitude() => throw new NotImplementedException();
}
