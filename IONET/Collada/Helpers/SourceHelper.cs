using IONET.Collada.Core.Data_Flow;
using System;
using System.Collections.Generic;

namespace IONET.Collada.Helpers
{
    public class SourceManager
    {
        private List<SourceHelper> _helpers = new List<SourceHelper>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        public void AddSource(Source src)
        {
            _helpers.Add(new SourceHelper(src));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceid"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string[] GetNameValue(string sourceid, int index)
        {
            sourceid = ColladaHelper.SanitizeID(sourceid);

            var helper = _helpers.Find(e => e.ID == sourceid);

            if (helper == null)
                return new string[0];

            return helper.GetStringValues(index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceid"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public float[] GetFloatValue(string sourceid, int index)
        {
            sourceid = ColladaHelper.SanitizeID(sourceid);
            
            var helper = _helpers.Find(e=>e.ID == sourceid);

            if (helper == null)
                return new float[0];

            return helper.GetFloatValues(index);
        }
    }

    public class SourceHelper
    {
        private Source _source;

        private float[] FloatValues = null;

        private string[] NameValues = null;

        public string ID { get => _source.ID; }

        private uint _stride { get => _source.Technique_Common.Accessor.Stride == 0 ? 1 : _source.Technique_Common.Accessor.Stride; }

        /// <summary>
        /// 
        /// </summary>
        public SourceHelper()
        {
            _source = new Source();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        public SourceHelper(Source src)
        {
            _source = src;

            if (_source.Float_Array != null)
                FloatValues = _source.Float_Array.GetValues();

            if (_source.Name_Array != null)
                NameValues = _source.Name_Array.GetValues();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string[] GetStringValues(int index)
        {
            return GetValues(index, NameValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public float[] GetFloatValues(int index)
        {
            return GetValues(index, FloatValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceid"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public T[] GetValues<T>(int index, T[] values)
        {
            if (values == null)
                return null;

            T[] f = new T[_stride];

            for(int i = 0; i < f.Length; i++)
                f[i] = values[index * f.Length + i];

            return f;
        }
        
    }
}
