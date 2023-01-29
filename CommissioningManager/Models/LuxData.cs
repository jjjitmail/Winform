using CommissioningManager.Attributes;
using CommissioningManager.Converters;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommissioningManager.Models
{
    [Table("Ziut_ImportLightData")]
    public class LuxData
    {
        [NotMapped]
        [Conditions(ReadOnly = true)]
        public int _ID { get; set; }

        [NotMapped]
        [Conditions(ReadOnly = true)]
        public int ANL_ID { get; set; }

        [Column("OrganizationName", TypeName = "nvarchar")]
        [Conditions(Compare = true, Required = true, MaxLength = 40)]
        public string OrganizationName { get; set; }

        [Column("ProjectNumber", TypeName = "nvarchar")]
        [Conditions(MaxLength = 20, ReadOnly =true )]
        public string ProjectNumber { get; set; }

        [NotMapped]
        [Conditions(MaxLength = 40, ReadOnly = true)]
        public string Town { get; set; }

        [Column("Suburb", TypeName = "nvarchar")]
        [Conditions(MaxLength = 40)]
        public string Suburb { get; set; }

        [Column("Street", TypeName = "nvarchar")]
        [Conditions(Required = true, MaxLength = 40, ReadOnly =true)]
        public string Street { get; set; }

        [Column("MastNumber", TypeName = "nvarchar")]
        [Conditions(Required = true, MaxLength = 20, ReadOnly = true)]
        public string Mastnumber { get; set; }

        [Column("LuminaireNumber", TypeName = "nvarchar")]
        [Conditions(Required = true, MaxLength = 20, ReadOnly = true)]
        public string LuminaireNumber { get; set; }

        [Column("Latitude", TypeName = "nvarchar")]
        [Conditions(Compare = true, Required = true, ConverterType = typeof(NLLatitudeConverter), MaxLength = 20, ReadOnly = true)]
        public string Latitude { get; set; }

        [Column("Longitude", TypeName = "nvarchar")]
        [Conditions(Compare = true, Required = true, ConverterType = typeof(NLLongitudeConverter), MaxLength = 20, ReadOnly = true)]
        public string Longitude { get; set; }

        [Key]
        [Column("LuminaireID", TypeName = "nvarchar")]
        [Conditions(Compare = true, Required = true, MaxLength = 40, ReadOnly = true)]
        public string LuminaireID { get; set; }

        [Column("ObjectOwnedBy", TypeName = "nvarchar")]
        [Conditions(Required = true, MaxLength = 20)]
        public string ObjectOwnedBy { get; set; }

        [Column("Description", TypeName = "nvarchar")]
        [Conditions(MaxLength = 80, ReadOnly = true)]
        [JsonIgnore]
        public string Description { get; set; }

        [Column("Processed", TypeName = "bit")]
        [JsonIgnore]
        [Conditions(ReadOnly = true)]
        public bool Processed { get; set; }
    }
}