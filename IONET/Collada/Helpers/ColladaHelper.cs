using System;
using System.Collections.Generic;
using System.Text;

namespace IONET.Collada.Helpers
{
    public class ColladaHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string SanitizeID(string id)
        {
            if (id.StartsWith("#"))
                return id.Substring(1, id.Length - 1);

            return id;
        }
    }
}
