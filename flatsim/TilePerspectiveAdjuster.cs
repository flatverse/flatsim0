using System;
using Microsoft.Xna.Framework;

namespace flatsim
{
    public interface TilePerspectiveAdjuster
    {
        void init(TilePerspective perspective);
        float getPriority();
        void adjust(TileDrawInfo toAdjust, int coordNS, int coordWE, float height, TilePart part, string slope);
    }
}