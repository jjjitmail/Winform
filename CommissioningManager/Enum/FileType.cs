using CommissioningManager.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommissioningManager
{
    public enum FileType
    {
        [StringValue("*.accdb")]
        LuxData,
        [StringValue("*.icf")]
        ScanData,
        [StringValue("*.jsn")]
        TeleControllerData
    }
}
