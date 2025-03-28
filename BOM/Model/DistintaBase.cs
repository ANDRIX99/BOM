using System.ComponentModel.DataAnnotations.Schema;

namespace BOM.Model
{
    public class DistintaBase
    {
        public int Id { get; set; } // Primary Key
        public int VersioneDistintaBaseId { get; set; } // Foreign Key
        public int FiglioId { get; set; } // Foreign Key

        [Column(TypeName = "decimal(18, 3)")]
        public decimal Amount { get; set; }

        // Relationship
        public VersioneDistintaBase VersioneDistintaBase { get; set; }  // Reference to version BOM
        public Item Figlio { get; set; }    // Reference to Item
    }
}
