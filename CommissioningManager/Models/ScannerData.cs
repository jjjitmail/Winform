using CommissioningManager.Attributes;
using CommissioningManager.Converters;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommissioningManager.Models
{
    [Table("Ziut_LightControllerAssociation")]
    public class ScannerData
    {
        [Key]
        [Column("AssetID", TypeName = "nvarchar")]
        [Conditions(MaxLength = 40, DisplayName = "AssetID", ReadOnly = true, Required = true, CompareRequiredSame = true, CompareRequiredSameField = true)]
        public string RFID { get; set; }

        [NotMapped]
        [JsonIgnore]
        [Conditions(MaxLength = 40)]
        public string Client { get; set; }

        [Column("MastNumber", TypeName = "nvarchar")]
        [Conditions(Compare = true, Required = true, MaxLength = 40, DisplayName = "MastNumber", CompareRequiredSameField = true)]
        public string LamppostNumber { get; set; }

        [Column("OrganizationName", TypeName = "nvarchar")]
        [Conditions(Required = true, MaxLength = 40)]
        public string OrganizationName { get; set; }

        [Column("Street", TypeName = "nvarchar")]
        [Conditions(MaxLength = 40)]
        public string Street { get; set; }

        [Column("Latitude", TypeName = "nvarchar")]
        [Conditions(Required = true, MaxLength = 20, ConverterType = typeof(NLLatitudeConverter))]
        public string Latitude { get; set; }

        [Column("Longitude", TypeName = "nvarchar")]
        [Conditions(Required = true, MaxLength = 20, ConverterType = typeof(NLLongitudeConverter))]
        public string Longitude { get; set; }
 
        [Column("Description", TypeName = "nvarchar")]
        [Conditions(MaxLength = 40, CompareRequired = true)]
        public string Description { get; set; }

        [Column("SwapUnit", TypeName = "bit")]
        [Conditions(ConverterType = typeof(TextBooleanConverter))]
        public object Replacement { get; set; }

        [Column("ErrorText", TypeName = "nvarchar")]
        [Conditions(MaxLength = 80)]
        [JsonIgnore]
        public string ErrorText { get; set; }
    }
}