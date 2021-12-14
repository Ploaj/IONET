using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using IONET.Collada.Core.Scene;
using IONET.Core;
using IONET.Core.Model;
using IONET.Core.Skeleton;
using System.Numerics;
using IONET.Collada.Helpers;
using IONET.Collada.Core.Geometry;
using IONET.Collada.Enums;
using IONET.Collada.FX.Materials;
using IONET.Collada.FX.Rendering;
using IONET.Collada.FX.Profiles.COMMON;
using System.Xml;

namespace IONET.Collada
{
    public class ColladaImporter : ISceneLoader
    {
        /// <summary>
        /// 
        /// </summary>
        private Collada _collada;

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
        /// <param name="filePath"></param>
        /// <returns></returns>
        public IOScene GetScene(string filePath)
        {
            // generate a new scene
            IOScene scene = new IOScene();


            // load collada file
            _collada = Collada.LoadFromFile(filePath);

            // failed to load collada file
            if (_collada == null)
                return scene;

            // load material library's to scene
            if(_collada.Library_Materials != null)
            {
                foreach(var mat in _collada.Library_Materials.Material)
                {
                    scene.Materials.Add(LoadMaterial(mat));
                }
            }
            

            // look through all visual scene
            foreach(var colscene in _collada.Library_Visual_Scene.Visual_Scene)
            {
                // treat each scene as a "model"
                IOModel model = new IOModel()
                {
                    Name = colscene.Name
                };

                // scan skeletons
                List<string> skelIDs = new List<string>();
                foreach (var v in colscene.Node)
                {
                    if (GetSkeletonReferences(v, out List<string> joints))
                        foreach (var j in joints)
                            if (!skelIDs.Contains(j))
                                skelIDs.Add(j);
                }

                // load nodes
                foreach (var v in colscene.Node)
                {
                    LoadNodes(v, null, model, skelIDs);
                }

                // add model
                scene.Models.Add(model);
            }
            

            // cleanup
            _collada = null;


            // done
            return scene;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetExtensions()
        {
            return new string[] { ".dae" };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool Verify(string filePath)
        {
            return Path.GetExtension(filePath).ToLower().Equals(".dae");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool GetSkeletonReferences(Node n, out List<string> jointIDs)
        {
            jointIDs = new List<string>();

            if(n.Instance_Controller != null)
               foreach(var c in n.Instance_Controller)
                    if (c.Skeleton != null)
                        foreach (var s in c.Skeleton)
                            jointIDs.Add(s.Value.Substring(1, s.Value.Length - 1));

            return jointIDs.Count > 0;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="bones"></param>
        private IOBone LoadNodes(Node n, IOBone parent, IOModel model, List<string> skeletonIds)
        {
            // create bone to represent node
            IOBone bone = new IOBone()
            {
                Name = n.Name,
                AltID = n.sID
            };

            // load matrix
            if (n.Matrix != null && n.Matrix.Length >= 0)
                bone.LocalTransform = n.Matrix[0].ToMatrix();
            else
            {
                // or segmented transform
                Vector3 scale = Vector3.One;
                Vector3 position = Vector3.Zero;
                Vector4 rx = Vector4.UnitX;
                Vector4 ry = Vector4.UnitY;
                Vector4 rz = Vector4.UnitZ;

                if (n.Scale != null && n.Scale.Length > 0)
                {
                    var val = n.Scale[0].GetValues();
                    scale = new Vector3(val[0], val[1], val[2]);
                }

                if (n.Translate != null && n.Translate.Length > 0)
                {
                    var val = n.Translate[0].GetValues();
                    position = new Vector3(val[0], val[1], val[2]);
                }

                if (n.Rotate != null && n.Rotate.Length > 0)
                {
                    foreach(var r in n.Rotate)
                    {
                        var val = r.GetValues();
                        switch (r.sID)
                        {
                            case "rotationX":
                                rx = new Vector4(val[0], val[1], val[2], val[3]);
                                break;
                            case "rotationY":
                                ry = new Vector4(val[0], val[1], val[2], val[3]);
                                break;
                            case "rotationZ":
                                rz = new Vector4(val[0], val[1], val[2], val[3]);
                                break;
                        }
                    }
                }

                // create transform
                bone.LocalTransform = Matrix4x4.CreateTranslation(position) *
                    Matrix4x4.CreateFromAxisAngle(new Vector3(rz.X, rz.Y, rz.Z), rz.W) *
                    Matrix4x4.CreateFromAxisAngle(new Vector3(ry.X, ry.Y, ry.Z), ry.W) *
                    Matrix4x4.CreateFromAxisAngle(new Vector3(rx.X, rx.Y, rx.Z), rx.W) *
                    Matrix4x4.CreateScale(scale);
            }


            // add this node to parent
            if (parent != null)
                parent.AddChild(bone);

            // load children
            if (n.node != null)
                foreach (var v in n.node)
                    LoadNodes(v, bone, model, skeletonIds);


            // load instanced geometry
            if (n.Instance_Geometry != null)
            {
                foreach (var g in n.Instance_Geometry)
                {
                    var geom = LoadGeometryFromID(n, g.URL);
                    geom.TransformVertices(bone.WorldTransform);
                    geom.ParentBone = bone;
                    model.Meshes.Add(geom);
                }
            }

            // load instanced geometry controllers
            if (n.Instance_Controller != null)
            {
                foreach (var c in n.Instance_Controller)
                {
                    var geom = LoadGeometryControllerFromID(n, c.URL);
                    geom.TransformVertices(bone.WorldTransform);
                    geom.ParentBone = bone;
                    model.Meshes.Add(geom);
                }
            }

            // detect skeleton
            if ((!string.IsNullOrEmpty(bone.Name) && skeletonIds.Contains(bone.Name)) ||
                (n.Type == Node_Type.JOINT && parent == null) ||
                (n.Instance_Camera == null && 
                n.Instance_Controller == null &&
                n.Instance_Geometry == null &&
                n.Instance_Light == null && 
                n.Instance_Node == null && 
                parent == null && 
                n.node != null && 
                n.node.Length > 0))
            {
                model.Skeleton.RootBones.Add(bone);
            }

            // complete
            return bone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IOMesh LoadGeometryControllerFromID(Node n, string id)
        {
            // sanitize
            id = ColladaHelper.SanitizeID(id);

            // find geometry by id
            var con = _collada.Library_Controllers.Controller.FirstOrDefault(e => e.ID == id);

            // not found
            if (con == null)
                return null;

            // load controllers

            SourceManager srcs = new SourceManager();
            foreach (var src in con.Skin.Source)
                srcs.AddSource(src);

            var v = con.Skin.Vertex_Weights.V.GetValues();
            var counts = con.Skin.Vertex_Weights.VCount.GetValues();
            var vi = 0;
            var vertexIndex = 0;

            List<IOEnvelope> envelopes = new List<IOEnvelope>();

            for (int i = 0; i < con.Skin.Vertex_Weights.Count; i++)
            {
                var en = new IOEnvelope();

                var stride = con.Skin.Vertex_Weights.Input.Length;

                for (int j = 0; j < counts[i]; j++)
                {
                    IOBoneWeight bw = new IOBoneWeight();
                    foreach (var input in con.Skin.Vertex_Weights.Input)
                    {
                        var index = v[vi + input.Offset + j * stride];
                        switch (input.Semantic)
                        {
                            case Input_Semantic.JOINT:
                                foreach(var jointInput in con.Skin.Joints.Input)
                                {
                                    switch (jointInput.Semantic)
                                    {
                                        case Input_Semantic.JOINT:
                                            bw.BoneName = srcs.GetNameValue(jointInput.source, index)[0];
                                            break;
                                        case Input_Semantic.INV_BIND_MATRIX:
                                            var m = srcs.GetFloatValue(jointInput.source, index);
                                            var t = new Matrix4x4(
                                                m[0], m[4], m[8], m[12],
                                                m[1], m[5], m[9], m[13],
                                                m[2], m[6], m[10], m[14],
                                                m[3], m[7], m[11], m[15]);
                                            bw.BindMatrix = t;
                                            break;
                                    }
                                }
                                break;
                            case Input_Semantic.WEIGHT:
                                bw.Weight = srcs.GetFloatValue(input.source, index)[0];
                                break;
                        }
                    }
                    en.Weights.Add(bw);
                }

                envelopes.Add(en);

                vi += counts[i] * stride;
                vertexIndex++;
            }

            // load geometry
            var geom = string.IsNullOrEmpty(con.Skin.sourceid) ? LoadGeometryFromID(n, con.Skin.sID, envelopes) : LoadGeometryFromID(n, con.Skin.sourceid, envelopes);


            // bind shape
            if(con.Skin.Bind_Shape_Matrix != null)
            {
                var m = con.Skin.Bind_Shape_Matrix.GetValues();
                var t = new Matrix4x4(
                    m[0], m[4], m[8], m[12],
                    m[1], m[5], m[9], m[13],
                    m[2], m[6], m[10], m[14],
                    m[3], m[7], m[11], m[15]);
                geom.TransformVertices(t);
            }

            return geom;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IOMesh LoadGeometryFromID(Node n, string id, List<IOEnvelope> vertexEnvelopes = null)
        {
            // sanitize
            id = ColladaHelper.SanitizeID(id);

            // find geometry by id
            var geom = _collada.Library_Geometries.Geometry.First(e => e.ID == id);

            // not found
            if (geom == null)
                return null;

            // create new mesh
            IOMesh mesh = new IOMesh()
            {
                Name = n.Name
            };

            // create source manager helper 
            SourceManager srcs = new SourceManager();
            if (geom.Mesh.Source != null)
                foreach (var src in geom.Mesh.Source)
                    srcs.AddSource(src);

            
            // load geomtry meshes
            if(geom.Mesh.Triangles != null)
            {
                foreach (var tri in geom.Mesh.Triangles)
                {
                    var stride = tri.Input.Max(e => e.Offset) + 1;
                    var poly = new IOPolygon() {
                        PrimitiveType = IOPrimitive.TRIANGLE,
                        MaterialName = tri.Material
                    };

                    var p = tri.P.GetValues();

                    for(int i = 0; i < tri.Count * 3; i++)
                    {
                        IOVertex vertex = new IOVertex();

                        for(int j = 0; j < tri.Input.Length; j++)
                        {
                            var input = tri.Input[j];

                            var index = p[i * stride + input.Offset];

                            ProcessInput(input.Semantic, input.source, input.Set, vertex, geom.Mesh.Vertices, index, srcs, vertexEnvelopes);
                        }

                        poly.Indicies.Add(mesh.Vertices.Count);
                        mesh.Vertices.Add(vertex);
                    }

                    mesh.Polygons.Add(poly);
                }
            }

            //TODO: collada trifan

            //TODO: collada  tristrip

            //TODO: collada linestrip

            //TODO: collada polylist

            //TODO: collada polygon
            
            return mesh;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="semantic"></param>
        /// <param name="values"></param>
        /// <param name="vertex"></param>
        /// <param name="vertices"></param>
        private void ProcessInput(Input_Semantic semantic, string source, int set, IOVertex vertex, Vertices vertices, int index, SourceManager srcs, List<IOEnvelope> vertexEnvelopes)
        {
            var values = srcs.GetFloatValue(source, index);

            switch (semantic)
            {
                case Input_Semantic.VERTEX:
                    // process per vertex input
                    foreach (var vertInput in vertices.Input)
                        ProcessInput(vertInput.Semantic, vertInput.source, 0, vertex, vertices, index, srcs, vertexEnvelopes);

                    // load envelopes if availiable
                    if (vertexEnvelopes != null && index < vertexEnvelopes.Count)
                    {
                        // copy bone weights
                        var en = vertexEnvelopes[index];
                        for (int i = 0; i < en.Weights.Count; i++)
                        {
                            vertex.Envelope.Weights.Add(new IOBoneWeight()
                            {
                                BoneName = en.Weights[i].BoneName,
                                Weight = en.Weights[i].Weight,
                                BindMatrix = en.Weights[i].BindMatrix
                            });
                        }

                        // make the bind matrix as being used
                        vertex.Envelope.UseBindMatrix = true;
                    }
                    break;
                case Input_Semantic.POSITION:
                    vertex.Position = new Vector3(
                        values.Length > 0 ? values[0] : 0,
                        values.Length > 1 ? values[1] : 0,
                        values.Length > 2 ? values[2] : 0);
                    break;
                case Input_Semantic.NORMAL:
                    vertex.Normal = new Vector3(
                        values.Length > 0 ? values[0] : 0,
                        values.Length > 1 ? values[1] : 0,
                        values.Length > 2 ? values[2] : 0);
                    break;
                case Input_Semantic.TANGENT:
                    vertex.Tangent = new Vector3(
                        values.Length > 0 ? values[0] : 0,
                        values.Length > 1 ? values[1] : 0,
                        values.Length > 2 ? values[2] : 0);
                    break;
                case Input_Semantic.BINORMAL:
                    vertex.Binormal = new Vector3(
                        values.Length > 0 ? values[0] : 0,
                        values.Length > 1 ? values[1] : 0,
                        values.Length > 2 ? values[2] : 0);
                    break;
                case Input_Semantic.TEXCOORD:
                    vertex.SetUV(
                        values.Length > 0 ? values[0] : 0,
                        values.Length > 1 ? values[1] : 0,
                        set);
                    break;
                case Input_Semantic.COLOR:
                    vertex.SetColor(
                        values.Length > 0 ? values[0] : 0,
                        values.Length > 1 ? values[1] : 0,
                        values.Length > 2 ? values[2] : 0,
                        values.Length > 3 ? values[3] : 0,
                        set);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IOMaterial LoadMaterial(Material mat)
        {
            var effectURL = mat.Instance_Effect?.URL;

            if (effectURL == null)
                return null;

            var effect = _collada.Library_Effects.Effect.ToList().Find(e=>e.ID == ColladaHelper.SanitizeID(effectURL));

            IOMaterial material = new IOMaterial()
            {
                Name = mat.ID,
                Label = mat.Name
            };
            
            if(effect != null && effect.Profile_COMMON != null && effect.Profile_COMMON.Length > 0)
            {
                var prof = effect.Profile_COMMON[0];

                var phong = prof.Technique.Phong;
                var blinn = prof.Technique.Blinn;
                var lambert = prof.Technique.Lambert;

                if (phong != null)
                {
                    if (phong.Transparency != null)
                        material.Alpha = phong.Transparency.Float.Value;

                    if (phong.Shininess != null)
                        material.Shininess = phong.Shininess.Float.Value;

                    if (phong.Diffuse != null)
                    {
                        if (ReadEffectColorType(prof, phong.Diffuse, out Vector4 color, out IOTexture texture))
                            material.DiffuseColor = color;

                        if (texture != null)
                            material.DiffuseMap = texture;
                    }

                    if (phong.Ambient != null)
                    {
                        if(ReadEffectColorType(prof, phong.Ambient, out Vector4 color, out IOTexture texture))
                            material.AmbientColor = color;

                        if (texture != null)
                            material.AmbientMap = texture;
                    }

                    if (phong.Specular != null)
                    {
                        if (ReadEffectColorType(prof, phong.Specular, out Vector4 color, out IOTexture texture))
                            material.SpecularColor = color;

                        if (texture != null)
                            material.SpecularMap = texture;
                    }

                    if (phong.Reflective != null)
                    {
                        if (ReadEffectColorType(prof, phong.Reflective, out Vector4 color, out IOTexture texture))
                            material.ReflectiveColor = color;

                        if (texture != null)
                            material.ReflectiveMap = texture;
                    }
                }


                if (lambert != null)
                {
                    if (lambert.Transparency != null)
                        material.Alpha = lambert.Transparency.Float.Value;

                    if (lambert.Diffuse != null)
                    {
                        if (ReadEffectColorType(prof, lambert.Diffuse, out Vector4 color, out IOTexture texture))
                            material.DiffuseColor = color;

                        if (texture != null)
                            material.DiffuseMap = texture;
                    }

                    if (lambert.Ambient != null)
                    {
                        if (ReadEffectColorType(prof, lambert.Ambient, out Vector4 color, out IOTexture texture))
                            material.AmbientColor = color;

                        if (texture != null)
                            material.AmbientMap = texture;
                    }

                    if (lambert.Reflective != null)
                    {
                        if (ReadEffectColorType(prof, lambert.Reflective, out Vector4 color, out IOTexture texture))
                            material.ReflectiveColor = color;

                        if (texture != null)
                            material.ReflectiveMap = texture;
                    }
                }


                if (blinn != null)
                {
                    if (blinn.Transparency != null)
                        material.Alpha = blinn.Transparency.Float.Value;

                    if (blinn.Shininess != null)
                        material.Shininess = blinn.Shininess.Float.Value;

                    if (blinn.Diffuse != null)
                    {
                        if (ReadEffectColorType(prof, blinn.Diffuse, out Vector4 color, out IOTexture texture))
                            material.DiffuseColor = color;

                        if (texture != null)
                            material.DiffuseMap = texture;
                    }

                    if (blinn.Ambient != null)
                    {
                        if (ReadEffectColorType(prof, blinn.Ambient, out Vector4 color, out IOTexture texture))
                            material.AmbientColor = color;

                        if (texture != null)
                            material.AmbientMap = texture;
                    }

                    if (blinn.Specular != null)
                    {
                        if (ReadEffectColorType(prof, blinn.Specular, out Vector4 color, out IOTexture texture))
                            material.SpecularColor = color;

                        if (texture != null)
                            material.SpecularMap = texture;
                    }

                    if (blinn.Reflective != null)
                    {
                        if (ReadEffectColorType(prof, blinn.Reflective, out Vector4 color, out IOTexture texture))
                            material.ReflectiveColor = color;

                        if (texture != null)
                            material.ReflectiveMap = texture;
                    }
                }

            }

            return material;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="color"></param>
        /// <param name="texture"></param>
        private bool ReadEffectColorType(Profile_COMMON prof, FX_Common_Color_Or_Texture_Type type, out Vector4 color, out IOTexture texture)
        {
            color = Vector4.One;
            texture = null;

            if (type.Color != null)
            {
                var c = type.Color.GetValues();
                color = new Vector4(c[0], c[1], c[2], c[3]);
            }

            if (type.Texture != null)
            {
                // create diffuse texture
                texture = new IOTexture();

                var texid = type.Texture.Textures;


                // blender image lookup
                if (prof.New_Param != null)
                {
                    var samp = prof.New_Param.FirstOrDefault(e => e.sID == type.Texture.Textures);
                    if (samp != null && samp.Data != null)
                    {
                        var samp2d = samp.Data.FirstOrDefault(e => e.Name == "sampler2D");
                        if (samp2d != null)
                        {
                            var src = FindChild(samp2d, "source");
                            if (src != null)
                            {
                                var surf = prof.New_Param.FirstOrDefault(e => e.sID == src.InnerText);
                                if (surf != null && surf.Data != null)
                                {
                                    var sur2d = surf.Data.FirstOrDefault(e => e.Name == "surface");
                                    if (sur2d != null)
                                    {
                                        var init = FindChild(sur2d, "init_from");
                                        texid = init.InnerText;
                                    }
                                }
                            }
                        }
                    }
                }

                // lookup image from image library
                var image = _collada.Library_Images.Image.FirstOrDefault(e => e.ID == texid);
                if (image != null)
                {
                    texture.Name = image.Name;
                    texture.FilePath = string.IsNullOrEmpty(image.Init_From.Ref) ? image.Init_From.Value : image.Init_From.Ref;
                }
            }

            return (type.Color != null);
        }


        private static XmlNode FindChild(XmlNode node, string name)
        {
            foreach (XmlElement v in node.ChildNodes)
            {
                if (v.Name == name)
                {
                    return v;
                }
            }
            return null;
        }
    }
}
