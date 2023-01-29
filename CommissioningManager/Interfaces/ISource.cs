using System.Collections.Generic;

namespace CommissioningManager.Interfaces
{
    public interface ISource
    {
        List<Result> ResultList { get; set; }
    }
}
