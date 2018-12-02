using System;

namespace Algorithms {
    using static BinarySearchClass;

    public static class BinarySearchClass {
        /// <summary>
        /// 範囲内の整数のうち、条件を満たす最小の値を二分探索で探索します。
        /// </summary>
        /// <param name="predicate">満たすべき条件</param>
        /// <param name="first">範囲の最小の値</param>
        /// <param name="last">範囲の最大の値。この値が条件を満たすことはユーザーが保証してください。</param>
        /// <returns></returns>
        public static int BinarySearch(Func<int, bool> predicate, int first, int last) {
            if (predicate(first)) return first;
            if (last == first + 1) {
                if (predicate(last)) return last;
                throw new ArgumentException();
            }

            int mid = (first + last) / 2;
            return predicate(mid) ? BinarySearch(predicate, first, mid) : BinarySearch(predicate, mid, last);
        }

        /// <summary>
        /// 範囲内の実数のうち、条件を満たす最小の値を二分探索で探索します。
        /// </summary>
        /// <param name="predicate">満たすべき条件</param>
        /// <param name="first">範囲の最小の値</param>
        /// <param name="last">範囲の最大の値。この値が条件を満たすことユーザーが保証してください。</param>
        /// <param name="epsilon">first と last の差がこの値以下になったら探索を打ち切ります。</param>
        /// <returns></returns>
        public static double BinarySearch(Func<double, bool> predicate, double first, double last, double epsilon = 0.0000000001) {
            if (last - first < epsilon) {
                if (predicate(last)) return last;
                throw new ArgumentException();
            }
            double mid = (first + last) / 2;
            return predicate(mid) ? BinarySearch(predicate, first, mid) : BinarySearch(predicate, mid, last);
        }
    }
}
