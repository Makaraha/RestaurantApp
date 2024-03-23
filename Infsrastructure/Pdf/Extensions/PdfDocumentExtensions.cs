using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Pdf.Enums;
using Infrastructure.Pdf.Models;
using Infrastructure.Pdf.Util;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Infrastructure.Pdf.Extensions
{
    public static class PdfDocumentExtensions
    {
        public static void AddParagraph(this Document document, string text, ParagraphFormatModel format)
        {
            var paragraph = ParagraphFactory.CreateParagraph(text, format);
            document.Add(paragraph);
        }

        public static void AddTable(this Document document, TableModel model, float[] relativeWidths)
        {
            var table = new PdfPTable(relativeWidths)
            {
                SpacingBefore = model.SpacingBefore,
                SpacingAfter = model.SpacingAfter
            };

            AddHeader(table, model);

            for (int i = 0; i < model.RowsCount; i++)
            {
                for (int j = 0; j < model.ColumnsCount; j++)
                {
                    var cell = MapCellModelToPdfCell(model[i][j], model);
                    if(cell != null)
                        table.AddCell(cell);
                }
            }
            document.Add(table);
        }

        private static void AddHeader(PdfPTable table, TableModel model)
        {
            if (string.IsNullOrEmpty(model.Header)) return;
            var cellModel = new CellModel(model.Header, model.HeaderFormat);
            cellModel.Colspan = model.ColumnsCount;

            var cell = MapCellModelToPdfCell(cellModel, model);
            cell.PaddingTop = PdfDefaultValues.Space20;
            cell.PaddingBottom = PdfDefaultValues.Space20;
            cell.Border = Rectangle.NO_BORDER;
            var cells = new PdfPCell[model.ColumnsCount];
            cells[0] = cell;

            table.Rows.Add(new PdfPRow(cells));
        }

        private static PdfPCell? MapCellModelToPdfCell(CellModel? cellModel, TableModel tableModel)
        {
            if (cellModel == null)
                return null;

            var cell = new PdfPCell(cellModel.Text)
            {
                UseAscender = true,
                VerticalAlignment = (int)cellModel.Format.VerticalAligment,
                HorizontalAlignment = (int)cellModel.Format.HorizontalAligment,
                Colspan = cellModel.Colspan,
                Rowspan = cellModel.Rowspan,
                BackgroundColor = cellModel.BackgroundColor
            };

            if (cellModel.Format.HorizontalAligment == HorizontalAlignment.Left)
                cell.PaddingLeft = cellModel.Format.HorizontalPadding;
            if (cellModel.Format.HorizontalAligment == HorizontalAlignment.Right)
                cell.PaddingRight = cellModel.Format.HorizontalPadding;

            cell.PaddingTop = cellModel.Format.VerticalPadding;
            cell.PaddingBottom = cellModel.Format.VerticalPadding;
            if (!tableModel.IsBordersVisible)
                cell.Border = Rectangle.NO_BORDER;

            return cell;
        }
    }
}
