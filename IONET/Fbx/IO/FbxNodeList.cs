using System.Collections.Generic;

namespace IONET.Fbx.IO
{
	/// <summary>
	/// Base class for nodes and documents
	/// </summary>
	public abstract class FbxNodeList
	{
		/// <summary>
		/// The list of child/nested nodes
		/// </summary>
		/// <remarks>
		/// A list with one or more null elements is treated differently than an empty list,
		/// and represented differently in all FBX output files.
		/// </remarks>
		public List<FbxNode> Nodes { get; } = new List<FbxNode>();

		/// <summary>
		/// Gets a named child node
		/// </summary>
		/// <param name="name"></param>
		/// <returns>The child node, or null</returns>
		public FbxNode this[string name] { get { return Nodes.Find(n => n != null && n.Name == name); } }

		/// <summary>
		/// Gets a child node, using a '/' separated path
		/// </summary>
		/// <param name="path"></param>
		/// <returns>The child node, or null</returns>
		public FbxNode GetRelative(string path)
		{
			var tokens = path.Split('/');
			FbxNodeList n = this;
			foreach (var t in tokens)
			{
				if (t == "")
					continue;
				n = n[t];
				if (n == null)
					break;
			}
			return n as FbxNode;
		}

        /// <summary>
        /// Returns all sub-nodes with name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FbxNode[] GetNodesByValue(string value)
        {
            List<FbxNode> nodes = new List<FbxNode>();

            foreach (var node in Nodes)
            {
                if (node == null) continue;
                nodes.AddRange(node.GetNodesByValue(node, value));
            }

            return nodes.ToArray();
        }

        /// <summary>
        /// Returns all sub-nodes with name
        /// </summary>
        /// <returns></returns>
        private List<FbxNode> GetNodesByValue(FbxNode node, string value, List<FbxNode> c = null)
        {
            if (c == null)
                c = new List<FbxNode>();

            if (node == null)
                return c;

            if (node.Value != null && node.Value.ToString().Equals(value))
                c.Add(node);

            foreach (var child in node.Nodes)
                GetNodesByValue(child, value, c);

            return c;
        }

        /// <summary>
        /// Returns all sub-nodes with name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public FbxNode[] GetNodesByName(string name)
        {
            List<FbxNode> nodes = new List<FbxNode>();

            foreach(var node in Nodes)
            {
                if (node == null) continue;
                nodes.AddRange(node.GetNodesByName(node, name));
            }

            return nodes.ToArray();
        }

        /// <summary>
        /// Returns all sub-nodes with name
        /// </summary>
        /// <returns></returns>
        private List<FbxNode> GetNodesByName(FbxNode node, string name, List<FbxNode> c = null)
        {
            if (c == null)
                c = new List<FbxNode>();

            if (node == null)
                return c;

            if (node.Name.Equals(name))
                c.Add(node);

            foreach (var child in node.Nodes)
                GetNodesByName(child, name, c);

            return c;
        }
    }
}
