using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace flatsim
{
    public class TilePerspectiveAdjusterManager : TilePerspectiveAdjuster
    {
        public float priority = 1;

        private SortedList<float, List<TilePerspectiveAdjuster>> sortedList;

        public TilePerspectiveAdjusterManager()
        {
            sortedList = new SortedList<float, List<TilePerspectiveAdjuster>>(new Comparer());
        }

        public virtual void init(TilePerspective perspective)
        {
            foreach (KeyValuePair<float, List<TilePerspectiveAdjuster>> priorToAdjList in sortedList)
            {
                foreach (TilePerspectiveAdjuster adj in priorToAdjList.Value)
                {
                    adj.init(perspective);
                }
            }
        }

        public virtual void add(TilePerspectiveAdjuster adjuster) {
            float prior = adjuster.getPriority();
            List<TilePerspectiveAdjuster> list;
            if (!sortedList.TryGetValue(prior, out list))
            {
                list = new List<TilePerspectiveAdjuster>();
                list.Add(adjuster);
                sortedList.Add(prior, list);
            }
            else
            {
                list.Add(adjuster);
            }
        }

        public virtual void adjust(TileDrawInfo tileDrawInfo, int coordNS, int coordWE, float height, TilePart part, string slope)
        {
            foreach (KeyValuePair<float, List<TilePerspectiveAdjuster>> priorToAdjList in sortedList)
            {
                foreach (TilePerspectiveAdjuster adj in priorToAdjList.Value)
                {
                    adj.adjust(tileDrawInfo, coordNS, coordWE, height, part, slope);
                }
            }
        }

        public virtual float getPriority()
        {
            return priority;
        }

        /*
         * Comparer
         * 
         * I'm sure there's a built in implementation of this that could be used,
         * but I couldn't find it.
         */
        private class Comparer : IComparer<float>
        {
            public int Compare(float a, float b)
            {
                if (a < b)
                {
                    return -1;
                }
                else if (a == b)
                {
                    return 0;
                }
                else // a > b
                {
                    return 1;
                }
            }
        }
    }
}