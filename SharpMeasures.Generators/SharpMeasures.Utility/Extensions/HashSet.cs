namespace SharpMeasures;

using System.Collections.Generic;
using System.Linq;

public static partial class UtilityExtensions
{
    public static int GetOrderIndependentHashCode<TKey>(this HashSet<TKey> hashSet) => hashSet.OrderBy(static (x) => x).GetSequenceHashCode();
}
