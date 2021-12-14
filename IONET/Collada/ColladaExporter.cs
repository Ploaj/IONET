using IONET.Core;
using System;
using System.Collections.Generic;
using IONET.Collada.Core.Scene;
using IONET.Collada.Core.Metadata;
using IONET.Core.Model;
using IONET.Core.Skeleton;
using IONET.Collada.Core.Transform;
using IONET.Collada.Core.Geometry;
using IONET.Collada.Core.Data_Flow;
using IONET.Collada.Helpers;
using System.Linq;
using IONET.Collada.Enums;
using IONET.Collada.Core.Controller;
using System.Numerics;
using IONET.Collada.FX.Materials;
using IONET.Collada.FX.Effects;
using IONET.Collada.FX.Texturing;
using IONET.Collada.FX.Rendering;
using IONET.Collada.Core.Parameters;
using System.Xml;

namespace IONET.Collada
{
    public class ColladaExporter : ISceneExporter
    {
        private ExportSettings _settings;
        private IONET.Collada.Collada _collada;

        private HashSet<string> _usedIDs = new HashSet<string>();

        /// <summary>
        /// Gets a unique ID for given string
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUniqueID(string id)
        {
            if (!_usedIDs.Contains(id))
            {
                _usedIDs.Add(id);
                return id;
            }
            else
            {
                int index = 0;
                while (_usedIDs.Contains(id + "_" + index))
                    index++;
                _usedIDs.Add(id + "_" + index);
                return id + "_" + index;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Name()
        {
            return "Collada";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="filePath"></param>
        public void ExportScene(IOScene scene, string filePath, ExportSettings settings)
        {
            // create collada document
            _collada = new IONET.Collada.Collada();
            _settings = settings;

            _collada.Version = "1.4.1";
            

            // export materials
            foreach (var mat in scene.Materials)
                ProcessMaterial(mat);


            // initialize scene
            var visscene = new Visual_Scene
            {
                Name = scene.Name
            };
            visscene.ID = scene.Name;


            // export models nodes
            List<Node> nodes = new List<Node>();
            foreach(var mod in scene.Models)
                nodes.AddRange(ProcessModel(mod));
            visscene.Node = nodes.ToArray();


            // instance the scene
            var scene_instance = new Instance_Visual_Scene();
            scene_instance.URL = "#" + visscene.Name;

            _collada.Library_Visual_Scene = new Library_Visual_Scenes();
            _collada.Library_Visual_Scene.Visual_Scene = new Visual_Scene[] { visscene };

            _collada.Scene = new Scene();
            _collada.Scene.Visual_Scene = scene_instance;

            
            // initialize asset
            InitAsset();


            // save to file
            _collada.SaveToFile(filePath);

            // cleanup
            _usedIDs.Clear();
            _collada = null;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitAsset()
        {
            _collada.Asset = new Asset();
            _collada.Asset.Up_Axis = "Y_UP";
            _collada.Asset.Unit = new Asset_Unit()
            {
                Name = "Meter",
                Meter = 1
            };

            _collada.Asset.Contributor = new Asset_Contributor[1] { new Asset_Contributor()
            {
                Authoring_Tool = "IONET Exporter"
            } };
            _collada.Asset.Created = DateTime.Now;
            _collada.Asset.Modified = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="tex"></param>
        /// <returns></returns>
        private FX_Common_Color_Or_Texture_Type GenerateTextureInfo(string sid, Vector4 color, IOTexture tex, List<New_Param> textureParams)
        {
            FX.Custom_Types.Texture texData = null;

            if(tex != null)
            {
                var surfaceString = AddImage(tex);

                var doc = new XmlDocument();

                var surfacenode = doc.CreateElement("surface", "http://www.collada.org/2005/11/COLLADASchema");
                surfacenode.SetAttribute("type", "2D");
                var init_node = doc.CreateElement("init_from", "http://www.collada.org/2005/11/COLLADASchema");
                init_node.InnerText = surfaceString;
                surfacenode.AppendChild(init_node);

                textureParams.Add(new New_Param()
                {
                    sID = surfaceString + "_surface",
                    Data = new XmlElement[] { surfacenode }
                });

                var samplernode = doc.CreateElement("sampler2D", "http://www.collada.org/2005/11/COLLADASchema");
                var sourceNode = doc.CreateElement("source", "http://www.collada.org/2005/11/COLLADASchema");
                sourceNode.InnerText = surfaceString + "_surface";
                samplernode.AppendChild(sourceNode);

                textureParams.Add(new New_Param()
                {
                    sID = surfaceString + "_sampler",
                    Data = new XmlElement[] { samplernode }
                });

                texData = new IONET.Collada.FX.Custom_Types.Texture()
                {
                    Textures = surfaceString + "_sampler",
                    TexCoord = "CHANNEL0",
                };
            }
            
            return new FX_Common_Color_Or_Texture_Type()
            {
                Color = new IONET.Collada.Core.Lighting.Color()
                { sID = sid, Value_As_String = $"{color.X} {color.Y} {color.Z} {color.W}" },

                Texture = _settings.ExportTextureInfo ? texData : null
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mat"></param>
        private void ProcessMaterial(IOMaterial mat)
        {
            List<New_Param> textureParams = new List<New_Param>();

            // create phong shading
            var phong = new Phong()
            {
                Shininess = new FX_Common_Float_Or_Param_Type { Float = new IONET.Collada.Types.SID_Float() { sID = "shininess", Value = mat.Shininess } },
                Transparency = new FX_Common_Float_Or_Param_Type() { Float = new IONET.Collada.Types.SID_Float() { sID = "transparency", Value = mat.Alpha } },
                Reflectivity = new FX_Common_Float_Or_Param_Type() { Float = new IONET.Collada.Types.SID_Float() { sID = "reflectivity", Value = mat.Reflectivity } },
                Ambient = GenerateTextureInfo("ambient", mat.AmbientColor, mat.AmbientMap, textureParams),
                Diffuse = GenerateTextureInfo("diffuse", mat.DiffuseColor, mat.DiffuseMap, textureParams),
                Specular = GenerateTextureInfo("specular", mat.SpecularColor, mat.SpecularMap, textureParams),
                Emission = GenerateTextureInfo("emission", mat.EmissionColor, mat.EmissionMap, textureParams),
                Reflective = GenerateTextureInfo("reflective", mat.ReflectiveColor, mat.ReflectiveMap, textureParams),
            };

            // create effect
            Effect effect = new Effect()
            {
                ID = GetUniqueID(mat.Name + "-effect"),
                Name = mat.Name,
                Profile_COMMON = new IONET.Collada.FX.Profiles.COMMON.Profile_COMMON[]
                {
                    new IONET.Collada.FX.Profiles.COMMON.Profile_COMMON()
                    {
                        Technique = new IONET.Collada.FX.Profiles.COMMON.Effect_Technique_COMMON()
                        {
                            sID = "standard",
                            Phong = phong
                        },
                        New_Param = textureParams.ToArray()
                    }
                }
            };

            // create material
            Material material = new Material()
            {
                ID = GetUniqueID(mat.Name),
                Name = mat.Name,
                Instance_Effect = new Instance_Effect()
                {
                    URL = "#" + effect.ID
                }
            };

            // add effect to effect library
            if (_collada.Library_Effects == null)
                _collada.Library_Effects = new Library_Effects();

            if (_collada.Library_Effects.Effect == null)
                _collada.Library_Effects.Effect = new Effect[0];

            Array.Resize(ref _collada.Library_Effects.Effect, _collada.Library_Effects.Effect.Length + 1);

            _collada.Library_Effects.Effect[_collada.Library_Effects.Effect.Length - 1] = effect;

            // add material to material library
            if (_collada.Library_Materials == null)
                _collada.Library_Materials = new Library_Materials();

            if (_collada.Library_Materials.Material == null)
                _collada.Library_Materials.Material = new Material[0];

            Array.Resize(ref _collada.Library_Materials.Material, _collada.Library_Materials.Material.Length + 1);

            _collada.Library_Materials.Material[_collada.Library_Materials.Material.Length - 1] = material;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string AddImage(IOTexture tex)
        {
            var name = tex.Name;
            var filePath = tex.FilePath;

            // create image node
            Image img = new Image()
            {
                ID = GetUniqueID(name + "-image"),
                Name = name,
                Init_From = new Init_From()
                {
                    Value = filePath
                }
            };

            // add image element to image library
            if (_collada.Library_Images == null)
                _collada.Library_Images = new Library_Images();

            if (_collada.Library_Images.Image == null)
                _collada.Library_Images.Image = new Image[0];

            Array.Resize(ref _collada.Library_Images.Image, _collada.Library_Images.Image.Length + 1);

            _collada.Library_Images.Image[_collada.Library_Images.Image.Length - 1] = img;

            // return id
            return img.ID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private List<Node> ProcessModel(IOModel model)
        {
            List<Node> nodes = new List<Node>();

            // bones
            foreach (var root in model.Skeleton.RootBones)
                nodes.Add(ProcessSkeleton(root));

            // get root bone
            IOBone rootBone = null;
            if (model.Skeleton != null && model.Skeleton.RootBones.Count > 0)
                rootBone = model.Skeleton.RootBones[0];

            // mesh
            foreach (var mesh in model.Meshes)
                nodes.Add(ProcessMesh(mesh, model, rootBone));

            return nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bone"></param>
        /// <returns></returns>
        private Node ProcessSkeleton(IOBone bone)
        {
            Node n = new Node()
            {
                Name = bone.Name,
                sID = bone.Name,
                ID = bone.Name,
                Matrix = new Matrix[] { new Matrix() },
                Type = Node_Type.JOINT
            };

            n.Matrix[0].FromMatrix(bone.LocalTransform);

            n.node = new Node[bone.Children.Length];

            int childIndex = 0;
            foreach(var child in bone.Children)
                n.node[childIndex++] = ProcessSkeleton(child);

            return n;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        private Node ProcessMesh(IOMesh mesh, IOModel model, IOBone rootBone)
        {
            Node n = new Node()
            {
                Name = mesh.Name,
                sID = mesh.Name,
                ID = mesh.Name,
                Type = Node_Type.NODE
            };

            var materials = mesh.Polygons.Select(e => e.MaterialName).Distinct();

            if (mesh.HasEnvelopes())
            {
                var geom = new Instance_Controller();

                geom.URL = "#" + GenerateGeometryController(mesh, model.Skeleton);

                if (rootBone != null)
                    geom.Skeleton = new Skeleton[] { new Skeleton() { Value = "#" + rootBone.Name } };

                n.Instance_Controller = new Instance_Controller[] { geom };
                
                n.Instance_Controller[0].Bind_Material = new IONET.Collada.FX.Materials.Bind_Material[]
                {
                    new Bind_Material()
                    {
                        Technique_Common = new FX.Technique_Common.Technique_Common_Bind_Material()
                        {
                            Instance_Material = materials.Select(e => new Instance_Material_Geometry() { Symbol = e, Target = "#" + e }).ToArray()
                        }
                    }
                };
            }
            else
            {
                var geom = new Instance_Geometry();

                geom.URL = "#" + GenerateGeometry(mesh);

                n.Instance_Geometry = new Instance_Geometry[] { geom };

                n.Instance_Geometry[0].Bind_Material = new IONET.Collada.FX.Materials.Bind_Material[]
                {
                    new Bind_Material()
                    {
                        Technique_Common = new FX.Technique_Common.Technique_Common_Bind_Material()
                        {
                            Instance_Material = materials.Select(e => new Instance_Material_Geometry() { Symbol = e, Target = "#" + e }).ToArray()
                        }
                    }
                };
            }

            return n;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        private string GenerateGeometryController(IOMesh mesh, IOSkeleton skeleton)
        {
            Controller con = new Controller()
            {
                ID = GetUniqueID(mesh.Name + "-controller"),
                Name = mesh.Name
            };

            con.Skin = new Skin()
            {
                sourceid = "#" + GenerateGeometry(mesh)
            };

            con.Skin.Bind_Shape_Matrix = new IONET.Collada.Types.Float_Array_String()
            {
                Value_As_String = "1 0 0 0 0 1 0 0 0 0 1 0 0 0 0 1"
            };

            List<int> weightCounts = new List<int>();
            List<int> binds = new List<int>();

            List<string> boneNames = new List<string>();
            List<float> boneBinds = new List<float>();
            List<float> weights = new List<float>();

            foreach (var v in mesh.Vertices)
            {
                weightCounts.Add(v.Envelope.Weights.Count);
                foreach(var bw in v.Envelope.Weights)
                {
                    if (!boneNames.Contains(bw.BoneName))
                    {
                        boneNames.Add(bw.BoneName);
                        Matrix4x4.Invert(skeleton.GetBoneByName(bw.BoneName).WorldTransform, out Matrix4x4 mat);
                        boneBinds.AddRange(new float[] {
                            mat.M11, mat.M21, mat.M31, mat.M41,
                            mat.M12, mat.M22, mat.M32, mat.M42,
                            mat.M13, mat.M23, mat.M33, mat.M43,
                            mat.M14, mat.M24, mat.M34, mat.M44
                        });
                    }

                    if (!weights.Contains(bw.Weight))
                        weights.Add(bw.Weight);

                    binds.Add(boneNames.IndexOf(bw.BoneName));
                    binds.Add(weights.IndexOf(bw.Weight));
                }
            }

            if (_settings.BlenderMode)
            {
                // blender is so stupid
                // the binds need to be every so slightly different or it kills bones
                for (int i = 0; i < boneBinds.Count; i++)
                    boneBinds[i] += 0.000001f;
            }

            var mid = GetUniqueID(mesh.Name + "-matrices");
            var jid = GetUniqueID(mesh.Name + "-joints");
            var wid = GetUniqueID(mesh.Name + "-weights");

            var midarr = GetUniqueID(mesh.Name + "-matrices-array");
            var jidarr = GetUniqueID(mesh.Name + "-joints-array");
            var widarr = GetUniqueID(mesh.Name + "-weights-array");

            con.Skin.Source = new Source[]
            {
                new Source()
                {
                    ID = jid,
                    Name_Array = new Name_Array()
                    {
                        Count = boneNames.Count,
                        ID = jidarr,
                        Value_Pre_Parse = string.Join(" ", boneNames)
                    },
                    Technique_Common = new IONET.Collada.Core.Technique_Common.Technique_Common_Source()
                    {
                        Accessor = new Accessor()
                        {
                            Count = (uint)boneNames.Count,
                            Source =  "#" + jidarr,
                            Param = new IONET.Collada.Core.Parameters.Param[]
                            {
                                new IONET.Collada.Core.Parameters.Param()
                                {
                                    Name = "JOINT",
                                    Type = "Name"
                                }
                            },
                            Stride = 1
                        }
                    }
                },
                new Source()
                {
                    ID = mid,
                    Float_Array = new Float_Array()
                    {
                        Count = boneBinds.Count,
                        ID = midarr,
                        Value_As_String = string.Join(" ", boneBinds)
                    },
                    Technique_Common = new IONET.Collada.Core.Technique_Common.Technique_Common_Source()
                    {
                        Accessor = new Accessor()
                        {
                            Count = (uint)boneBinds.Count / 16,
                            Source =  "#" + midarr,
                            Param = new IONET.Collada.Core.Parameters.Param[]
                            {
                                new IONET.Collada.Core.Parameters.Param()
                                {
                                    Name = "TRANSFORM",
                                    Type = "float4x4"
                                }
                            },
                            Stride = 16
                        }
                    }
                },
                new Source()
                {
                    ID = wid,
                    Float_Array = new Float_Array()
                    {
                        Count = weights.Count,
                        ID = widarr,
                        Value_As_String = string.Join(" ", weights)
                    },
                    Technique_Common = new IONET.Collada.Core.Technique_Common.Technique_Common_Source()
                    {
                        Accessor = new Accessor()
                        {
                            Count = (uint)weights.Count,
                            Source =  "#" + widarr,
                            Param = new IONET.Collada.Core.Parameters.Param[]
                            {
                                new IONET.Collada.Core.Parameters.Param()
                                {
                                    Name = "WEIGHT",
                                    Type = "float"
                                }
                            },
                            Stride = 1
                        }
                    }
                },
            };

            con.Skin.Joints = new Joints()
            {
                Input = new Input_Unshared[]
                {
                    new Input_Unshared()
                    {
                        Semantic = Input_Semantic.JOINT,
                        source = "#" + jid
                    },
                    new Input_Unshared()
                    {
                        Semantic = Input_Semantic.INV_BIND_MATRIX,
                        source = "#" + mid
                    },
                }
            };

            con.Skin.Vertex_Weights = new Vertex_Weights()
            {
                Count = weightCounts.Count,
                V = new IONET.Collada.Types.Int_Array_String()
                {
                    Value_As_String = string.Join(" ", binds)
                },
                VCount = new IONET.Collada.Types.Int_Array_String()
                {
                    Value_As_String = string.Join(" ", weightCounts)
                },
                Input = new Input_Shared[]
                {
                    new Input_Shared()
                    {
                        Semantic = Input_Semantic.JOINT,
                        source = "#" + jid,
                        Offset = 0
                    },
                    new Input_Shared()
                    {
                        Semantic = Input_Semantic.WEIGHT,
                        source = "#" + wid,
                        Offset = 1
                    },
                }
            };


            // add geometry element to document
            if (_collada.Library_Controllers == null)
                _collada.Library_Controllers = new Library_Controllers();

            if (_collada.Library_Controllers.Controller == null)
                _collada.Library_Controllers.Controller = new Controller[0];

            Array.Resize(ref _collada.Library_Controllers.Controller, _collada.Library_Controllers.Controller.Length + 1);

            _collada.Library_Controllers.Controller[_collada.Library_Controllers.Controller.Length - 1] = con;

            return con.ID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GenerateGeometry(IOMesh mesh)
        {
            // convert mesh to triangles to simplify
            mesh.MakeTriangles();

            // create a unique geometry id
            var geomID = GetUniqueID(mesh.Name + "-geometry");

            // create geometry element
            Geometry geom = new Geometry()
            {
                ID = geomID,
                Name = mesh.Name
            };

            geom.Mesh = new Mesh();

            // generate sources
            SourceGenerator srcgen = new SourceGenerator();

            srcgen.AddSourceData(
                geomID, Input_Semantic.POSITION,
                mesh.Vertices.SelectMany(e => new float[] { e.Position.X, e.Position.Y, e.Position.Z }).ToArray());

            srcgen.AddSourceData(
                geomID, Input_Semantic.NORMAL,
                mesh.Vertices.SelectMany(e => new float[] { e.Normal.X, e.Normal.Y, e.Normal.Z }).ToArray());

            for (int i = 0; i < 7; i++)
                if (mesh.HasUVSet(i))
                {
                    srcgen.AddSourceData(
                        geomID, Input_Semantic.TEXCOORD,
                        mesh.Vertices.SelectMany(e => new float[] { e.UVs[i].X, e.UVs[i].Y }).ToArray(),
                        i);
                }

            for (int i = 0; i < 7; i++)
                if (mesh.HasColorSet(i))
                {
                    srcgen.AddSourceData(
                        geomID, Input_Semantic.COLOR,
                        mesh.Vertices.SelectMany(e => new float[] { e.Colors[i].X, e.Colors[i].Y, e.Colors[i].Z, e.Colors[i].W }).ToArray(),
                        i);
                }

            // fill in vertex info
            geom.Mesh.Vertices = new Vertices()
            {
                ID = GetUniqueID(mesh.Name + "-vertices"),
                Input = new Input_Unshared[]{
                    new Input_Unshared()
                    {
                        Semantic = IONET.Collada.Enums.Input_Semantic.POSITION,
                        source = "#" + srcgen.GetID(Input_Semantic.POSITION)
                    }
                }
            };

            // fill in triangles

            var polyIndex = 0;
            geom.Mesh.Triangles = new Triangles[mesh.Polygons.Count];
            foreach (var poly in mesh.Polygons)
            {
                if (poly.PrimitiveType != IOPrimitive.TRIANGLE)
                {
                    System.Diagnostics.Debug.WriteLine("Warning: " + poly.PrimitiveType + " not currently supported");
                    continue;
                }

                Triangles tri = new Triangles()
                {
                    Count = poly.Indicies.Count / 3,
                    Material = poly.MaterialName
                };
                
                List<Input_Shared> inputs = new List<Input_Shared>();
                inputs.Add(new Input_Shared()
                {
                    Semantic = Input_Semantic.VERTEX,
                    Offset = inputs.Count,
                    source = "#" + geom.Mesh.Vertices.ID
                });

                inputs.Add(new Input_Shared()
                {
                    Semantic = Input_Semantic.NORMAL,
                    Offset = inputs.Count,
                    source = "#" + srcgen.GetID(Input_Semantic.NORMAL)
                });
                
                for (int i = 0; i < 7; i++)
                    if (mesh.HasUVSet(i))
                    {
                        inputs.Add(new Input_Shared()
                        {
                            Semantic = Input_Semantic.TEXCOORD,
                            source = "#" + srcgen.GetID(Input_Semantic.TEXCOORD, i),
                            Offset = inputs.Count,
                            Set = i
                        });
                    }

                for (int i = 0; i < 7; i++)
                    if (mesh.HasColorSet(i))
                    {
                        inputs.Add(new Input_Shared()
                        {
                            Semantic = Input_Semantic.COLOR,
                            source = "#" + srcgen.GetID(Input_Semantic.COLOR, i),
                            Offset = inputs.Count,
                            Set = i
                        });
                    }

                tri.Input = inputs.ToArray();

                tri.P = new IONET.Collada.Types.Int_Array_String()
                {
                    Value_As_String = string.Join(" ", srcgen.Remap(poly.Indicies))
                };
                
                geom.Mesh.Triangles[polyIndex++] = tri;
            }


            // generate sources
            geom.Mesh.Source = srcgen.GetSources();


            // add geometry element to document
            if (_collada.Library_Geometries ==null)
                _collada.Library_Geometries = new Library_Geometries();

            if (_collada.Library_Geometries.Geometry == null)
                _collada.Library_Geometries.Geometry = new Geometry[0];

            Array.Resize(ref _collada.Library_Geometries.Geometry, _collada.Library_Geometries.Geometry.Length + 1);

            _collada.Library_Geometries.Geometry[_collada.Library_Geometries.Geometry.Length - 1] = geom;

            // return geometry id
            return geomID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetExtensions()
        {
            return new string[] { ".dae" };
        }
        
    }
}
