using CommissioningManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommissioningManager.Interfaces
{
    public interface IModel<T>
    {
        DataControl DataControl { get; set; }
        T ReadFiles();
        void PreFillQuery(bool value = true);
        void RegisterEvents(DashBoard dashboard, Action<bool> action);
    }
}
