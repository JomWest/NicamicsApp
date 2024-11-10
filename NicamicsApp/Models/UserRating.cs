using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Models
{
    public class UserRating
    {
        public string userId { get; set; } = null!;

        public int rating { get; set; }
    }
}
