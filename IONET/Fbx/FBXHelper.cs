using IONET.Core.IOMath;
using IONET.Core.Model;
using IONET.Core.Skeleton;
using IONET.Fbx.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace IONET.Fbx
{
    /// <summary>
    /// Helper class for scrapping relevent information from fbx files
    /// Current known to work with Version 7400 and 6100 but other may work as well
    /// </summary>
    public class FbxHelper
    {
        private FbxDocument _document;

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, List<string>> Connections = new Dictionary<string, List<string>>();

        /// <summary>
        /// Number of properties used to describe a node
        /// Different depending on fbx version
        /// </summary>
        private int PropertyDescSize
        {
            get
            {
                if(Version == 7400 || Version == 7500 || Version == 7200)
                    return 4;

                return 3;
            }
        }

        /// <summary>
        /// Number of properties used to describe a node
        /// Different depending on fbx version
        /// </summary>
        private int NodeDescSize
        {
            get
            {
                if (Version == 7400 || Version == 7500 || Version == 7200)
                    return 3;

                return 2;
            }
        }

        /// <summary>
        /// Gets FBX document version
        /// </summary>
        public int Version
        {
            get; internal set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        public FbxHelper(FbxDocument document)
        {
            // set document
            _document = document;

            // get and store sfbx version
            var ver = _document.GetNodesByName("FBXVersion");
            if (ver.Length > 0)
                Version = (int)ver[0].Value;
            else
                Version = -1;

            // get connectors
            var connections = _document.GetNodesByName("C");
            if (connections.Length == 0)
                connections = _document.GetNodesByName("Connect");

            // use strings for backwards compatibility
            foreach (var oo in connections)
            {
                if (!Connections.ContainsKey(oo.Properties[1].ToString()))
                    Connections.Add(oo.Properties[1].ToString(), new List<string>());

                Connections[oo.Properties[1].ToString()].Add(oo.Properties[2].ToString());
            }
        }

        /// <summary>
        /// Extracts skeleton information from fbx file
        /// </summary>
        /// <returns></returns>
        public IOSkeleton GetSkeleton()
        {
            // search all limb nodes
            var limbs = _document.GetNodesByName("Model").Where(e => e.Properties.Count >= NodeDescSize && e.Properties[NodeDescSize - 1].ToString().Contains("Limb"));

            // map the id to the newly create bone
            Dictionary<string, IOBone> idToBone = new Dictionary<string, IOBone>();

            // convert nodes to bones
            foreach(var l in limbs)
                idToBone.Add(l.Value.ToString(), CreateBoneFromNode(l));

            // create io skeleton
            var skeleton = new IOSkeleton();

            // make connections
            foreach (var v in idToBone)
            {
                if (Connections.ContainsKey(v.Key) && Connections[v.Key].Count > 0 && idToBone.ContainsKey(Connections[v.Key][0]))
                    idToBone[Connections[v.Key][0]].AddChild(v.Value);
                else
                    skeleton.RootBones.Add(v.Value);
            }

            return skeleton;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        private IOBone CreateBoneFromNode(FbxNode l)
        {
            IOBone bone = new IOBone()
            {
                Name = GetNameWithoutNamespace(l.Properties[NodeDescSize - 2].ToString())
            };

            // scan and extract info from properties
            var properties = l.GetNodesByName("P");
            if (properties.Length == 0)
                properties = l.GetNodesByName("Property");

            Quaternion preRot = new Quaternion(0, 0, 0, 1);
            Quaternion postRot = new Quaternion(0, 0, 0, 1);

            foreach (var prop in properties)
            {
                if (prop.Properties.Count < PropertyDescSize)
                    continue;
                
                if (prop.Properties[0].ToString() == "PreRotation")
                    preRot = MathExt.FromEulerAngles(
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize]),
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize + 1]),
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize + 2]));

                if (prop.Properties[0].ToString() == "PostRotation")
                    postRot = MathExt.FromEulerAngles(
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize]),
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize + 1]),
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize + 2]));
                
                if (prop.Properties[0].ToString() == "Lcl Translation")
                    bone.Translation = new Vector3((float)(double)prop.Properties[PropertyDescSize], (float)(double)prop.Properties[PropertyDescSize + 1], (float)(double)prop.Properties[PropertyDescSize + 2]);

                if (prop.Properties[0].ToString() == "Lcl Rotation")
                    bone.RotationEuler = new Vector3(
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize]),
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize + 1]),
                        MathExt.DegToRad((float)(double)prop.Properties[PropertyDescSize + 2]));

                if (prop.Properties[0].ToString() == "Lcl Scaling")
                    bone.Scale = new Vector3((float)(double)prop.Properties[PropertyDescSize], (float)(double)prop.Properties[PropertyDescSize + 1], (float)(double)prop.Properties[PropertyDescSize + 2]);

            }

            bone.Rotation = preRot * bone.Rotation * postRot;

            return bone;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IOMesh> ExtractMesh()
        {
            // gather mesh objects
            var mesh = _document.GetNodesByName("Model").Where(e => e.Properties.Count >= NodeDescSize && e.Properties[NodeDescSize - 1].ToString() == "Mesh");

            List<IOMesh> meshes = new List<IOMesh>();

            var subdeformers = _document.GetNodesByName("SubDeformer");

            // extract geometry information into iomesh
            foreach (var m in mesh)
            {
                // generate mesh
                IOMesh iomesh = new IOMesh()
                {
                    Name = GetNameWithoutNamespace(m.Properties[NodeDescSize - 2].ToString())
                };
                meshes.Add(iomesh);


                // reverse search material
                var material = "";
                var materials = GetChildConnections(m).Find(e => e.Name == "Material");
                if (materials != null)
                    material = GetNameWithoutNamespace(materials.Properties[NodeDescSize - 2].ToString());


                // collect geometry information
                // in older fbx this was stored in the mesh node
                // in newer versions it's in its own node

                var geoms = _document.GetNodesByName("Geometry").Where(e => IsParentedTo(e.Properties[0].ToString(), m.Properties[0].ToString())).ToArray();

                if(geoms.Length > 0)
                {
                    foreach (var g in geoms)
                    {
                        ProcessGeometry(g, out IOPolygon poly, out List<IOVertex> verts);
                        for (int i = 0; i < poly.Indicies.Count; i++)
                            poly.Indicies[i] += iomesh.Vertices.Count;
                        iomesh.Vertices.AddRange(verts);
                        poly.MaterialName = material;
                        iomesh.Polygons.Add(poly);
                    }
                }
                else
                {
                    ProcessGeometry(m, out IOPolygon poly, out List<IOVertex> verts);
                    poly.MaterialName = material;
                    iomesh.Vertices = verts;
                    iomesh.Polygons.Add(poly);
                }

                // get transform for this node
                var tra = CreateBoneFromNode(m);
                iomesh.TransformVertices(tra.WorldTransform);
            }


            return meshes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<FbxNode> GetChildConnections(FbxNode m)
        {
            List<FbxNode> nodes = new List<FbxNode>();

            foreach (var con in Connections)
            {
                if (con.Value.Contains(m.Value.ToString()))
                {
                    var node = _document.GetNodesByValue(con.Key);

                    if(node != null)
                        nodes.AddRange(node);
                }
            }
            
            return nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ProcessGeometry(FbxNode node, out IOPolygon poly, out List<IOVertex> verts)
        {
            double[] vertices;
            int[] indices;

            // load vertices
            if (node["Vertices"].Value is double[])
                vertices = (double[])node["Vertices"].Value;
            else
                vertices = node["Vertices"].Properties.Select(e=>(double)e).ToArray();


            // load vertex indices
            if (node["PolygonVertexIndex"].Value is int[])
                indices = (int[])node["PolygonVertexIndex"].Value;
            else
                indices = node["PolygonVertexIndex"].Properties.Select(e => (int)e).ToArray();

            
            // get binds
            var deformers = GetChildConnections(node).Where(e=>e.Name == "Deformer");

            List<Dictionary<int, Tuple<float, string>>> deforms = new List<Dictionary<int, Tuple<float, string>>>();

            foreach (var par in deformers)
            {
                foreach(var sub in GetChildConnections(par))
                {
                    // get bone name
                    var name = "";
                    foreach (var mod in GetChildConnections(sub))
                        if(mod.Name == "Model")
                            name = GetNameWithoutNamespace(mod.Properties[NodeDescSize - 2].ToString());

                    if (!string.IsNullOrEmpty(name) && sub["Indexes"] != null)
                    {
                        // create deform map
                        Dictionary<int, Tuple<float, string>> deformmap = new Dictionary<int, Tuple<float, string>>();

                        // indices
                        int[] ind;
                        if (sub["Indexes"].Value is int[])
                            ind = (int[])sub["Indexes"].Value;
                        else
                            ind = sub["Indexes"].Properties.Select(e => (int)e).ToArray();

                        // weights
                        float[] weights;
                        if (sub["Weights"].Value is double[])
                            weights = ((double[])sub["Weights"].Value).Select(e => (float)e).ToArray();
                        else
                            weights = sub["Weights"].Properties.Select(e => (float)(double)e).ToArray();
                        
                        // generate map
                        for(int i = 0; i < weights.Length; i++)
                        {
                            deformmap.Add(ind[i], new Tuple<float, string>(weights[i], name));
                        }

                        // add deform entry
                        deforms.Add(deformmap);
                    }
                }
            }

            // generate polygon
            poly = new IOPolygon();
            verts = new List<IOVertex>();

            poly.PrimitiveType = IOPrimitive.TRIANGLE;
            
            // TODO: triangle strips detection and processing
            // for now assuming always triangluated
            for (int i = 0; i < indices.Length; i++)
            {
                var idx1 = indices[i];
                if(idx1 < 0) idx1 = Math.Abs(idx1) - 1;

                IOVertex v = new IOVertex()
                {
                    Position = new Vector3((float)vertices[idx1 * 3], (float)vertices[idx1 * 3 + 1], (float)vertices[idx1 * 3 + 2])
                };

                // find deforms
                foreach(var map in deforms)
                {
                    if (map.ContainsKey(idx1))
                    {
                        v.Envelope.Weights.Add(new Core.IOBoneWeight()
                        {
                            Weight = map[idx1].Item1,
                            BoneName = map[idx1].Item2,
                        });
                    }
                }

                poly.Indicies.Add(i);
                verts.Add(v);
            }


            // get layer information
            ProcessLayer(node, "Normal", indices, verts);
            ProcessLayer(node, "Tangent", indices, verts);
            ProcessLayer(node, "Binormal", indices, verts);
            ProcessLayer(node, "UV", indices, verts);
            ProcessLayer(node, "Color", indices, verts);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        private string GetLayerNodeName(string layer)
        {
            switch (layer)
            {
                case "Normal":
                case "Tangent":
                case "Binormal":
                case "Color":
                    return layer + "s";
            }
            return layer;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ProcessLayer(FbxNode node, string layer, int[] indices, List<IOVertex> verts)
        {
            var normalLayers = node.GetNodesByName($"LayerElement{layer}");

            foreach(var lay in normalLayers)
            {
                // layer index
                var index = (int)lay.Properties[0];


                // get data
                var dataName = GetLayerNodeName(layer);
                double[] data;
                if (lay[$"{dataName}"].Value is double[])
                    data = (double[])lay[$"{dataName}"].Value;
                else
                    data = lay[$"{dataName}"].Properties.Select(e => (double)e).ToArray();
                

                // indexed
                Dictionary<int, int> indexToDirect = new Dictionary<int, int>();
                if (lay["ReferenceInformationType"].Properties[0].ToString() == "IndexToDirect")
                {
                    int[] ind;
                    if (lay[$"{layer}Index"].Value is int[])
                        ind = (int[])lay[$"{layer}Index"].Value;
                    else
                        ind = lay[$"{layer}Index"].Properties.Select(e => (int)e).ToArray();

                    for (int i = 0; i < ind.Length; i++)
                        indexToDirect.Add(i, ind[i]);
                }


                // process map
                for (int i = 0; i < indices.Length; i++)
                {
                    var idx = i;

                    switch (lay["MappingInformationType"].Properties[0].ToString())
                    {
                        case "ByVertice":
                            idx = indices[i];
                            if (idx < 0) idx = Math.Abs(idx) - 1;
                            break;
                        case "ByPolygonVertex":
                            break;
                    }

                    if (indexToDirect.ContainsKey(idx))
                        idx = indexToDirect[idx];

                    switch (layer)
                    {
                        case "Normal":
                            verts[i].Normal = new Vector3((float)data[idx * 3], (float)data[idx * 3 + 1], (float)data[idx * 3 + 2]);
                            break;
                        case "Tangent":
                            verts[i].Tangent = new Vector3((float)data[idx * 3], (float)data[idx * 3 + 1], (float)data[idx * 3 + 2]);
                            break;
                        case "Binormal":
                            verts[i].Binormal = new Vector3((float)data[idx * 3], (float)data[idx * 3 + 1], (float)data[idx * 3 + 2]);
                            break;
                        case "UV":
                            verts[i].SetUV((float)data[idx * 2], (float)data[idx * 2 + 1], index);
                            break;
                        case "Color":
                            verts[i].SetColor((float)data[idx * 4], (float)data[idx * 4 + 1], (float)data[idx * 4 + 2], (float)data[idx * 4 + 3], index);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<IOMaterial> GetMaterials()
        {
            List<IOMaterial> materials = new List<IOMaterial>();
            
            foreach(var m in _document.GetNodesByName("Material"))
            {
                // generate material
                IOMaterial mat = new IOMaterial()
                {
                    Name = GetNameWithoutNamespace(m.Properties[NodeDescSize - 2].ToString())
                };

                // load material attributes
                if(m["ShadingModel"] != null && m["ShadingModel"].Properties[0].ToString().ToLower() == "phong")//ToLower Because it's sometimes "Phong" 
                {
                    var properties = m.GetNodesByName("P");
                    if (properties.Length == 0)
                        properties = m.GetNodesByName("Property");

                    foreach (var prop in properties)
                    {
                        switch (prop.Properties[0].ToString())
                        {
                            case "AmbientColor":
                                mat.AmbientColor = GetColor(prop);
                                break;
                            case "DiffuseColor":
                                mat.DiffuseColor = GetColor(prop);
                                break;
                            case "SpecularColor":
                                mat.SpecularColor = GetColor(prop);
                                break;
                            case "ReflectionColor":
                                mat.ReflectiveColor = GetColor(prop);
                                break;
                            case "Emissive":
                                mat.EmissionColor = GetColor(prop);
                                break;
                            case "Shininess":
                                mat.Shininess = (float)(double)prop.Properties[PropertyDescSize];
                                break;
                            case "Opacity":
                                mat.Alpha = (float)(double)prop.Properties[PropertyDescSize];
                                break;
                        }
                    }
                }

                // get textures

                var children = GetChildConnections(m).Where(e=>e.Name == "Texture");

                foreach(var t in children)
                {
                    // the property is stored in the connections
                    // for now always map it to diffuse

                    mat.DiffuseMap = new IOTexture()
                    {
                        Name = t.Properties[NodeDescSize - 2].ToString(),
                        FilePath = t["FileName"].Properties[0].ToString()
                    };
                }

                materials.Add(mat);
            }

            return materials;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        private Vector4 GetColor(FbxNode prop)
        {
            return new Vector4((float)(double)prop.Properties[PropertyDescSize], (float)(double)prop.Properties[PropertyDescSize + 1], (float)(double)prop.Properties[PropertyDescSize + 2], 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetNameWithoutNamespace(string name)
        {
            name = Regex.Match(name.ToString(), @"(?<=(::))(.*)").Value;

            var match = Regex.Match(name, @"(.*)(?=\.[0-9][0-9][0-9])");
            if (match.Success)
                name = match.Value;

            return name;
        }

        /// <summary>
        /// Returns true if the child id is parented to the parent id in some way
        /// </summary>
        /// <param name="ChildID"></param>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        private bool IsParentedTo(string ChildID, string ParentID)
        {
            if (Connections.ContainsKey(ChildID))
            {
                foreach (var connection in Connections[ChildID])
                {
                    if (connection == ParentID || IsParentedTo(connection, ParentID))
                        return true;
                }
            }

            return false;
        }
    }
}
