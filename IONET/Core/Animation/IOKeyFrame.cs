using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IONET.Core.Animation
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

    public class IOKeyFrameHermite : IOKeyFrame
    {
        /// <summary>
        /// 
        /// </summary>
        public float TangentSlopeInput { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float TangentSlopeOutput { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float TangentWeightInput { get; set; } = 1.0f;
        /// <summary>
        /// 
        /// </summary>
        public float TangentWeightOutput { get; set; } = 1.0f;

        /// <summary>
        /// 
        /// </summary>
        public bool IsWeighted => (TangentWeightInput != 1.0f || TangentWeightOutput != 1.0f);

        public static IOKeyFrameHermite FromBezier(IOKeyFrameBezier bezier)
        {
            return new IOKeyFrameHermite()
            {
                Frame = bezier.Frame,
                Value = bezier.Value,
            };
        }
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
