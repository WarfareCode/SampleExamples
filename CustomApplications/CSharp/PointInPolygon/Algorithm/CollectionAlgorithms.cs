using System;
using System.Collections.Generic;

namespace PointInPolygon
{
    public static class CollectionAlgorithms
    {
        public static int EnumerableCount<T>(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }

            IList<T> list = enumerable as IList<T>;

            if (list != null)
            {
                return list.Count;
            }

            int count = 0;
            foreach (T t in enumerable)
            {
                ++count;
            }

            return count;
        }

        public static bool EnumerableCountGreaterThanOrEqual<T>(IEnumerable<T> enumerable, int minimumCount)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }

            if (minimumCount < 0)
            {
                throw new ArgumentOutOfRangeException("minimumCount");
            }

            IList<T> list = enumerable as IList<T>;

            if (list != null)
            {
                return list.Count >= minimumCount;
            }

            int count = 0;
            foreach (T t in enumerable)
            {
                if (++count >= minimumCount)
                {
                    return true;
                }
            }

            return false;
        }

        public static IList<T> EnumerableToList<T>(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException("enumerable");
            }

            IList<T> list = enumerable as IList<T>;

            if (list != null)
            {
                return list;
            }
            else
            {
                int count = EnumerableCount(enumerable);
                IList<T> newList = new List<T>(count);

                foreach (T t in enumerable)
                {
                    newList.Add(t);
                }

                return newList;
            }
        }

        public static IList<Vector3D> ArrayOfVectorsToList(Array array)
        {
            IList<Vector3D> list = new List<Vector3D>(array.Length / 3);
            for (int i = 0; i < array.Length; i += 3)
            {
                list.Add(
                    new Vector3D(
                        (double)array.GetValue(i),
                        (double)array.GetValue(i + 1),
                        (double)array.GetValue(i + 2)));
            }
            return list;
        }
    }
}