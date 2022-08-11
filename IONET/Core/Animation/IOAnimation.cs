using System;
using System.Collections.Generic;
using System.Linq;

namespace IONET.Core.Animation
{
    public class IOAnimation
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<IOAnimation> Groups { get; internal set; } = new List<IOAnimation>();

        /// <summary>
        /// 
        /// </summary>
        public List<IOAnimationTrack> Tracks { get; internal set; } = new List<IOAnimationTrack>();

        /// <summary>
        /// 
        /// </summary>
        public float StartFrame { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float EndFrame { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float GetFrameCount()
        {
            float frameCount = 0;
            foreach (var group in Groups)
                frameCount = Math.Max(frameCount, group.GetFrameCount());
            foreach (var track in Tracks)
            {
                //Important to +1 as the frame is the currently played frame in a keyframe
                var frame = track.KeyFrames.Max(x => x.Frame) + 1;
                //Get largest key frame value
                frameCount = Math.Max(frameCount, frame);
            }
            return frameCount;
        }
    }
}
