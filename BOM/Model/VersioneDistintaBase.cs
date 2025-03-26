namespace BOM.Model
{
    public class VersioneDistintaBase
    {
        public int Id { get; set; }     // Primary Key
        public int IdProduct { get; set; } // Foreign Key
        public string Version { get; set; }
        public DateTime CreationTime { get; set; }

        // Relationship
        public Item Product { get; set; } // Reference to Item
    }
}
