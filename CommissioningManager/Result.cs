using CommissioningManager.Controls;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;

namespace CommissioningManager
{
    public class Result
    {
        public Result()
        {
            Error = new Exception("");
            DataStyle = new DataStyle();
        }

        public List<DbEntityValidationResult> DbEntityValidationResults { get; set; }

        public Exception Error { get; set; }

        private string _Message;
        public string Message {
            get
            {
                if (!string.IsNullOrEmpty(Error.Message))
                {
                    return Error.Message;
                }
                return _Message;
            }
            set { _Message = value; }
        }

        public DataStyle DataStyle { get; set; }
    }
}