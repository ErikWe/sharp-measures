namespace SharpMeasures.Generators.Tests;

using SharpMeasures.Generators.Scalars;

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
using SharpMeasures.Attributes;

namespace Foo
{
    namespace Bar
    {
        [ScalarQuantity]
        public readonly partial record struct Time() { }
    }
}";

        return Utility.VerifyGenerator<ScalarQuantityGenerator>(source);
    }
}