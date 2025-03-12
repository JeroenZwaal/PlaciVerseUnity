using Newtonsoft.Json;
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
        [JsonProperty("objectId")]
        public int ObjectId;

        [JsonProperty("environmentId")]
        public int EnvironmentId;

        [JsonProperty("prefabId")]
        public int PrefabId;

        [JsonProperty("positionX")]
        public float PositionX;

        [JsonProperty("positionY")]
        public float PositionY;

        [JsonProperty("scaleX")]
        public float ScaleX;

        [JsonProperty("scaleY")]
        public float ScaleY;

        [JsonProperty("rotationZ")]
        public float RotationZ;

        [JsonProperty("sortingLayer")]
        public int SortingLayer;
    }
}
