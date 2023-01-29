using System;
using System.Collections;
using System.Reflection;

namespace CommissioningManager.Attributes
{
    public class StringValueAttribute : Attribute
    {
        private string _value;

        public StringValueAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }        
    }   
}
