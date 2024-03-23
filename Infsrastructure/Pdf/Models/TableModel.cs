using iTextSharp.text;

namespace Infrastructure.Pdf.Models
{
    public class TableModel
    {
        private readonly CellFormatModel _headerFormat;
        private List<CellModel?[]> _table;

        public TableModel(int rows, int columns)
        {
            _headerFormat = PdfDefaultValues.CenterAligmentCellFormat;
            _headerFormat.Font = PdfDefaultValues.BoldFont;

            ColumnsCount = columns;
            _table = new List<CellModel?[]>();
            for (int i = 0; i < rows; i++)
                AddRow();
        }

        public int RowsCount => _table.Count;

        public readonly int ColumnsCount;

        public float SpacingBefore { get; set; } = PdfDefaultValues.Space20;

        public float SpacingAfter { get; set; } = 0f;

        public bool IsBordersVisible { get; set; } = true;

        public string Header { get; set; } = string.Empty;

        public CellFormatModel HeaderFormat => _headerFormat;

        public CellModel?[] LastRow => _table.Last();

        public CellModel?[] this[int i]
        {
            get { return _table[i]; }
        }

        public void AddRow()
        {
            _table.Add(new CellModel?[ColumnsCount]);
            var row = _table.Last();
            for (int i = 0; i < ColumnsCount; i++)
                row[i] = null;
        }

        public void SetRowBackgroundColor(int row, BaseColor color)
        {
            foreach (var cell in _table[row])
            {
                if(cell != null)
                    cell.BackgroundColor = color;
            }
        }
    }
}
