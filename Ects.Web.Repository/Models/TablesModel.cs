using System.Collections.Generic;

namespace Ects.Web.Repository.Models
{
    public class TablesModel
    {
        public class TableModel
        {
            public RowModel Header { get; set; }

            public IList<RowModel> Body { get; set; }

            public string Name { get; set; }

            public TableModel()
            {
                Body = new List<RowModel>();
            }
        }

        public class RowModel
        {
            public IList<CellModel> Cells { get; set; }

            public double Height { get; set; }

            public RowModel()
            {
                Cells = new List<CellModel>();
            }

            public CellModel AddCell()
            {
                var cell = new CellModel();
                Cells.Add(cell);
                return cell;
            }
        }

        public class CellModel
        {
            public string Value { get; set; }

            public int Colspan { get; set; }

            public int Rowspan { get; set; }

            public CellModel()
            {
                Value = string.Empty;
                Colspan = 1;
                Rowspan = 1;
            }
        }

        public IList<TableModel> Tables { get; set; }

        public TablesModel()
        {
            Tables = new List<TableModel>();
        }

        public TableModel Add()
        {
            var tableModel = new TableModel();
            Tables.Add(tableModel);
            return tableModel;
        }
    }
}