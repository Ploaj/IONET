using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IONET
{
    public class IOKeyFrame
    {
        /// <summary>
        /// 
        /// </summary>
        public float Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Frame { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object Value { get; set; }
    }


    public class IOKeyFrameCubic : IOKeyFrame
    {
        /// <summary>
        /// 
        /// </summary>
        public float TangentInput { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float TangentOutput { get; set; }
    }

    public class IOKeyFrameHermite : IOKeyFrame
    {
        /// <summary>
        /// 
        /// </summary>
        public float TangentInput { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float TangentOutput { get; set; }
    }

    public class IOKeyFrameBezier : IOKeyFrame
    {
        /// <summary>
        /// 
        /// </summary>
        public float TangentInputX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float TangentInputY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float TangentOutputX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float TangentOutputY { get; set; }
    }
}
