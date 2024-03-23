using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Pdf.Enums;
using Infrastructure.Pdf.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Infrastructure.Pdf
{
    public static class PdfDefaultValues
    {
        public static Font DefaultFont => GetFont(11);

        public static Font BoldFont => GetFont( 11, Font.BOLD);

        public static Font SmallItalicFont => GetFont(9, Font.ITALIC);

        public static float DefaultVerticalPadding => 5f;

        public static float DefaultHorizontalPadding => 5f;

        public static float Space30 => 30f;

        public static float Space20 => 20f;

        public static float Space10 => 10f;

        public static int TableWidthInSymbols => 75;

        public static int KeepTogetherRowsCount => 10;

        public static float LeftMargin => 36;

        public static float RightMargin => 36;

        public static float TopMargin => 36;

        public static float BottomMargin => 50;

        public static CellFormatModel LeftAligmentCellFormat => new CellFormatModel()
        {
            VerticalPadding = DefaultVerticalPadding,
            HorizontalPadding = DefaultHorizontalPadding,
            VerticalAligment = VerticalAlignment.Center,
            HorizontalAligment = HorizontalAlignment.Left,
            Font = DefaultFont
        };

        public static CellFormatModel CenterAligmentCellFormat => new CellFormatModel()
        {
            VerticalPadding = DefaultVerticalPadding,
            HorizontalPadding = DefaultHorizontalPadding,
            VerticalAligment = VerticalAlignment.Center,
            HorizontalAligment = HorizontalAlignment.Center,
            Font = DefaultFont
        };

        public static CellFormatModel RightAligmentCellFormat => new CellFormatModel()
        {
            VerticalPadding = DefaultVerticalPadding,
            HorizontalPadding = DefaultHorizontalPadding,
            VerticalAligment = VerticalAlignment.Center,
            HorizontalAligment = HorizontalAlignment.Right,
            Font = DefaultFont
        };

        public static ParagraphFormatModel HeaderFormatWithSpacingBefore => new ParagraphFormatModel()
        {
            HorizontalAligment = HorizontalAlignment.Center,
            Font = BoldFont,
            SpacingBefore = Space30,
            SpacingAfter = Space20
        };

        public static ParagraphFormatModel DefaultParagraphFormat => new ParagraphFormatModel()
        {
            HorizontalAligment = HorizontalAlignment.Left,
            Font = DefaultFont
        };

        public static ParagraphFormatModel CenterAligmentParagraphFormat => new ParagraphFormatModel()
        {
            HorizontalAligment = HorizontalAlignment.Center,
            Font = DefaultFont
        };

        public static ParagraphFormatModel HeaderFormat => new ParagraphFormatModel()
        {
            HorizontalAligment = HorizontalAlignment.Center,
            Font = BoldFont,
            SpacingAfter = Space20
        };

        public static Font GetFont(int size, int style = -1, string fontName = "Arial", string filename = "Arial.TTF")
        {
            if (!FontFactory.IsRegistered(fontName))
            {
                var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\" + filename;
                FontFactory.Register(fontPath);
            }
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, size, style);
        }
    }
}
