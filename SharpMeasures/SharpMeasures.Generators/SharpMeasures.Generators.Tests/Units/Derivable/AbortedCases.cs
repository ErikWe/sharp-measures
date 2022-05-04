namespace SharpMeasures.Generators.Tests.Units.Derivable;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public void ExpressionNull()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedScalarQuantity(typeof(UnitOfTime))]
public class Time { }

[GeneratedScalarQuantity(typeof(UnitOfSpeed))]
public class Speed { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public partial class UnitOfTime { }

[DerivableUnit(null, typeof(UnitOfLength), typeof(UnitOfTime))]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void TypeNotUnit()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedScalarQuantity(typeof(UnitOfTime))]
public class Time { }

[GeneratedScalarQuantity(typeof(UnitOfSpeed))]
public class Speed { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public partial class UnitOfTime { }

[DerivableUnit(""{{0}} / {{1}}"", typeof(Length), typeof(UnitOfTime))]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void NoUnitArguments()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedScalarQuantity(typeof(UnitOfTime))]
public class Time { }

[GeneratedScalarQuantity(typeof(UnitOfSpeed))]
public class Speed { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public partial class UnitOfTime { }

[DerivableUnit(string.Empty)]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void MoreUnitsThanExpressionExpected()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedScalarQuantity(typeof(UnitOfTime))]
public class Time { }

[GeneratedScalarQuantity(typeof(UnitOfSpeed))]
public class Speed { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public partial class UnitOfTime { }

[DerivableUnit(""{{0}}"", typeof(UnitOfLength), typeof(UnitOfTime))]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void FewerUnitsThanExpressionExpected()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedScalarQuantity(typeof(UnitOfTime))]
public class Time { }

[GeneratedScalarQuantity(typeof(UnitOfSpeed))]
public class Speed { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public partial class UnitOfTime { }

[DerivableUnit(""{{0}} / {{1}}"", typeof(UnitOfLength))]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void NullUnit()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedScalarQuantity(typeof(UnitOfTime))]
public class Time { }

[GeneratedScalarQuantity(typeof(UnitOfSpeed))]
public class Speed { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public partial class UnitOfTime { }

[DerivableUnit(""{{0}} / {{1}}"", null, typeof(UnitOfTime))]
[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.LengthTimeSpeed_NoDerivable);
    }
}