using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography.Xml;

namespace HotFix.API.Models
{
// CREATE TABLE interventions (
//    id SERIAL PRIMARY KEY,
//    intervention_date DATE NOT NULL,
//    intervention_time TIME NOT NULL,
//    technician_id INTEGER NOT NULL,
//    request_id INTEGER NOT NULL,
//    FOREIGN KEY(technician_id) REFERENCES technicians(id),
//    FOREIGN KEY(request_id) REFERENCES requests(id)
//);
    public class Intervention
    {
        public int Id { get; set; }
        public DateTime InterventionDate { get; set; }
        public int TechnicianId { get; set; }
        public int RequestId { get; set; }
    }
}
