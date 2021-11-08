using System.Collections.Generic;

namespace Sham
{
    class JointedMeshSkeleton
    {
        public class JMSHeader
        {
            public int Version = -1;
            public int NodeCount = -1;
            public List<Node> Nodes = new List<Node>();
            public int MaterialCount = -1;
            public List<Material> Materials = new List<Material>();

            public const int NodeLineLength = 4;
            public const int ShaderLineLength = 2;
        }

        public struct Material
        {
            public string Name;
            public int MaterialSlotIndex;
            public string Permutation;
            public string Region;
        }

        public struct Node
        {
            public string NodeName;
            public int ParentNodeIndex;
            public Quaternion Rotation;
            public Vector3 Location;
        }

        public struct Vector3
        {
            public float x;
            public float y;
            public float z;
        }

        public struct Quaternion
        {
            public float x;
            public float y;
            public float z;
            public float w;
        }
    }
}
