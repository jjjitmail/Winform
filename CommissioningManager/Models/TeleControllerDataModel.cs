using CommissioningManager.Controls;
using CommissioningManager.Interfaces;
using CommissioningManager.Models.Base;
using Newtonsoft.Json;
using System.IO;

namespace CommissioningManager.Models
{
    public class TeleControllerDataModel : SourceModel<TeleControllerData>, IModel<TeleControllerDataModel>
    {
        public override FileType FileType => FileType.TeleControllerData;

        public TeleControllerDataModel(): base() { }

        internal override Model InternalProcess<Model>()
        {
            var thisModel = new Model();

            var model = Process(file =>
            {
                thisModel.DataList = JsonConvert.DeserializeObject<SortableBindingList<TeleControllerData>>(File.ReadAllText(file.FullName));
                return thisModel;
            });
            return model();
        }

        public TeleControllerDataModel ReadFiles()
        {
            return InternalProcess<TeleControllerDataModel>();
        }
    }
}