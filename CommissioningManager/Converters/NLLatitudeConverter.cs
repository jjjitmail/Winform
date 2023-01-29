﻿using CommissioningManager.Interfaces;
using System;
using System.Globalization;

namespace CommissioningManager.Converters
{
    public class NLLatitudeConverter : IConverter
    {
        public object Convert(object obj)
        {
            CultureInfo EnglishCulture = new CultureInfo("en-EN");

            double value;
            if (obj == null)
                throw new Exception();
            try
            {
                if (double.TryParse(obj.ToString().Replace(",", "."), NumberStyles.Float, EnglishCulture, out value))
                {
                    if (value < 50 || value > 54)
                        throw new Exception();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            return value;
        }
    }
}
