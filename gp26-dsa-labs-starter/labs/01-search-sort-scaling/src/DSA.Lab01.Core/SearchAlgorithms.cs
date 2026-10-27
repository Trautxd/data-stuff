using System;

namespace DSA.Lab01
{
    public static class SearchAlgorithms
    {
        public static int LinearSearch(int[] values, int target)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            for (int i = 0; i < values.Length; ++i)
            {
                if (values[i] == target)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int BinarySearch(int[] sortedValues, int target)
        {

            if (sortedValues == null)
            {
                throw new ArgumentNullException(nameof(sortedValues));
            }

            int low = 0;
            int high = sortedValues.Length - 1;

            while (low <= high)
            {

                int mid = (low + high) / 2;

                if (sortedValues[mid] == target)
                {
                    return mid;
                }

                if (sortedValues[mid] < target)
                {
                    low = mid + 1;


                }
                else
                {
                    high = mid - 1;
                }
            }
           
            return -1;

            // TODO: Implement binary search.
            // The input to this method is guaranteed to be sorted in ascending order.
            //
            // Suggested plan:
            // 1. Keep track of a low and high index.
            // 2. Inspect the middle value.
            // 3. Return the index if it matches.
            // 4. Otherwise discard either the lower or upper half.
            // 5. Repeat while there is still a valid search range.
            
        }
    }
}
