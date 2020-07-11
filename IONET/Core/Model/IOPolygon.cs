using System.Collections.Generic;

namespace IONET.Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public enum IOPrimitive
    {
        TRIANGLE,
        QUAD,
        TRISTRIP,
        TRIFAN,
        POINT,
        LINE,
        LINESTRIP
    }

    public class IOPolygon
    {
        /// <summary>
        /// 
        /// </summary>
        public IOPrimitive PrimitiveType { get; set; } = IOPrimitive.TRIANGLE;

        /// <summary>
        /// 
        /// </summary>
        public string MaterialName { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public List<int> Indicies = new List<int>();

        /// <summary>
        /// 
        /// </summary>
        public void ToTriangles(IOMesh parentMesh)
        {
            switch (PrimitiveType)
            {
                case IOPrimitive.QUAD:
                    Indicies = QuadToList(Indicies);
                    PrimitiveType = IOPrimitive.TRIANGLE;
                    break;

                case IOPrimitive.TRIFAN:
                    Indicies = FanToList(Indicies);
                    PrimitiveType = IOPrimitive.TRIANGLE;
                    break;

                case IOPrimitive.TRISTRIP:
                    Indicies = StripToList(Indicies, parentMesh.Vertices);
                    PrimitiveType = IOPrimitive.TRIANGLE;
                    break;

                case IOPrimitive.TRIANGLE:
                case IOPrimitive.LINE:
                case IOPrimitive.LINESTRIP:
                case IOPrimitive.POINT:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<int> QuadToList(List<int> input)
        {
            var output = new List<int>();

            for (int i = 0; i < input.Count; i += 4)
            {
                output.Add(input[i]);
                output.Add(input[i + 1]);
                output.Add(input[i + 2]);

                output.Add(input[i + 2]);
                output.Add(input[i + 3]);
                output.Add(input[i]);
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<int> FanToList(List<int> input)
        {
            var output = new List<int>();

            var center = input[0];

            for (int i = 1; i < input.Count; i++)
            {
                output.Add(center);
                output.Add(input[i]);
                output.Add(input[i + 1]);
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<int> StripToList(List<int> input, List<IOVertex> vertices)
        {
            var output = new List<int>();

            for (int index = 2; index < input.Count; index++)
            {
                bool isEven = (index % 2 != 1);

                var vert1 = input[index - 2];
                var vert2 = isEven ? input[index] : input[index - 1];
                var vert3 = isEven ? input[index - 1] : input[index];

                if (!vertices[vert1].Position.Equals(vertices[vert2].Position) &&
                    !vertices[vert2].Position.Equals(vertices[vert3].Position) && 
                    !vertices[vert3].Position.Equals(vertices[vert1].Position))
                {
                    output.Add(vert3);
                    output.Add(vert2);
                    output.Add(vert1);
                }
            }

            return output;
        }
    }
}
