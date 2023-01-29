using System;

namespace CommissioningManager.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Property)]
    public class Conditions : Attribute
    {
        private bool _Compare;
        public bool Compare
        {
            get { return _Compare; }
            set { _Compare = value; }
        }

        private bool _CompareRequired;
        public bool CompareRequired
        {
            get { return _CompareRequired; }
            set { _CompareRequired = value; }
        }

        private bool _CompareRequiredSame;
        public bool CompareRequiredSame
        {
            get { return _CompareRequiredSame; }
            set { _CompareRequiredSame = value; }
        }

        private bool _CompareRequiredSameField;
        public bool CompareRequiredSameField
        {
            get { return _CompareRequiredSameField; }
            set { _CompareRequiredSameField = value; }
        }

        private bool _Required;
        public bool Required
        {
            get { return _Required; }
            set { _Required = value; }
        }

        private Type _ExpectedType;
        public Type ExpectedType
        {
            get { return _ExpectedType; }
            set { _ExpectedType = value; }
        }

        private int _MaxLength;
        public int MaxLength
        {
            get { return _MaxLength; }
            set { _MaxLength = value; }
        }

        private bool _ReadOnly;
        public bool ReadOnly
        {
            get { return _ReadOnly; }
            set { _ReadOnly = value; }
        }

        private string _DisplayName;
        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }

        private Type _ConverterType;
        public Type ConverterType
        {
            get { return _ConverterType; }
            set { _ConverterType = value; }
        }
    }
}