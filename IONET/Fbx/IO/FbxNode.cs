using System.Collections.Generic;
using System.Linq;

namespace IONET.Fbx.IO
{
	/// <summary>
	/// Represents a node in an FBX file
	/// </summary>
	public class FbxNode : FbxNodeList
	{
		/// <summary>
		/// The node name, which is often a class type
		/// </summary>
		/// <remarks>
		/// The name must be smaller than 256 characters to be written to a binary stream
		/// </remarks>
		public string Name { get; set; }

		/// <summary>
		/// The list of properties associated with the node
		/// </summary>
		/// <remarks>
		/// Supported types are primitives (apart from byte and char),arrays of primitives, and strings
		/// </remarks>
		public List<object> Properties { get; } = new List<object>();

		/// <summary>
		/// The first property element
		/// </summary>
		public object Value
		{
			get { return Properties.Count < 1 ? null : Properties[0]; }
			set
			{
				if (Properties.Count < 1)
					Properties.Add(value);
				else
					Properties[0] = value;
			}
		}

        /// <summary>
        /// Whether the node is empty of data
        /// </summary>
        public bool IsEmpty => string.IsNullOrEmpty(Name) && Properties.Count == 0 && Nodes.Count == 0;


        /// <summary>
        /// Whether the node is empty of data
        /// </summary>
        public FbxNode()
        {
        }
        /// <summary>
        /// Whether the node is empty of data
        /// </summary>
        public FbxNode(string Name)
        {
            this.Name = Name;
        }
        /// <summary>
        /// Whether the node is empty of data
        /// </summary>
        public FbxNode(string Name, object Property)
        {
            this.Name = Name;
            Properties.Add(Property);
        }
        /// <summary>
        /// Whether the node is empty of data
        /// </summary>
        public FbxNode(string Name, object[] Properties)
        {
            this.Name = Name;
            this.Properties.AddRange(Properties);
        }

        /// <summary>
        /// Returns all immediate child nodes with given name
        /// </summary>
        /// <returns></returns>
        public List<FbxNode> GetChildrenWithName(string name)
        {
            return Nodes.Where(e => e != null && e.Name == name).ToList();
        }

    }
}
