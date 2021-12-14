using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IONET.Core.Animation
{
    public class IOAnimationTrack
    {
        /// <summary>
        /// 
        /// </summary>
        public IOAnimationTrackType ChannelType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IOCurveWrapMode PreWrap { get; set; } = IOCurveWrapMode.Constant;

        /// <summary>
        /// 
        /// </summary>
        public IOCurveWrapMode PostWrap { get; set; } = IOCurveWrapMode.Constant;

        /// <summary>
        /// 
        /// </summary>
        public List<IOKeyFrame> KeyFrames { get; internal set; } = new List<IOKeyFrame>();
    }
}
