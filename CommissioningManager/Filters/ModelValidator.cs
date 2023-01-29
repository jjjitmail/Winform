using CommissioningManager.Attributes;
using CommissioningManager.Controls;
using CommissioningManager.Interfaces;
using CommissioningManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommissioningManager.Filters
{
    public class ModelValidator<T> : ISource where T : class
    {
        public List<Result> ResultList { get; set; }
        
        public static Model Validate<Model>(Model model) where Model : SourceModel<T>
        {
            model.ResultList = new List<Result>();
            DataStyle DataStyleForCompareSame = new DataStyle();
            
            model.DataList = model.DataControl.DataGridView.DataSource as SortableBindingList<T>;
            
            var dataListAfterCompare = model.DataList.Distinct(new ModelComparer<T>());
            var doubleDataList = model.DataList.Except(dataListAfterCompare);

            var dataListAfterCompareSame = model.DataList.Distinct(new SameComparer<T>());
            var doubleSameDataList = model.DataList.Except(dataListAfterCompareSame);

            var dataListAfterCompareSameField = model.DataList.Distinct(new SameFieldComparer<T>());
            var doubleSameFieldDataList = model.DataList.Except(dataListAfterCompareSameField);

            if (doubleDataList.Count() > 0)
            {
                model.DoubleDataList = new List<T>();
                model.DoubleDataList = doubleDataList;

                model.ResultList.Add(new Result { Message = DateTime.Now + ": (warning) Double records found, see highlighted cells" });                
            }

            if (doubleSameDataList.Count() > 0)
            {
                model.DoubleSameDataList = doubleSameDataList;
                model.InvalidDoubleSameDataList = new List<T>();
                model.InvalidDoubleSameDataList = doubleSameDataList.Except(doubleSameFieldDataList);
            }
            
            for (int i = 0; i < model.DataList.Count; i++)
            {
                var data = model.DataList[i];
                var PropertyInfoList = data.GetType().GetProperties()
                .Where(z => z.GetCustomAttributes(typeof(Conditions), false).Count() > 0);

                DataStyle DataStyle = new DataStyle();

                for (int j = 0; j < PropertyInfoList.Count(); j++)
                {
                    var item = PropertyInfoList.ToList()[j];
                    var obj = item.GetCustomAttributes(typeof(Conditions), false);

                    var value = data.GetType().GetProperty(item.Name)?.GetValue(data);

                    if ((obj.First() as Conditions).Required)
                    {
                        if (data.GetType().GetProperty(item.Name) == null)
                        {
                            model.ResultList.Add(new Result
                            {
                                Error = new Exception(DateTime.Now + ": (error) '" + item.Name + "' is not found")
                                ,
                                DataStyle = new DataStyle { RowIndex = new List<int>() { i }, CellIndex = new List<int>() { j } }
                            });
                        }
                        else
                        {
                            if (value == null || String.IsNullOrEmpty(value?.ToString().Trim()))
                            {
                                model.ResultList.Add(new Result
                                {
                                    Error = new Exception(DateTime.Now + ": (error) '" + item.Name + "' is required")
                                    ,
                                    DataStyle = new DataStyle { RowIndex = new List<int>() { i }, CellIndex = new List<int>() { j } }
                                });
                            }
                            else
                            {
                                DataStyle.Value += value.ToString().Trim();
                                DataStyle.RowIndex.Add(i);
                                DataStyle.CellIndex.Add(j);
                            }
                        }
                    }
                    //twee keer of meer mastnr - waarschuwing en omschrijving is verplicht
                    if ((obj.First() as Conditions).CompareRequired)
                    {
                        var cellvalue = model.DataControl.DataGridView.Rows[i].Cells[j].Value;
                        
                        if (cellvalue == null || string.IsNullOrWhiteSpace(cellvalue?.ToString()))
                        {
                            // if (DataStyle.RowIndex.Any(x => x == i) && HasDoubleValue(model.DoubleDataList?.ToList(), data, RequiredValue))
                            if (HasDoubleValue(model.DoubleDataList?.ToList(), data, CompareValue))
                            {
                                model.ResultList.Add(new Result
                                {
                                    Error = new Exception(DateTime.Now + ": (error) '" + item.Name + "' is required, see highlighted red cell(s) ")
                                ,
                                    DataStyle = new DataStyle { RowIndex = new List<int>() { i }, CellIndex = new List<int>() { j } }
                                });
                            }
                        }
                    }

                    if ((obj.First() as Conditions).CompareRequiredSame)
                    {
                        if (model.InvalidDoubleSameDataList?.Count() > 0)
                        {
                            var foundOne = model.InvalidDoubleSameDataList.FirstOrDefault(x => RequiredSameValue(x) == RequiredSameValue(data));
                            if (foundOne != null)
                            {
                                model.ResultList.Add(new Result
                                {
                                    Error = new Exception(DateTime.Now + ": (error) '" + item.Name + "' should have same child value, see highlighted red cell(s) ")
                                ,
                                    DataStyle = new DataStyle { RowIndex = new List<int>() { i }, CellIndex = new List<int>() { j } }
                                });
                            }
                        }
                    }

                    if ((obj.First() as Conditions).CompareRequiredSameField)
                    {
                        if (model.InvalidDoubleSameDataList?.Count() > 0)
                        {
                            var foundOne = model.InvalidDoubleSameDataList.FirstOrDefault(x => RequiredSameValue(x) == RequiredSameValue(data));
                            if (foundOne != null)
                            {
                                model.ResultList.Add(new Result
                                {
                                    Error = new Exception(DateTime.Now + ": (error) '" + item.Name + "' has different value, see highlighted red cell(s) ")
                                ,
                                    DataStyle = new DataStyle { RowIndex = new List<int>() { i }, CellIndex = new List<int>() { j } }
                                });
                            }
                        }
                    }

                    if (value != null && !string.IsNullOrEmpty(value.ToString()))
                    {
                        if ((obj.First() as Conditions).ConverterType != null)
                        {
                            Type type = (obj.First() as Conditions).ConverterType;

                            try
                            {
                                var typeValue = ((IConverter)Activator.CreateInstance(type)).Convert(value);
                                data.GetType().GetProperty(item.Name).SetValue(data, typeValue.ToString());
                            }
                            catch (Exception err)
                            {
                                model.ResultList.Add(new Result
                                {
                                    Error = new Exception(DateTime.Now + ": (error) type of '" + item.Name + "' is not correct, conversion was failed) Details: " + err.Message)
                                ,
                                    DataStyle = new DataStyle { RowIndex = new List<int>() { i }, CellIndex = new List<int>() { j } }
                                });
                            }
                        }
                        else if ((obj.First() as Conditions).MaxLength > 0)
                        {
                            int maxLenght = (obj.First() as Conditions).MaxLength;
                            if (value.ToString().Length > maxLenght)
                            {
                                data.GetType().GetProperty(item.Name).SetValue(data, value.ToString().Trim());
                            }
                            if (value.ToString().Length > maxLenght)
                            {
                                model.ResultList.Add(new Result
                                {
                                    Error = new Exception(DateTime.Now + ": (error) 'MaxLength' of '" + item.Name + "' is expected to be '" + maxLenght + "', but 'Length' of '" + value.ToString().Length + "'('" + value.ToString() + "') is found")
                                    ,
                                    DataStyle = new DataStyle { RowIndex = new List<int>() { i }, CellIndex = new List<int>() { j } }
                                });
                            }
                        }
                        if ((obj.First() as Conditions).ExpectedType != null)
                        {
                            Type type = (obj.First() as Conditions).ExpectedType;

                            if (value.GetType() != type)
                            {
                                try
                                {
                                    Convert.ChangeType(value, type);
                                }
                                catch (Exception err)
                                {
                                    model.ResultList.Add(new Result
                                    {
                                        Error = new Exception(DateTime.Now + ": (error) '" + type + "' of '" + item.Name + "' is expected, but '" + value + "'('" + value.GetType() + "') is found")
                                        ,
                                        DataStyle = new DataStyle { RowIndex = new List<int>() { i }, CellIndex = new List<int>() { j } }
                                    });
                                }
                            }
                        }
                    }
                }
                if (model.ProgressBar.Value < model.ProgressBar.Maximum)
                    model.ProgressBar.Value++;
            }
            return model;
        }

        private static string RequiredValue(T data)
        {
            var PropertyInfoList = data.GetType().GetProperties()
                .Where(z => z.GetCustomAttributes(typeof(Conditions), false).Count() > 0);
            var value = "";
            for (int j = 0; j < PropertyInfoList.Count(); j++)
            {
                var item = PropertyInfoList.ToList()[j];
                var obj = item.GetCustomAttributes(typeof(Conditions), false);
                if ((obj.First() as Conditions).Required)
                {
                    value += data.GetType().GetProperty(item.Name)?.GetValue(data);
                }
            }
            return value;
        }

        private static string CompareValue(T data)
        {
            var PropertyInfoList = data.GetType().GetProperties()
                .Where(z => z.GetCustomAttributes(typeof(Conditions), false).Count() > 0);
            var value = "";
            for (int j = 0; j < PropertyInfoList.Count(); j++)
            {
                var item = PropertyInfoList.ToList()[j];
                var obj = item.GetCustomAttributes(typeof(Conditions), false);
                if ((obj.First() as Conditions).Compare)
                {
                    value += data.GetType().GetProperty(item.Name)?.GetValue(data);
                }
            }
            return value;
        }

        private static string RequiredSameValue(T data)
        {
            var PropertyInfoList = data.GetType().GetProperties()
                .Where(z => z.GetCustomAttributes(typeof(Conditions), false).Count() > 0);
            var value = "";
            for (int j = 0; j < PropertyInfoList.Count(); j++)
            {
                var item = PropertyInfoList.ToList()[j];
                var obj = item.GetCustomAttributes(typeof(Conditions), false);
                if ((obj.First() as Conditions).CompareRequiredSame)
                {
                    value += data.GetType().GetProperty(item.Name)?.GetValue(data);
                }
            }
            return value;
        }

        private static string RequiredSameFieldValue(T data)
        {
            var PropertyInfoList = data.GetType().GetProperties()
                .Where(z => z.GetCustomAttributes(typeof(Conditions), false).Count() > 0);
            var value = "";
            for (int j = 0; j < PropertyInfoList.Count(); j++)
            {
                var item = PropertyInfoList.ToList()[j];
                var obj = item.GetCustomAttributes(typeof(Conditions), false);
                if ((obj.First() as Conditions).CompareRequiredSameField)
                {
                    value += data.GetType().GetProperty(item.Name)?.GetValue(data);
                }
            }
            return value;
        }

        private static bool HasDoubleValue(List<T> list, T dataValue)
        {
            if (list == null)
                return false;
            var datavalue = RequiredValue(dataValue);

            for (int i = 0; i < list.Count; i++)
            {
                var newValue = RequiredValue(list[i]);
                if (datavalue == newValue)
                    return true;
            }
            return false;
        }

        private static bool HasDoubleValue(List<T> list, T dataValue, Func<T, string> func)
        {
            if (list == null)
                return false;
            var datavalue = func(dataValue);

            for (int i = 0; i < list.Count; i++)
            {
                var newValue = func(list[i]);
                if (datavalue == newValue)
                    return true;
            }
            return false;
        }
    }
}