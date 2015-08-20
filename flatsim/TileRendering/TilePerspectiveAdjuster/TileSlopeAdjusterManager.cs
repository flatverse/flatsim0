using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace flatsim
{
    public class TileSlopeAdjusterManager : TilePerspectiveAdjuster
    {
        public float priority = 0.1f;

        private Dictionary<string, TilePerspectiveAdjuster> adjusters;

        public TileSlopeAdjusterManager()
        {
            adjusters = new Dictionary<string, TilePerspectiveAdjuster>();
        }

        public virtual void init(TilePerspective perspective)
        {
            foreach (KeyValuePair<string, TilePerspectiveAdjuster> adjuster in adjusters)
            {
                adjuster.Value.init(perspective);
            }
        }

        public virtual void add(string slope, TilePerspectiveAdjuster adjuster)
        {
            adjusters.Add(slope, adjuster);
        }

        public virtual void adjust(TileDrawInfo tileDrawInfo)
        {
            if (adjusters.ContainsKey(tileDrawInfo.slope))
            {
                adjusters[tileDrawInfo.slope].adjust(tileDrawInfo);
            }
        }

        public virtual float getPriority()
        {
            return priority;
        }
    }
}