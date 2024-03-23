using iTextSharp.text;

namespace Infrastructure.Pdf.Models
{
    public class CellModel
    {
        public CellModel(string text)
        {
            Format = PdfDefaultValues.LeftAligmentCellFormat;
            Text = new Phrase(text, Format.Font);
        }

        public CellModel(string text, CellFormatModel format)
        {
            Text = new Phrase(text, format.Font);
            Format = format;
        }

        public Phrase Text { get; set; }

        public int Colspan { get; set; } = 1;

        public int Rowspan { get; set; } = 1;

        public BaseColor BackgroundColor { get; set; } = BaseColor.WHITE;

        public CellFormatModel Format { get; set; }

        public void AddNewLineParahraph(string text)
        {
            AddNewLineParahraph(text, Format.Font);
        }

        public void AddNewLineParahraph(string text, Font font)
        {
            Text.Add(new Phrase('\n' + text, font));
        }
    }
}
