using System.Collections.Generic;

namespace Art.Web.Client.Models
{
    public class TablesModel
    {
        public TablesModel()
        {
            Tables = new List<TableModel>();
        }

        public IList<TableModel> Tables { get; set; }

        public TableModel Add()
        {
            var tableModel = new TableModel();
            Tables.Add(tableModel);
            return tableModel;
        }

        public class TableModel
        {
            public TableModel()
            {
                Body = new List<RowModel>();
            }

            public RowModel Header { get; set; }

            public IList<RowModel> Body { get; set; }

            public string Name { get; set; }
        }

        public class RowModel
        {
            public RowModel()
            {
                Cells = new List<CellModel>();
            }

            public IList<CellModel> Cells { get; set; }

            public double Height { get; set; }

            public CellModel AddCell()
            {
                var cell = new CellModel();
                Cells.Add(cell);
                return cell;
            }
        }

        public class CellModel
        {
            public CellModel()
            {
                Value = string.Empty;
                Colspan = 1;
                Rowspan = 1;
            }

            public string Value { get; set; }

            public int Colspan { get; set; }

            public int Rowspan { get; set; }
        }
    }
}
