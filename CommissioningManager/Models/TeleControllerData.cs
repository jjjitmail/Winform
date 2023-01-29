using CommissioningManager.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommissioningManager.Models
{
    [Table("Ziut_ImportLoraTeleControllerData")]
    public class TeleControllerData
    {
        [Column("DevEUI", TypeName = "nvarchar")]
        [Conditions(Compare = true, Required = true, MaxLength = 255, ReadOnly = true)]
        public string DevEUI { get; set; }

        [Key]
        [Column("ApplicationKey", TypeName = "nvarchar")]
        [Conditions(Compare = true, Required = true, MaxLength = 255, ReadOnly = true)]
        public string ApplicationKey { get; set; }

        [Column("NetworkKey", TypeName = "nvarchar")]
        [Conditions(Compare = true, Required = true, MaxLength = 255, ReadOnly = true)]
        public string NetworkKey { get; set; }

        [Column("DevAddress", TypeName = "nvarchar")]
        [Conditions(Compare = true, Required = true, MaxLength = 255, ReadOnly = true)]
        public string DevAddress { get; set; }

        [Column("AssetID", TypeName = "nvarchar")]
        [Conditions(Compare = true, Required = true, MaxLength = 255, ReadOnly = true)]
        public string AssetID { get; set; }

        [Column("VersionHardware", TypeName = "nvarchar")]
        [Conditions(Compare = true, Required = true, MaxLength = 255, ReadOnly = true)]
        public string VersionHardware { get; set; }

        [Column("VersionSoftware", TypeName = "nvarchar")]
        [Conditions(Compare = true, Required = true, MaxLength = 255, ReadOnly = true)]
        public string VersionSoftware { get; set; }

        [JsonIgnore]
        [Column("Processed", TypeName = "bit")]
        [Conditions(ReadOnly = true)]
        public bool? Processed { get; set; }
    }
}