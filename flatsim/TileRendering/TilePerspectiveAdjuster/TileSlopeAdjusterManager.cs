using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace flatsim
{
    public class TileSlopeAdjusterManager : TilePerspectiveAdjuster
    {
        public float priority = 0.1f;

        private Dictionary<int, TilePerspectiveAdjuster> adjusters;

        public TileSlopeAdjusterManager()
        {
            adjusters = new Dictionary<int, TilePerspectiveAdjuster>();
        }

        public virtual void init(TilePerspective perspective)
        {
            foreach (KeyValuePair<int, TilePerspectiveAdjuster> adjuster in adjusters)
            {
                adjuster.Value.init(perspective);
            }
        }

        public virtual void add(int slopeTypeId, TilePerspectiveAdjuster adjuster)
        {
            adjusters.Add(slopeTypeId, adjuster);
        }

        public virtual void adjust(TileDrawInfo tileDrawInfo)
        {
            if (adjusters.ContainsKey(tileDrawInfo.slopeInfo.getSlopeTypeId()))
            {
                adjusters[tileDrawInfo.slopeInfo.getSlopeTypeId()].adjust(tileDrawInfo);
            }
        }

        public virtual float getPriority()
        {
            return priority;
        }
    }
}