using static System.Net.Mime.MediaTypeNames;
using System.Net;

namespace HotFix.API.Models
{
// CREATE TABLE requests (
//    id SERIAL PRIMARY KEY,
//    description TEXT NOT NULL,
//    address TEXT NOT NULL,
//    type VARCHAR(100) NOT NULL
//);
    public class Request
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
    }
}
