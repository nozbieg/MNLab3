using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;

using OxyPlot.SkiaSharp;

namespace MNLab3
{
    public static class Lab3
    {

       public static double F1(double x, double y, double a, double b) => a * x + b * y; 
       public static double F2(double x, double y, double a, double b) => a * x - b * y;
       public static double F3(double x, double y, double a, double b) => a * x * b * y;
       public static double F4(double x, double y, double a, double b) => y != 0 ? (a * x) / (b * y) : 0;

        // Get parameters from user
       public static (double, double, double, int, double, double) GetParameters()
        {
            Console.Write("Podaj x0: ");
            double x0 = double.Parse(Console.ReadLine());

            Console.Write("Podaj y0: ");
            double y0 = double.Parse(Console.ReadLine());

            Console.Write("Podaj krok h: ");
            double h = double.Parse(Console.ReadLine());

            Console.Write("Podaj liczbę kroków n: ");
            int n = int.Parse(Console.ReadLine());

            double a;
            while (true)
            {
                Console.Write("Podaj stałą a (różną od zera): ");
                a = double.Parse(Console.ReadLine());
                if (a != 0) break;
                Console.WriteLine("Stała a nie może być równa 0! Spróbuj ponownie.");
            }

            double b;
            while (true)
            {
                Console.Write("Podaj stałą b (różną od zera): ");
                b = double.Parse(Console.ReadLine());
                if (b != 0) break;
                Console.WriteLine("Stała b nie może być równa 0! Spróbuj ponownie.");
            }

            return (x0, y0, h, n, a, b);
        }

        // Euler Method
        public static (List<double>, List<double>) MetodaEulera(double x0, double y0, double h, int n, Func<double, double, double, double, double> F, double a, double b)
        {
            var xVals = new List<double> { x0 };
            var yVals = new List<double> { y0 };

            for (int i = 0; i < n; i++)
            {
                double xi = xVals[^1];
                double yi = yVals[^1];
                double xiPlus1 = xi + h;
                double yiPlus1 = yi + h * F(xi, yi, a, b);
                xVals.Add(xiPlus1);
                yVals.Add(yiPlus1);
            }

            return (xVals, yVals);
        }

        // First Improvement
        public static (List<double>, List<double>) PierwszeUlepszenie(double x0, double y0, double h, int n, Func<double, double, double, double, double> F, double a, double b)
        {
            var xVals = new List<double> { x0 };
            var yVals = new List<double> { y0 };

            for (int i = 0; i < n; i++)
            {
                double xi = xVals[^1];
                double yi = yVals[^1];
                double xiPlus1 = xi + h;
                double xStar = 0.5 * (xi + xiPlus1);
                double yStar = yi + 0.5 * h * F(xi, yi, a, b);
                double mStar = F(xStar, yStar, a, b);
                double yiPlus1 = yi + h * mStar;
                xVals.Add(xiPlus1);
                yVals.Add(yiPlus1);
            }

            return (xVals, yVals);
        }

        // Second Improvement (Euler-Cauchy)
        public static (List<double>, List<double>) DrugieUlepszenie(double x0, double y0, double h, int n, Func<double, double, double, double, double> F, double a, double b)
        {
            var xVals = new List<double> { x0 };
            var yVals = new List<double> { y0 };

            for (int i = 0; i < n; i++)
            {
                double xi = xVals[^1];
                double yi = yVals[^1];
                double xiPlus1 = xi + h;
                double yStar = yi + h * F(xi, yi, a, b);
                double mStar = F(xiPlus1, yStar, a, b);
                double yiPlus1 = yi + 0.5 * h * (F(xi, yi, a, b) + mStar);
                xVals.Add(xiPlus1);
                yVals.Add(yiPlus1);
            }

            return (xVals, yVals);
        }

        // Plot results using OxyPlot
        public static void PlotResults(List<double> xVals, List<double> yVals, string methodName)
        {
            var plotModel = new PlotModel { Title = $"Rozwiązanie równania metodą {methodName}" };
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "x" });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "y" });

            var series = new LineSeries
            {
                Title = $"Metoda: {methodName}",
                MarkerType = MarkerType.Square,
                MarkerSize = 4,
                MarkerStroke = OxyColors.Brown
            };

            for (int i = 0; i < xVals.Count; i++)
            {
                series.Points.Add(new DataPoint(xVals[i], yVals[i]));
            }

            plotModel.Series.Add(series);


            // Save plot to a PNG file
            PngExporter.Export(plotModel, "plot.png", 800, 600);
            Console.WriteLine("Wykres zapisany jako plot.png");
        }
    }
}
