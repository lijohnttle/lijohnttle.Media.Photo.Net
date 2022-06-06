using System;
using System.Collections.Generic;
using System.Linq;

namespace lijohnttle.Media.Photo.Filters.Internal.Helpers
{
    internal static class CollectionExtensions
    {
        public static T QuickSelect<T>(this IEnumerable<T> source, IComparer<T> comparer)
        {
            IList<T> sourceList = source.ToList();

            if (sourceList.Count == 1)
            {
                return sourceList[0];
            }

            int middleIndex = sourceList.Count / 2;

            return Partition(sourceList, 0, sourceList.Count - 1);


            T Partition(IList<T> list, int left, int right)
            {
                int pivotIndex = (left + right) / 2;
                T pivot = list[pivotIndex];

                int leftRunnerIndex = left;
                int rightRunnerIndex = right;

                while (leftRunnerIndex < rightRunnerIndex)
                {
                    RunFromLeft(list, pivot, ref leftRunnerIndex, rightRunnerIndex);
                    RunFromRight(list, pivot, ref rightRunnerIndex, leftRunnerIndex);

                    if (leftRunnerIndex == rightRunnerIndex)
                    {
                        break;
                    }

                    // swap
                    (list[rightRunnerIndex], list[leftRunnerIndex]) = (list[leftRunnerIndex], list[rightRunnerIndex]);

                    leftRunnerIndex++;
                    rightRunnerIndex--;
                }

                // now the list split into 2 parts at leftRunnerIndex
                if (middleIndex <= leftRunnerIndex)
                {
                    if (leftRunnerIndex - 1 - left == 0)
                    {
                        return list[middleIndex];
                    }

                    return Partition(list, left, leftRunnerIndex);
                }
                else
                {
                    if (right - leftRunnerIndex - 1 == 0)
                    {
                        return list[middleIndex];
                    }

                    return Partition(list, leftRunnerIndex, right);
                }
            }

            void RunFromLeft(IList<T> sourceList, T pivot, ref int index, int limitIndex)
            {
                while (index < limitIndex)
                {
                    var item = sourceList[index];

                    if (Compare(item, pivot) >= 0)
                    {
                        break;
                    }

                    index += 1;
                }
            }

            void RunFromRight(IList<T> sourceList, T pivot, ref int index, int limitIndex)
            {
                while (index > limitIndex)
                {
                    var item = sourceList[index];

                    if (Compare(item, pivot) < 0)
                    {
                        break;
                    }

                    index -= 1;
                }
            }

            int Compare(T item1, T item2)
            {
                if (comparer != null)
                {
                    return comparer.Compare(item1, item2);
                }

                if (item1 is IComparable<T> item1Comparable)
                {
                    return item1Comparable.CompareTo(item2);
                }

                throw new InvalidOperationException("Comparer is required.");
            }
        }
    }
}
