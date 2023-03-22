namespace SharpMeasures.Tests;

using System;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "Used as test input.")]
internal sealed record class ReferenceVector3Quantity : IVector3Quantity<ReferenceVector3Quantity>
{
    Scalar IVector3Quantity.X => throw new NotImplementedException();
    Scalar IVector3Quantity.Y => throw new NotImplementedException();
    Scalar IVector3Quantity.Z => throw new NotImplementedException();

    Vector3 IVector3Quantity.Components => throw new NotImplementedException();

    static ReferenceVector3Quantity IVector3Quantity<ReferenceVector3Quantity>.WithComponents(Scalar x, Scalar y, Scalar z) => throw new NotImplementedException();
    static ReferenceVector3Quantity IVector3Quantity<ReferenceVector3Quantity>.WithComponents(Vector3 components) => throw new NotImplementedException();

    Scalar IVectorQuantity.Magnitude() => throw new NotImplementedException();
    Scalar IVectorQuantity.SquaredMagnitude() => throw new NotImplementedException();
}
