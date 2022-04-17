namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Tests.Utility;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public Task NotPartial()
    {
        string source = @"
using SharpMeasures.Generators;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public class UnitOfLength { }";

        return VerifyGenerator.FromRawText<UnitGenerator>(source);
    }

    [Fact]
    public Task ArgumentNotScalarQuantity()
    {
        string source = @"
using SharpMeasures.Generators;

public partial class Length { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }";

        return VerifyGenerator.FromRawText<UnitGenerator>(source);
    }

    [Fact]
    public Task ArgumentNotType()
    {
        string source = @"
using SharpMeasures.Generators;

[GeneratedUnit(3)]
public partial class UnitOfLength { }";

        return VerifyGenerator.FromRawText<UnitGenerator>(source);
    }

    [Fact]
    public Task MissingArgument()
    {
        string source = @"
using SharpMeasures.Generators;

[GeneratedUnit]
public partial class UnitOfLength { }";

        return VerifyGenerator.FromRawText<UnitGenerator>(source);
    }

    [Fact]
    public Task InvalidTypeName()
    {
        string source = @"
using SharpMeasures.Generators;

[
[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public partial class struct { }";

        return VerifyGenerator.FromRawText<UnitGenerator>(source);
    }
}
