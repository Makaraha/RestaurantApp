using Infrastructure.Pdf.Enums;
using iTextSharp.text;

namespace Infrastructure.Pdf.Models
{
    public class ParagraphFormatModel
    {
        public HorizontalAlignment HorizontalAligment { get; set; } = HorizontalAlignment.Center;

        public Font Font { get; set; } = PdfDefaultValues.DefaultFont;

        public float SpacingAfter { get; set; } = 0f;

        public float SpacingBefore { get; set; } = 0f;
    }
}
