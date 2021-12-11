using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IONET
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
        public List<IOKeyFrame> KeyFrames { get; internal set; } = new List<IOKeyFrame>();
    }
}
