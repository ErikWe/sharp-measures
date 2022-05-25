namespace SharpMeasures;

using System;
using System.Collections;

public static partial class UtilityExtensions
{
    public static int GetSequenceHashCode(this IEnumerable enumerable)
    {
        if (enumerable is null)
        {
            throw new ArgumentNullException(nameof(enumerable));
        }

        unchecked
        {
            int hashCode = (int)2166136261;

            foreach (object? item in enumerable)
            {
                int itemHashCode = item?.GetHashCode() ?? 0;

                for (int i = 0; i < 4; i++)
                {
                    hashCode = (hashCode * 16777619) ^ (itemHashCode >> 8 * i);
                }
            }

            return hashCode;
        }
    }
}
