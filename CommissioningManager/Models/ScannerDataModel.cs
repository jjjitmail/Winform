using CommissioningManager.Controls;
using CommissioningManager.Interfaces;
using CommissioningManager.Models.Base;
using Newtonsoft.Json;
using System.IO;

namespace CommissioningManager.Models
{
    public class ScannerDataModel : SourceModel<ScannerData>, IModel<ScannerDataModel>
    {
        public override FileType FileType => FileType.ScanData;

        public ScannerDataModel() : base() { }

        internal override Model InternalProcess<Model>()
        {
            var thisModel = new Model();

            var model = Process(file =>
            {                
                thisModel.DataList = JsonConvert.DeserializeObject<SortableBindingList<ScannerData>>(File.ReadAllText(file.FullName));
                return thisModel;
            });
            return model();
        }

        public ScannerDataModel ReadFiles()
        {
            return InternalProcess<ScannerDataModel>();
        }
    }
}