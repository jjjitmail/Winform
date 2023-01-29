using CommissioningManager.Controls;
using CommissioningManager.Interfaces;
using CommissioningManager.Models.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace CommissioningManager.Models
{
    public class LuxDataModel : SourceModel<LuxData>, IModel<LuxDataModel>
    {
        private string LuxDataSelectQuery = "SELECT * FROM ResultTable";
        public override FileType FileType => FileType.LuxData;

        public LuxDataModel() : base() { }

        internal override Model InternalProcess<Model>()
        {
            string strAccessConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Inputs\";
            var thisModel = new Model();

            var model = Process(file =>
            {
                using (OleDbConnection myAccessConn = new OleDbConnection(strAccessConn + file + ";"))
                {
                    myAccessConn.Open();

                    using (DataSet ds = new DataSet())
                    using (OleDbCommand myAccessCommand = new OleDbCommand(LuxDataSelectQuery, myAccessConn))
                    using (OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand))
                    {
                        myDataAdapter.Fill(ds, "ResultTable");
                        string json = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented);
                        thisModel.DataList = JsonConvert.DeserializeObject<SortableBindingList<LuxData>>(json);
                    }
                }
                return thisModel;
            });
            return model();
        }

        public LuxDataModel ReadFiles()
        {
            var model = InternalProcess<LuxDataModel>();
            return model;
        }
    }
}