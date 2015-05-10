using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZ.solution.secondaryProblem
{
    public class Tree
    {
        public SortedDictionary<Geometry.Segment, Node> Nodes =
            new SortedDictionary<Geometry.Segment, Node>(new SortedSegmentsComparer());

        public Geometry.Segment GetPrevious(Geometry.Segment s)
        {
            Geometry.Segment segment;
            try
            {
                segment = FindFirstLowerThan(Nodes, s);
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
            return segment;
        }

        public Geometry.Segment GetNext(Geometry.Segment s)
        {
            Geometry.Segment segment;
            try
            {
                segment = FindFirstGreaterThan(Nodes, s);
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
            return segment;
        }

        private static int BinarySearch(IList<Geometry.Segment> list, Geometry.Segment value, bool greater)
        {
            if (list == null)
                throw new ArgumentNullException("list");
            var comp = new SortedSegmentsComparer();
            int lo = 0, hi = list.Count - 1;
            while (lo < hi)
            {
                int m = (hi + lo) / 2;  // this might overflow; be careful.
                int comparisonResut = comp.Compare(list[m], value);
                if (comparisonResut < 0)
                    lo = m + 1;
                else 
                    hi = m - 1;
            }
            if (greater)
            {
                if (comp.Compare(list[lo], value) <= 0)
                    lo++;
            }
            else
            {
                if (comp.Compare(list[lo], value) <= 0)
                    lo--;
            }
            return lo;
        }

        public static Geometry.Segment FindFirstGreaterThan(IDictionary<Geometry.Segment, Node> sortedDictionary, Geometry.Segment key)
        {
            var keys = new List<Geometry.Segment>(sortedDictionary.Keys);
            var index = BinarySearch(keys, key, true);
            return keys[index];
        }
        public static Geometry.Segment FindFirstLowerThan(IDictionary<Geometry.Segment, Node> sortedDictionary, Geometry.Segment key)
        {
            var keys = new List<Geometry.Segment>(sortedDictionary.Keys);
            var index = BinarySearch(keys, key, false);
            return keys[index];
        }
    }

    public class SortedSegmentsComparer : IComparer<Geometry.Segment>
    {
        public int Compare(Geometry.Segment s1, Geometry.Segment s2)
        {
            return s1.ps.y.CompareTo(s2.ps.y);
        }
    }
}
