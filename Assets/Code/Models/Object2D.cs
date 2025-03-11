using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Models
{
    [Serializable]
    public class Object2D
    {
        public int Id;
        public int EnvironmentId;
        public int PrefabId;
        public float PositionX;
        public float PositionY;
        public float scaleX;
        public float scaleY;
        public float RotationZ;
        public int SortingLayer;
    }
}
