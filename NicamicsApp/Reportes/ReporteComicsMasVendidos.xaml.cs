using Microcharts;
using NicamicsApp.Models.ReporteModels;
using NicamicsApp.Service;
using SkiaSharp;

namespace NicamicsApp.Reportes
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

            DateRangeLabel.Text = $"Per�odo: {startDate} - {endDate}";

            // Obtener los datos del reporte (Aseg�rate de usar las fechas y vendedorId correctos)
            var reportData = await _reporteService.GetComicsMasVendidosAsync("670c05ca1c5dec5b0d11566e", startDate, endDate);

            var totalVentas = await _reporteService.GetVentasTotalesPorFecha("670c05ca1c5dec5b0d11566e", startDate, endDate);

            // Convertir los datos a entradas para el gr�fico
            var entries = reportData.Select(r => new ChartEntry((float)r.TotalVenta)
            {
                Label = r.NombreComic,
                ValueLabel = $"{Math.Round(r.TotalVenta, 2)} C$",
                Color = SKColor.Parse("#2c3e50") // Color de las barras
            }).ToList();

            var chart = new BarChart
            {
                Entries = entries,
                LabelTextSize = 40, // Tama�o del texto para las etiquetas
                ValueLabelTextSize = 40, // Tama�o del texto para los valores
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
                Margin = 50, // Aumentar el margen para m�s espacio en general
            };

            // Crear el gr�fico de barras
            BarChartView.Chart = chart;

            lblTotalVentas.Text = $"{Math.Round(totalVentas, 2)} C$";

        }

        private async Task LoadPieChart()
        {
            var vendedorId = "670c05ca1c5dec5b0d11566e"; // Reemplaza con el ID del vendedor real
            var startDate = "2024-07-10"; // Rango de fechas de inicio
            var endDate = "2024-10-14"; // Rango de fechas de fin

            // Obtener los datos de ventas por categor�a
            var ventasData = await _reporteService.GetVentasPorCategoriaAsync(vendedorId, startDate, endDate);

            // Convertir los datos a entradas para el gr�fico de pastel
            var entries = ventasData.Select(v => new ChartEntry((float)v.TotalVenta)
            {
                Label = v.Categoria,
                ValueLabel = $"{Math.Round(v.TotalVenta, 2)} C$",
                Color = SKColor.Parse("#" + (new Random().Next(0x1000000)).ToString("X6")) // Generar color aleatorio
            }).ToList();

            var chart = new PieChart
            {
                Entries = entries,
                LabelTextSize = 40, // Tama�o del texto para las etiquetas
                Margin = 50,
            };

            // Crear el gr�fico de pastel
            PieChartView.Chart = chart;
        }

        private async Task LoadLineChart()
        {

            var comidId = "670c06a01c5dec5b0d11566f";
            var a�o = 2024;

            // Obtener los datos de ventas mensuales del c�mic
            var ventasData = await _reporteService.GetTotalVentasComicPorMes(comidId, a�o);

            lblComicName.Text = ventasData.First().NombreComic;

            // Convertir los datos a entradas para el gr�fico de l�neas
            var entries = ventasData.Select(v => new ChartEntry((float)v.TotalVentas)
            {
                Label = $"{ObtenerMes(v.Mes)}",
                ValueLabel = $"{Math.Round(v.TotalVentas, 2)} C$",
                Color = SKColor.Parse("#3498db") // Color de las l�neas
            }).ToList();

            var chart = new LineChart
            {
                Entries = entries,
                LineMode = LineMode.Straight, // Modo de l�nea recta
                LineSize = 8, // Tama�o de la l�nea
                PointMode = PointMode.Circle, // Modo de puntos (c�rculos)
                PointSize = 18, // Tama�o de los puntos
                LabelTextSize = 30, // Tama�o del texto para las etiquetas
                ValueLabelTextSize = 30,
                LabelOrientation = Orientation.Horizontal,
                ValueLabelOrientation = Orientation.Horizontal,
                Margin = 50
            };

            // Crear el gr�fico de l�neas
            LineChartView.Chart = chart;
        }

        private string ObtenerMes(int mes)
        {
            if (mes < 1 || mes > 12)
            {
                return "N�mero de mes no v�lido";
            }

            return Enum.GetName(typeof(MesesDelA�o), mes) ?? "Mes desconocido";
        }
    }
}