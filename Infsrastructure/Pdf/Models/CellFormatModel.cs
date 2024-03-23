using Infrastructure.Pdf.Enums;
using iTextSharp.text;

namespace Infrastructure.Pdf.Models
{
    public record CellFormatModel
    {
        public VerticalAlignment VerticalAligment { get; set; } = VerticalAlignment.Center;

        public HorizontalAlignment HorizontalAligment { get; set; } = HorizontalAlignment.Left;

        public float VerticalPadding { get; set; } = PdfDefaultValues.DefaultVerticalPadding;

        public float HorizontalPadding { get; set; } = PdfDefaultValues.DefaultHorizontalPadding;

        public Font Font { get; set; } = PdfDefaultValues.DefaultFont;
    }
}
