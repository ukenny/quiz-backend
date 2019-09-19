using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quiz_backend.Models
{
    public class Credentials
    {
        [JsonProperty("email")]
        internal string Email { get; set; }
        [JsonProperty("password")]
        internal string Password { get; set; }
    }
}
