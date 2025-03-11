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
        public int Id;

        [JsonProperty("environmentId")]
        public int EnvironmentId;

        [JsonProperty("prefabId")]
        public int PrefabId;

        [JsonProperty("positionX")]
        public float PositionX;

        [JsonProperty("positionY")]
        public float PositionY;

        [JsonProperty("scaleX")]
        public float scaleX;

        [JsonProperty("scaleY")]
        public float scaleY;

        [JsonProperty("rotationZ")]
        public float RotationZ;

        [JsonProperty("sortingLayer")]
        public int SortingLayer;
    }
}
