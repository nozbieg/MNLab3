using MNLab3;
using static MNLab3.Lab3;


Console.WriteLine("Wybierz funkcję F(x, y):");
Console.WriteLine("1: F(x, y) = a * x + b * y");
Console.WriteLine("2: F(x, y) = a * x - b * y");
Console.WriteLine("3: F(x, y) = a * x * b * y");
Console.WriteLine("4: F(x, y) = (a * x) / (b * y)");
Console.Write("Twój wybór (1-4): ");
int choice = int.Parse(Console.ReadLine());

Func<double, double, double, double, double> F = choice switch
{
    1 => F1,
    2 => F2,
    3 => F3,
    4 => F4,
    _ => null
};

if (F == null)
{
    Console.WriteLine("Nieprawidłowy wybór!");
    return;
}

var (x0, y0, h, n, a, b) = Lab3.GetParameters();

Console.WriteLine("\nWybierz metodę:");
Console.WriteLine("1: Metoda Eulera");
Console.WriteLine("2: Pierwsze ulepszenie");
Console.WriteLine("3: Drugie ulepszenie (Euler-Cauchy)");
Console.Write("Twój wybór (1-3): ");
int methodChoice = int.Parse(Console.ReadLine());

List<double> xVals = new List<double>();
List<double> yVals = new List<double>();
string methodName = "";

switch (methodChoice)
{
    case 1:
        methodName = "Eulera";
        (xVals, yVals) = MetodaEulera(x0, y0, h, n, F, a, b);
        break;
    case 2:
        methodName = "Pierwsze ulepszenie";
        (xVals, yVals) = PierwszeUlepszenie(x0, y0, h, n, F, a, b);
        break;
    case 3:
        methodName = "Drugie ulepszenie";
        (xVals, yVals) = DrugieUlepszenie(x0, y0, h, n, F, a, b);
        break;
    default:
        Console.WriteLine("Nieprawidłowy wybór!");
        return;
}

Console.WriteLine("\nWyniki:");
for (int i = 0; i < xVals.Count; i++)
{
    Console.WriteLine($"x = {xVals[i]:F4}, y = {yVals[i]:F4}");
}

PlotResults(xVals, yVals, methodName);
