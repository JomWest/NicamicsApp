using Microcharts;
using NicamicsApp.Models.ReporteModels;
using NicamicsApp.Service;
using SkiaSharp;

namespace NicamicsApp.ReportesMongo
{



    public partial class ReporteComicsMasVendidos : ContentPage
    {
        private readonly ReporteService _reporteService;

        private string _fechaInicio = "";
        private string _fechaFinal = "";

        public ReporteComicsMasVendidos(ReporteService reporteService)
        {
            InitializeComponent();
            _reporteService = reporteService;
            // Configurar fechas inicial y final
            fechaInicialPicker.Date = new DateTime(DateTime.Now.Year, 1, 1);
            fechaFinalPicker.Date = new DateTime(DateTime.Now.Year, 12, 31);

            fechaInicialPicker.DateSelected += OnDateChanged;
            fechaFinalPicker.DateSelected += OnDateChanged;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            _fechaInicio = fechaInicialPicker.Date.ToString("yyyy-MM-dd");
            _fechaFinal = fechaFinalPicker.Date.ToString("yyyy-MM-dd");



            await LoadBarChart(_fechaInicio, _fechaFinal);
            await LoadPieChart(_fechaInicio, _fechaFinal);
            await LoadLineChart();
            await LoadBarChartComicsValorados(IpAddress.userId);
        }

        private async void OnDateChanged(object sender, DateChangedEventArgs e)
        {
            // Actualizar las fechas a partir de los DatePickers en el formato correcto


            if (fechaInicialPicker.Date > fechaFinalPicker.Date)
            {
                await DisplayAlert("Alerta", "La fecha inicial no puede ser mayor que la fecha final", "OK");
                return;
            }

            _fechaInicio = fechaInicialPicker.Date.ToString("yyyy-MM-dd");
            _fechaFinal = fechaFinalPicker.Date.ToString("yyyy-MM-dd");

            // Llamar a los m�todos de carga de gr�ficos con las fechas actualizadas
            await LoadBarChart(_fechaInicio, _fechaFinal);
            await LoadPieChart(_fechaInicio, _fechaFinal);
            await LoadLineChart();
        }

        private async Task LoadBarChartComicsValorados(string userId)
        {
            try
            {

                // Llamar al servicio para obtener los c�mics mejor valorados del usuario
                var reportData = await _reporteService.GetComicsMejorValoradosAsync("670c05ca1c5dec5b0d11566e");

                // Verificar si no se encontraron datos
                if (reportData.Count == 0)
                {
                    lblComicsValorados.Text = "No se encontraron datos";
                    BarChartComicsValorados.Chart = null;
                    return;
                }

                lblComicsValorados.Text = "C�mics Mejor Valorados";

                // Preparar los datos para el gr�fico de barras
                var entries = reportData.Select(r => new ChartEntry((float)r.RatingPromedio) 
                {
                    Label = r.NombreComic,
                    ValueLabel = $"{Math.Round(r.RatingPromedio, 2)}", // Mostrar la calificaci�n con dos decimales
                    Color = SKColor.Parse("#16a085"),
                    
                }).ToList();

                // Crear y configurar el gr�fico de barras
                var chart = new BarChart
                {
                    Entries = entries,
                    LabelTextSize = 30,
                    ValueLabelTextSize = 40,
                    LabelOrientation = Orientation.Vertical,
                    ValueLabelOrientation = Orientation.Horizontal,
                    Margin = 50,
                };

                // Asignar el gr�fico a la vista
                BarChartComicsValorados.Chart = chart;
            }
            catch (Exception ex)
            {
                // Manejar errores y mostrar un mensaje
                await DisplayAlert("Mensaje", "No se encontraron datos para el gr�fico de c�mics valorados", "OK");
            }
        }


        private async Task LoadBarChart(string fechaInicio, string fechaFinal)
        {
            try
            {
                DateRangeLabel.Text = $"Per�odo: {fechaInicio} - {fechaFinal}";

                var reportData = await _reporteService.GetComicsMasVendidosAsync("670c05ca1c5dec5b0d11566e", fechaInicio, fechaFinal);

                var totalVentas = await _reporteService.GetVentasTotalesPorFecha("670c05ca1c5dec5b0d11566e", fechaInicio, fechaFinal);

                DateRangeLabelTotalVentas.Text = $"Per�odo: {fechaInicio} - {fechaFinal}";

                lblTotalVentas.Text = $"{Math.Round(totalVentas, 2)} C$";

                if (reportData.Count == 0)
                {
                    lblComicsMasVendidos.Text = "No se encontraron datos";
                    BarChartView.Chart = null;
                    return;
                }

                lblComicsMasVendidos.Text = "Comics M�s Vendidos";

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
                    LabelOrientation = Orientation.Vertical,
                    ValueLabelOrientation = Orientation.Horizontal,
                    Margin = 50,
                };

                BarChartView.Chart = chart;

            }
            catch (Exception ex)
            {
               await DisplayAlert("Mensaje", "No se encontraron datos para el gr�fico de barras", "OK");
            }
        }

        private async Task LoadPieChart(string fechaInicio, string fechaFinal)
        {
            try
            {
                var vendedorId = "670c05ca1c5dec5b0d11566e"; // Reemplaza con el ID del vendedor real

                var ventasData = await _reporteService.GetVentasPorCategoriaAsync(vendedorId, fechaInicio, fechaFinal);

                if (ventasData.Count == 0)
                {
                    lblCategorias.Text = "No se encontraron datos";
                    PieChartView.Chart = null;
                    return;
                }

                lblCategorias.Text = "Categorias M�s Vendidas";

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
            catch (Exception ex) 
            {
                await DisplayAlert("Mensaje", "No se encontraron datos para el gr�fico de pastel", "OK");

            }

        }

        private async Task LoadLineChart()
        {
            try
            {
                var comidId = "670c06a01c5dec5b0d11566f";
                var a�o = fechaFinalPicker.Date.Year;

                var ventasData = await _reporteService.GetTotalVentasComicPorMes(comidId, a�o);

                if (ventasData.Count == 0)
                {
                    lblVentasMensualesComic.Text = "No se encontraron datos";
                    lblComicName.Text = "No se encontraron datos";
                    LineChartView.Chart = null;
                    return;
                }

                lblVentasMensualesComic.Text = $"Ventas Mensuales del C�mic en {a�o}";
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
            catch (Exception ex)
            {
                await DisplayAlert("Mensaje", "No se encontraron datos para el gr�fico de l�neas", "OK");
            }

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