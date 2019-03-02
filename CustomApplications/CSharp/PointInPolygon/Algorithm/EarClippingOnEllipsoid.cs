﻿using System;
using System.Collections.Generic;

namespace PointInPolygon
{
    public static class EarClippingOnEllipsoid
    {
        public static IList<int> Triangulate(IEnumerable<Vector3D> positions)
        {
            if (positions == null)
            {
                throw new ArgumentNullException("positions");
            }

            //
            // Doubly linked list.  This would be a tad cleaner if it were also circular.
            //
            LinkedList<IndexedVector<Vector3D>> remainingPositions = new LinkedList<IndexedVector<Vector3D>>(); ;

            int index = 0;
            foreach (Vector3D position in positions)
            {
                remainingPositions.AddLast(new IndexedVector<Vector3D>(position, index++));
            }

            if (remainingPositions.Count < 3)
            {
                throw new ArgumentOutOfRangeException("positions", "At least three positions are required.");
            }

            IList<int> indices = new List<int>(3 * (remainingPositions.Count - 2));

            ///////////////////////////////////////////////////////////////////

            LinkedListNode<IndexedVector<Vector3D>> previousNode = remainingPositions.First;
            LinkedListNode<IndexedVector<Vector3D>> node = previousNode.Next;
            LinkedListNode<IndexedVector<Vector3D>> nextNode = node.Next;

            int bailCount = 2 * remainingPositions.Count * remainingPositions.Count;

            while (remainingPositions.Count > 3)
            {
                Vector3D p0 = previousNode.Value.Vector;
                Vector3D p1 = node.Value.Vector;
                Vector3D p2 = nextNode.Value.Vector;

                if (IsPossibleEar(p0, p1, p2))
                {
                    bool isEar = true;
                    for (LinkedListNode<IndexedVector<Vector3D>> n = ((nextNode.Next != null) ? nextNode.Next : remainingPositions.First);
                        n != previousNode;
                        n = ((n.Next != null) ? n.Next : remainingPositions.First))
                    {
                        if (ContainmentTests.PointInsideThreeSidedInfinitePyramid(n.Value.Vector, Vector3D.Zero, p0, p1, p2))
                        {
                            isEar = false;
                            break;
                        }
                    }

                    if (isEar)
                    {
                        indices.Add(previousNode.Value.Index);
                        indices.Add(node.Value.Index);
                        indices.Add(nextNode.Value.Index);
                        remainingPositions.Remove(node);

                        node = nextNode;
                        nextNode = (nextNode.Next != null) ? nextNode.Next : remainingPositions.First;
                        continue;
                    }
                }

                previousNode = (previousNode.Next != null) ? previousNode.Next : remainingPositions.First;
                node = (node.Next != null) ? node.Next : remainingPositions.First;
                nextNode = (nextNode.Next != null) ? nextNode.Next : remainingPositions.First;

                if (--bailCount == 0)
                {
                    break;
                }
            }

            LinkedListNode<IndexedVector<Vector3D>> n0 = remainingPositions.First;
            LinkedListNode<IndexedVector<Vector3D>> n1 = n0.Next;
            LinkedListNode<IndexedVector<Vector3D>> n2 = n1.Next;
            indices.Add(n0.Value.Index);
            indices.Add(n1.Value.Index);
            indices.Add(n2.Value.Index);

            return indices;
        }

        private static bool IsPossibleEar(Vector3D p0, Vector3D p1, Vector3D p2)
        {
            Vector3D u = p1 - p0;
            Vector3D v = p2 - p1;

            return u.Cross(v).Dot(p1) >= 0.0;
        }
    }
}