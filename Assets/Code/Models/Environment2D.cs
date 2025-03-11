using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Models
{
    [Serializable]
    public class Environment2D
    {
        [JsonProperty("environmentId")]
        public int Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("maxLenght")]
        public int MaxLenght;

        [JsonProperty("maxHeight")]
        public int MaxHeight;

    }
}
