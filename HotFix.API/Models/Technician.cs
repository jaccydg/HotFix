namespace HotFix.API.Models
{
//CREATE TABLE technicians (
//    id SERIAL PRIMARY KEY,
//    name VARCHAR(100) NOT NULL,
//    specialization VARCHAR(100) NOT NULL,
//    experience INTEGER NOT NULL,
//    availability BOOLEAN NOT NULL DEFAULT TRUE
//);
    public class Technician
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public int Experience { get; set; }
        public bool Availability { get; set; }
    }
}
