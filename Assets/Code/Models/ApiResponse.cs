using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Models
{
    [Serializable]
    public class ApiResponse
    {
        public string type;
        public string title;
        public int status;
        public Dictionary<string, string[]> errors;
    }
}
