using Microcharts;
using NicamicsApp.Models.ReporteModels;
using NicamicsApp.Service;
using SkiaSharp;

namespace NicamicsApp.ReportesMongo
{



    public partial class ReporteComicsMasVendidos : ContentPage
    {
        private readonly ReporteService _reporteService;

        public ReporteComicsMasVendidos(ReporteService reporteService)
        {
            InitializeComponent();
            _reporteService = reporteService;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await LoadBarChart();
            await LoadPieChart();
            await LoadLineChart();

        }

        private async Task LoadBarChart()
        {
            string startDate = "2024-07-10";
            string endDate = "2024-10-14";

            DateRangeLabel.Text = $"Período: {startDate} - {endDate}";

            var reportData = await _reporteService.GetComicsMasVendidosAsync("670c05ca1c5dec5b0d11566e", startDate, endDate);

            var totalVentas = await _reporteService.GetVentasTotalesPorFecha("670c05ca1c5dec5b0d11566e", startDate, endDate);

            var entries = reportData.Select(r => new ChartEntry((float)r.TotalVenta)
            {
                Label = r.NombreComic,
                ValueLabel = $"{Math.Round(r.TotalVenta, 2)} C$",
                Color = SKColor.Parse("#2c3e50") // Color de las barras
            }).ToList();

            var chart = new BarChart
            {
                Entries = entries,
                LabelTextSize = 40, 
                ValueLabelTextSize = 40, 
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
                Margin = 50, 
            };

            BarChartView.Chart = chart;

            lblTotalVentas.Text = $"{Math.Round(totalVentas, 2)} C$";

        }

        private async Task LoadPieChart()
        {
            var vendedorId = "670c05ca1c5dec5b0d11566e"; // Reemplaza con el ID del vendedor real
            var startDate = "2024-07-10"; // Rango de fechas de inicio
            var endDate = "2024-10-14"; // Rango de fechas de fin

            var ventasData = await _reporteService.GetVentasPorCategoriaAsync(vendedorId, startDate, endDate);

            var entries = ventasData.Select(v => new ChartEntry((float)v.TotalVenta)
            {
                Label = v.Categoria,
                ValueLabel = $"{Math.Round(v.TotalVenta, 2)} C$",
                Color = SKColor.Parse("#" + (new Random().Next(0x1000000)).ToString("X6")) // Generar color aleatorio
            }).ToList();

            var chart = new PieChart
            {
                Entries = entries,
                LabelTextSize = 40, 
                Margin = 50,
            };

            PieChartView.Chart = chart;
        }

        private async Task LoadLineChart()
        {

            var comidId = "670c06a01c5dec5b0d11566f";
            var año = 2024;

            var ventasData = await _reporteService.GetTotalVentasComicPorMes(comidId, año);

            lblComicName.Text = ventasData.First().NombreComic;

            var entries = ventasData.Select(v => new ChartEntry((float)v.TotalVentas)
            {
                Label = $"{ObtenerMes(v.Mes)}",
                ValueLabel = $"{Math.Round(v.TotalVentas, 2)} C$",
                Color = SKColor.Parse("#3498db") 
            }).ToList();

            var chart = new LineChart
            {
                Entries = entries,
                LineMode = LineMode.Straight, 
                LineSize = 8,
                PointMode = PointMode.Circle, 
                PointSize = 18, 
                LabelTextSize = 30, 
                ValueLabelTextSize = 30,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
                Margin = 50
            };

            LineChartView.Chart = chart;
        }

        private string ObtenerMes(int mes)
        {
            if (mes < 1 || mes > 12)
            {
                return "Número de mes no válido";
            }

            return Enum.GetName(typeof(MesesDelAño), mes) ?? "Mes desconocido";
        }
    }
}