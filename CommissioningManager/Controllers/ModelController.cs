using CommissioningManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommissioningManager.Controllers
{
    public class ModelController<T>  where T : IModel<T>
    {
        public static List<T> TypeList { get; set; }
        
        public static void Init()
        {
            if (TypeList == null) 
            {
                TypeList = new List<T>();
            }
            var _model = TypeList.FirstOrDefault(x => x.GetType() == typeof(T));
            if (_model == null) 
            {
                var model = Activator.CreateInstance<T>();
                TypeList.Add(model);
                ThisModel = model;
            }
            else
            {
                ThisModel = _model;
            }
        }

        public static IModel<T> ThisModel { get; set; }

        public static IModel<T> LoadModel<Model>(DataControl control, bool preFillQuery = false) where Model : IModel<T>
        {
            Init();
            ThisModel.DataControl = control;
            var model = ThisModel.ReadFiles();
            if (preFillQuery)
            {
                model.PreFillQuery();
            }
            return model;
        }
    }
}
