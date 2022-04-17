namespace SharpMeasures.Generators.Tests;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Tests.Utility;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ScalarQuantityGeneratorTests
{
    [Fact]
    public Task ScalarQuantityGenerator_ShouldBeExactCode()
    {
        string source = @"
using SharpMeasures.Generators;

namespace Foo
{
    namespace Bar
    {
        [GeneratedScalarQuantity]
        public readonly partial record struct Time() { }
    }
}";

        return VerifyGenerator.FromRawText<ScalarQuantityGenerator>(source);
    }
}