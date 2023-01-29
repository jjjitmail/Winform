using CommissioningManager.Interfaces;
using System;
using System.ComponentModel;

namespace CommissioningManager.Converters
{
    public class TextBooleanConverter : IConverter
    {
        public object Convert(object value)
        {
            int returnValue = 0;
            if (value == null)
                return returnValue;
            var newValue = value.ToString().Trim().ToLower();

            if (newValue == "yes" || newValue == "ja" || newValue == "1" || newValue == "true")
                returnValue = 1;
            else if (newValue == "0" || newValue == "false" || newValue == "nee" || newValue == "no")
                returnValue = 0;
            else
                throw new Exception();
            return returnValue;
        }
    }
}