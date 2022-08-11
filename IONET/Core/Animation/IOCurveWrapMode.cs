using System;
using System.Collections.Generic;
using System.Text;

namespace IONET.Core.Animation
{
    /// <summary>
    /// Represents a wrap mode to determine how a curve behaves at the start/end key frames.
    /// </summary>
    public enum IOCurveWrapMode
    {
        /// <summary>
        /// Stops at first/last key value and stays
        /// </summary>
        Constant,
        /// <summary>
        /// Moves to the direction of the first/last key frame based on tangent angle
        /// </summary>
        Linear,
        /// <summary>
        /// Repeats back to first/last key frame
        /// </summary>
        Cycle,
        /// <summary>
        /// Repeats but starts relative to the first/last key frame
        /// </summary>
        CycleRelative,
        /// <summary>
        /// Repeats by reversing its values
        /// </summary>
        Oscillate,
    }
}
