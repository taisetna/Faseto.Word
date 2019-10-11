using System.Collections.Generic;

namespace Dna
{ 
    public static class ArrayExtensions
    {
        public static T[] Append<T>(this T[] source, params T[] toAdd)
        {
            // Create a list of the original items
            var list = new List<T>(source);

            // Append the new items
            list.AddRange(toAdd);

            // Return the new array
            return list.ToArray();
         }

        public static T[] Prepend<T>(this T[] source, params T[] toAdd)
        {
            // Create a list of the new items
            var list = new List<T>(source);

            // Append the source items
            list.AddRange(toAdd);

            // Return the new array
            return list.ToArray();
        }
    }
}
