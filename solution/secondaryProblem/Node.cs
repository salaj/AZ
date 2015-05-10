using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZ.solution.secondaryProblem
{
    public class Node : IComparable<Node>
    {
        private Geometry.Segment segment;
        public Node Previous { get; set; }
        public Node Next { get; set; }
        public Node(Geometry.Segment s)
        {
            segment = s;
        }

        public Geometry.Segment GetSegment()
        {
            return segment;
        }

        public int CompareTo(Node other)
        {
            return segment.ps.y.CompareTo(other.segment.ps.y);
        }
    }
}
