using System;

namespace flatsim
{
    /*
 * N: sort of neg y
 * W: sort of neg x
 * E: sort of pos x
 * S: sort of pos y
 *
 * Facing
 * NORTHWEST
 * 
 *   N /\ \
 *  / /  \ \
 * / /\NW/\ E
 *  /SW\/NE\
 *  \  /\  /
 * W \/SE\/ /
 *  \ \  / /
 *   \ \/ S
 *   
 * Facing
 * SOUTHWEST
 *
 *   W /\ \
 *  / /  \ \
 * / /\SW/\ N
 *  /SE\/NW\
 *  \  /\  /
 * S \/NE\/ /
 *  \ \  / /
 *   \ \/ E
 *   
 * Facing
 * SOUTHEAST
 *
 *   S /\ \
 *  / /  \ \
 * / /\SE/\ W
 *  /NE\/SW\
 *  \  /\  /
 * E \/NW\/ /
 *  \ \  / /
 *   \ \/ N
 *   
 * Facing
 * NORTHEAST
 *
 *   E /\ \
 *  / /  \ \
 * / /\NE/\ S
 *  /NW\/SE\
 *  \  /\  /
 * N \/SW\/ /
 *  \ \  / /
 *   \ \/ W
 */
    public enum Direction
    {
        NORTH, SOUTH, WEST, EAST,
        WESTNORTH, SOUTHWEST, EASTSOUTH, NORTHEAST
    }
}