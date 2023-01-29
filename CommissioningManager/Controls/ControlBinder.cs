using CommissioningManager.Attributes;
using CommissioningManager.Interfaces;
using CommissioningManager.Models.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CommissioningManager.Controls
{    
    public class ControlBinder<T> : ISource where T : class
    {
        public List<Result> ResultList { get; set; }
        
        public static void UpdateGridCells<Model>(Model model) where Model : SourceModel<T>
        {
            if (model.DataList == null)
                model.DataList = new SortableBindingList<T>();

            var hasDouble = model.DoubleDataList?.Any();

            var hasDoubleSame = model.DoubleSameDataList?.Any();

            var randInts = GetRandomNumbers(model.DataList.Count() + 20);
            List<DataStyle> DataStyles = new List<DataStyle>();

            for (int i = 0; i < model.DataList.Count(); i++)
            {
                var subPros = model.DataList[i].GetType().GetProperties().Where(z => z.GetCustomAttributes(typeof(Conditions), false).Count() > 0);

                DataStyle DataStyle = new DataStyle();
                
                for (int j = 0; j < subPros.Count(); j++)
                {
                    model.DataControl.DataGridView.Rows[i].Cells[j].Style.BackColor = Color.White;
                    var obj = subPros.Skip(j).First();
                    var objAttr = obj.GetCustomAttributes(typeof(Conditions), false);
                    if (objAttr.Count() > 0)
                    {
                        if (((Conditions)objAttr[0]).Compare)
                        {
                            DataStyle.Value += obj.GetValue(model.DataList[i])?.ToString().Trim();
                            DataStyle.RowIndex.Add(i);
                            DataStyle.CellIndex.Add(j);
                        }
                        if (model.ResultList != null && model.ResultList.Where(x => x.DataStyle.RowIndex.Any(y => y == i) && x.DataStyle.CellIndex.Any(z => z == j)).Any())
                        {
                            model.DataControl.DataGridView.Rows[i].Cells[j].Style.BackColor = Color.Red;
                        }
                        if (((Conditions)objAttr[0]).MaxLength > 0)
                        {
                            model.DataControl.DataGridView.Rows[i].Cells[j].Value = model.DataControl.DataGridView.Rows[i].Cells[j].Value?.ToString().Trim();
                            if (model.DataControl.DataGridView.Rows[i].Cells[j].Value?.ToString().Length > ((Conditions)objAttr[0]).MaxLength)
                            {
                                model.DataControl.DataGridView.Rows[i].Cells[j].Style.BackColor = Color.Red;
                            }
                        }
                        if (((Conditions)objAttr[0]).ReadOnly)
                        {
                            model.DataControl.DataGridView.Rows[i].Cells[j].ReadOnly = true;
                            if (model.DataControl.DataGridView.Rows[i].Cells[j].Style.BackColor == Color.White)
                            {
                                model.DataControl.DataGridView.Rows[i].Cells[j].Style.BackColor = Color.LightGray;
                            }
                        }
                        if (((Conditions)objAttr[0]).DisplayName != null)
                        {
                            model.DataControl.DataGridView.Columns[j].HeaderText = ((Conditions)objAttr[0]).DisplayName;
                        }
                    }
                }
                // highlight repeated data
                if (hasDouble.HasValue)
                {
                    var dataStyles = DataStyles.Where(x => x.Value == DataStyle.Value && !string.IsNullOrEmpty(x.Value));
                    if (dataStyles.Count() > 0)
                    {
                        int FirstInt = 256;
                        int SecondInt = 256;
                        int ThirdInt = 256;
                        if (i > 250)
                        {
                            FirstInt = randInts.Skip(250).First();
                            SecondInt = randInts.Skip(251).First();
                            ThirdInt = randInts.Skip(252).First();
                        }
                        else
                        {
                            FirstInt = randInts.Skip(i).First();
                            SecondInt = randInts.Skip(i + 1).First();
                            ThirdInt = randInts.Skip(i + 2).First();
                        }
                        DataStyle.Color = Color.FromArgb(FirstInt, SecondInt, ThirdInt);

                        for (int z = 0; z < dataStyles.Count(); z++)
                        {
                            var rowIndex = dataStyles.Skip(z).First().RowIndex.First();
                            for (int y = 0; y < dataStyles.Skip(z).First().CellIndex.Count; y++)
                            {
                                var cellIndex = dataStyles.Skip(z).First().CellIndex.Skip(y).First();
                                model.DataControl.DataGridView.Rows[rowIndex].Cells[cellIndex].Style.BackColor = DataStyle.Color;
                                model.DataControl.DataGridView.Rows[rowIndex].Cells[cellIndex].Style.ForeColor = ContrastColor(DataStyle.Color);
                            }
                        }

                        for (int s = 0; s < DataStyle.CellIndex.Count; s++)
                        {
                            model.DataControl.DataGridView.Rows[DataStyle.RowIndex.First()].Cells[DataStyle.CellIndex.Skip(s).First()].Style.BackColor = DataStyle.Color;
                            model.DataControl.DataGridView.Rows[DataStyle.RowIndex.First()].Cells[DataStyle.CellIndex.Skip(s).First()].Style.ForeColor = ContrastColor(DataStyle.Color);
                        }
                    }
                }
                DataStyles.Add(DataStyle);
                if (model.ProgressBar.Value < model.ProgressBar.Maximum)
                    model.ProgressBar.Value++;
            }
        }

        public static Color ContrastColor(Color color)
        {
            int d = 0;

            double a = 1 - (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;

            if (a < 0.5)
                d = 0;
            else
                d = 255;

            return Color.FromArgb(d, d, d);
        }

        private static IEnumerable<int> GetRandomNumbers(int numberOfRandoms)
        {
            if (numberOfRandoms > 255)
                numberOfRandoms = 255;
            Random rnd = new Random();
            var unique = new HashSet<int>();
            do
            {
                int num = rnd.Next(1, 256);
                if (unique.Contains(num))
                    continue;
                unique.Add(num);
            }
            while (unique.Count < numberOfRandoms);
            return unique.ToList();
        }

        public static void DataGridView<Model>(Model model) where Model : SourceModel<T>
        {
            model.DataControl.DataGridView.DataSource = model.DataList;
            UpdateGridCells(model);
        }
    }
}