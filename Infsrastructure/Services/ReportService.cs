using Infrastructure.Models;
using Infrastructure.Pdf;
using Infrastructure.Pdf.Extensions;
using Infrastructure.Pdf.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Infrastructure.Services
{
    public class ReportService
    {
        public string GeneratePdfReport(IEnumerable<OrderModel> orders, DateTime startDate, DateTime endDate)
        {
            using var stream = new MemoryStream();
            var doc = new Document();
            var writer = PdfWriter.GetInstance(doc, stream);

            doc.Open();

            doc.NewPage();

            var size = 100 / 6.0f;
            var sizes = new float[6];
            Array.Fill(sizes, size);

            doc.AddParagraph($"Report for the period {startDate.Date.ToShortDateString()} - {endDate.Date.ToShortDateString()}", PdfDefaultValues.CenterAligmentParagraphFormat);
            doc.AddTable(GetTableModel(orders),  sizes);

            doc.Close();
            writer.Close();

            var dir = Path.Combine(Directory.GetCurrentDirectory(), "Reports");

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var path = Path.Combine(dir, $"report_{DateTime.Now.ToString("dd-MM-yyyy_HH-mm")}.pdf");
            File.WriteAllBytes(path, stream.ToArray());

            return path;
        }

        private TableModel GetTableModel(IEnumerable<OrderModel> orders)
        {
            var tableModel = new TableModel(1, 6)
            {
                Header = "Orders"
            };

            var format = PdfDefaultValues.LeftAligmentCellFormat with { Font = PdfDefaultValues.BoldFont };

            tableModel[0][0] = new CellModel("Order", format);
            tableModel[0][1] = new CellModel("OrderTime", format);
            tableModel[0][2] = new CellModel("Dish", format);
            tableModel[0][3] = new CellModel("Dish type", format);
            tableModel[0][4] = new CellModel("Amount", format);
            tableModel[0][5] = new CellModel("Cost", format);
            tableModel.SetRowBackgroundColor(0, BaseColor.GRAY);

            FillData(tableModel, orders);

            return tableModel;
        }

        private void FillData(TableModel tableModel, IEnumerable<OrderModel> orders)
        {
            var color = BaseColor.LIGHT_GRAY;
            var secondColor = new BaseColor(204, 209, 209);

            foreach(var order in orders) 
            {
                AddOrder(tableModel, order, color);

                color = color == BaseColor.LIGHT_GRAY ? secondColor : BaseColor.LIGHT_GRAY;
            }

            tableModel.AddRow();
            tableModel.LastRow[0] = new CellModel("Total cost")
            {
                Colspan = 5
            };

            tableModel.LastRow[5] = new CellModel(orders.Sum(x => x.TotalCost).ToString("0.##"));
            tableModel.SetRowBackgroundColor(tableModel.RowsCount - 1, BaseColor.GRAY);
        }

        private void AddOrder(TableModel table, OrderModel order, BaseColor color)
        {
            var dishes = order.Dishes;
            table.AddRow();

            table.LastRow[0] = new CellModel(order.Name)
            {
                Rowspan = dishes.Count() + 1
            };

            table.LastRow[1] = new CellModel($"{order.Date.ToShortDateString()} {order.Date.ToShortTimeString()}")
            {
                Rowspan = dishes.Count() + 1
            };

            foreach(var dish in dishes)
            {
                table.LastRow[2] = new CellModel(dish.Name);
                table.LastRow[3] = new CellModel(dish.Type);
                table.LastRow[4] = new CellModel(dish.Amount.ToString("0.##"));
                table.LastRow[5] = new CellModel((dish.Cost * (decimal)dish.Amount).ToString("0.##"));
                table.SetRowBackgroundColor(table.RowsCount - 1, color);
                table.AddRow();
            }

            table.LastRow[2] = new CellModel("Order cost")
            {
                Colspan = 3
            };

            table.LastRow[5] = new CellModel(order.TotalCost.ToString());
            table.SetRowBackgroundColor(table.RowsCount - 1, color);
        }
    }
}
