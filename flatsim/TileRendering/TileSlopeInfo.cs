using System;

namespace flatsim
{
    public interface TileSlopeInfo
    {
        int getSlopeTypeId();
        float getRelativeHeight(float relativeTileX, float relativeTileY);
    }
}