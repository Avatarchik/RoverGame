////////////////////////////////////////////////////////////////////////////
//
//      Name:               HeightTool.cs
//      Author:             HOEKKII
//      
//      Description:        N/A
//      
////////////////////////////////////////////////////////////////////////////

using System;

namespace TerrainPainter
{
    [Serializable]
    public enum HeightTool
    {
        None = -1,
        Sculpt,
        Flatten,
        Pull,
        Erosion,
        Smooth,
        Clone,
        Path
    }
}
