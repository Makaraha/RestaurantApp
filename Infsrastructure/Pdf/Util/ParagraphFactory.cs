using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Pdf.Models;
using iTextSharp.text;

namespace Infrastructure.Pdf.Util
{
    public static class ParagraphFactory
    {
        public static Paragraph CreateParagraph(string text, ParagraphFormatModel format)
        {
            var paragraph = new Paragraph(text, format.Font);
            paragraph.Alignment = (int)format.HorizontalAligment;
            paragraph.SpacingBefore = format.SpacingBefore;
            paragraph.SpacingAfter = format.SpacingAfter;
            return paragraph;
        }
    }
}
