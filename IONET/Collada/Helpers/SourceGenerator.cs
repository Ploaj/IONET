using IONET.Collada.Core.Data_Flow;
using IONET.Collada.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IONET.Collada.Helpers
{
    public class SourceGenerator
    {
        public class SourceOptimizer
        {
            public string Name;

            public Input_Semantic Semantic;

            public int Set = 0;

            public int Stride
            {
                get
                {
                    switch (Semantic)
                    {
                        case Input_Semantic.TEXCOORD:
                        case Input_Semantic.UV:
                            return 2;
                        case Input_Semantic.COLOR:
                            return 4;
                        case Input_Semantic.POSITION:
                        case Input_Semantic.NORMAL:
                        case Input_Semantic.TANGENT:
                        case Input_Semantic.BINORMAL:
                        default:
                            return 3;
                    }
                }
            }

            private List<float> newVertices = new List<float>();
            private Dictionary<int, int> remap = new Dictionary<int, int>();

            /// <summary>
            /// 
            /// </summary>
            /// <param name="name"></param>
            /// <param name="sem"></param>
            public SourceOptimizer(string name, Input_Semantic sem, float[] values, int set)
            {
                Name = name + "-" + sem.ToString() + (set != 0 ? set.ToString() : "");
                Semantic = sem;
                Set = set;
                
                if(Semantic == Input_Semantic.POSITION)
                {
                    // do not optimize position
                    newVertices.AddRange(values);
                }
                else
                {
                    // optimize and generate remapping
                    Dictionary<string, int> valueToIndex = new Dictionary<string, int>();

                    for (int i = 0; i < values.Length; i += Stride)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int j = 0; j < Stride; j++)
                            sb.Append(values[i + j] + " ");
                        var val = sb.ToString();

                        if (!valueToIndex.ContainsKey(val))
                        {
                            valueToIndex.Add(val, newVertices.Count / Stride);
                            for (int j = 0; j < Stride; j++)
                                newVertices.Add(values[i + j]);
                        }

                        remap.Add(i / Stride, valueToIndex[val]);
                    }
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public int Remap(int index)
            {
                return remap[index];
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="indices"></param>
            /// <param name="values"></param>
            /// <returns></returns>
            public int[] Remap(int[] indices)
            {
                for(int i = 0; i < indices.Length; i++)
                    indices[i] = remap[indices[i]];

                return indices;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public Source GetSource()
            {
                var values = newVertices.ToArray();

                Source source = new Source()
                {
                    ID = Name
                };

                // set values
                source.Float_Array = new Float_Array()
                {
                    Count = values.Length,
                    ID = source.ID + "-array"
                };
                source.Float_Array.SetValues(values);

                // set technique
                switch (Semantic)
                {
                    case Input_Semantic.POSITION:
                    case Input_Semantic.NORMAL:
                    case Input_Semantic.TANGENT:
                    case Input_Semantic.BINORMAL:
                        source.Technique_Common = new IONET.Collada.Core.Technique_Common.Technique_Common_Source()
                        {
                            Accessor = new Accessor()
                            {
                                Source = source.Float_Array.ID,
                                Stride = 3,
                                Count = (uint)(values.Length / 3),
                                Param = new IONET.Collada.Core.Parameters.Param[]
                                {
                                new IONET.Collada.Core.Parameters.Param() { Name = "X", Type = "float" },
                                new IONET.Collada.Core.Parameters.Param() { Name = "Y", Type = "float" },
                                new IONET.Collada.Core.Parameters.Param() { Name = "Z", Type = "float" },
                                }
                            }
                        };
                        break;
                    case Input_Semantic.UV:
                    case Input_Semantic.TEXCOORD:
                        source.Technique_Common = new IONET.Collada.Core.Technique_Common.Technique_Common_Source()
                        {
                            Accessor = new Accessor()
                            {
                                Source = source.Float_Array.ID,
                                Stride = 2,
                                Count = (uint)(values.Length / 2),
                                Param = new IONET.Collada.Core.Parameters.Param[]
                                {
                                new IONET.Collada.Core.Parameters.Param() { Name = "S", Type = "float" },
                                new IONET.Collada.Core.Parameters.Param() { Name = "T", Type = "float" },
                                }
                            }
                        };
                        break;
                    case Input_Semantic.COLOR:
                        source.Technique_Common = new IONET.Collada.Core.Technique_Common.Technique_Common_Source()
                        {
                            Accessor = new Accessor()
                            {
                                Source = source.Float_Array.ID,
                                Stride = 4,
                                Count = (uint)(values.Length / 4),
                                Param = new IONET.Collada.Core.Parameters.Param[]
                                {
                                new IONET.Collada.Core.Parameters.Param() { Name = "R", Type = "double" },
                                new IONET.Collada.Core.Parameters.Param() { Name = "G", Type = "double" },
                                new IONET.Collada.Core.Parameters.Param() { Name = "B", Type = "double" },
                                new IONET.Collada.Core.Parameters.Param() { Name = "A", Type = "double" },
                                }
                            }
                        };
                        break;
                }

                return source;
            }
        }

        private List<SourceOptimizer> _optimizers = new List<SourceOptimizer>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Source[] GetSources()
        {
            return _optimizers.Select(e=>e.GetSource()).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="semantic"></param>
        /// <returns></returns>
        public string GetID(Input_Semantic semantic, int set = 0)
        {
            var src = _optimizers.Find(e => e.Semantic == semantic && e.Set == set);
            if (src == null)
                return "";
            else
                return src.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int[] Remap(List<int> triangles)
        {
            int[] d = new int[_optimizers.Count * triangles.Count];

            for(int i = 0; i < _optimizers.Count; i++)
            {
                for (int j = 0; j < triangles.Count; j++)
                {
                    if(_optimizers[i].Semantic == Input_Semantic.POSITION)
                        d[j * _optimizers.Count + i] = triangles[j];
                    else
                        d[j * _optimizers.Count + i] = _optimizers[i].Remap(triangles[j]);
                }
            }

            return d;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="semantic"></param>
        /// <param name="triangles"></param>
        /// <param name="values"></param>
        public void AddSourceData(string name, Input_Semantic semantic, float[] values, int set = 0)
        {
            var src = _optimizers.Find(e=>e.Semantic == semantic && e.Set == set);

            if(src == null)
            {
                src = new SourceOptimizer(name, semantic, values, set);
                src.Set = set;
                _optimizers.Add(src);
            }
        }

    }
}
