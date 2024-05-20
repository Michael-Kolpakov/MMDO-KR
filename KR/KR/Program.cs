// Варіант №9
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var variantNum = Input<int>("Введіть номер варіанту (1-30): ");

while (variantNum is < 1 or > 30)
{
    Console.Write("Помилка: введіть допустиме значення номеру варіанту. ");
    variantNum = Input<int>("Введіть номер варіанту (1-30): ");
}

var leftBound = Input<float>("Введіть ліву межу інтегрування: ");
var rightBound = Input<float>("Введіть праву межу інтегрування: ");
var iterationsCount = Input<int>("Введіть кількість обчислень: ");

while (iterationsCount <= 0)
{
    Console.Write("Помилка: кількість ітерацій не може бути менша 0 або дорівнювати 0. ");
    iterationsCount = Input<int>("Введіть кількість обчислень: ");
}

var maxOrMin = Input<bool>("Введіть 0, якщо потрібно знайти мінімум, або 1, якщо максимум (0/1): ");

var result = FibonacciOptimization(variantNum, leftBound, rightBound, iterationsCount, maxOrMin);
var xOpt = (result.x1 + result.x2) / 2;
var fOpt = Function(variantNum, xOpt);

Console.WriteLine($"\nОптимальна точка ({(maxOrMin ? "максимум" : "мінімум")}): x = {xOpt:f3}");
Console.WriteLine($"Значення функції в оптимальній точці: f(x) = {fOpt:f3}");

return;

// Функція для обчислення n-го числа Фібоначчі
int Fibonacci(int n)
{
    if (n is 0 or 1)
        return n;

    return Fibonacci(n - 1) + Fibonacci(n - 2);
}

// Метод для одновимірної оптимізації числа Фібоначчі
(float x1, float x2) FibonacciOptimization(int variantNum, float leftBound, float rightBound, int iterationsCount, bool findMax = false)
{
    var l = Fibonacci(iterationsCount - 1) / (float)Fibonacci(iterationsCount);
    var x1 = leftBound + (1 - l) * (rightBound - leftBound);
    var x2 = leftBound + l * (rightBound - leftBound);

    var f1 = Function(variantNum, x1);
    var f2 = Function(variantNum, x2);

    Console.WriteLine();
    
    for (int i = 1; i < iterationsCount - 1; i++)
    {
        Console.WriteLine(i < iterationsCount - 2
            ? $"Ітерація {i}: x1 = {x1:f3}, x2 = {x2:f3}, f(x1) = {f1:f3}, f(x2) = {f2:f3}"
            : $"Ітерація {i}: x1 = {x1 / 2:f3}, x2 = {x2:f3}, f(x1) = {Function(variantNum, x1 / 2):f3}, f(x2) = {f2:f3}");

        if ((findMax && f1 < f2) || (!findMax && f1 > f2))
        {
            leftBound = x1;
            x1 = x2;
            x2 = leftBound + Fibonacci(iterationsCount - i - 1) / (float)Fibonacci(iterationsCount - i) * (rightBound - leftBound);
            f1 = f2;
            f2 = Function(variantNum, x2);
        }
        else
        {
            rightBound = x2;
            x2 = x1;
            x1 = leftBound + Fibonacci(iterationsCount - i - 2) / (float)Fibonacci(iterationsCount - i) * (rightBound - leftBound);
            f2 = f1;
            f1 = Function(variantNum, x1);
        }
    }

    return (x1, x2);
}

// Функція для валідації введення числа з консолі
T Input<T>(string prompt)
{
    Console.Write(prompt);
    
    while (true)
    {
        if (typeof(T) == typeof(bool) && int.TryParse(Console.ReadLine(), out int tempIntValue))
            return (T)Convert.ChangeType(tempIntValue, typeof(T));
        if (typeof(T) == typeof(int) && int.TryParse(Console.ReadLine(), out int intValue))
            return (T)Convert.ChangeType(intValue, typeof(T));
        if (typeof(T) == typeof(float) && float.TryParse(Console.ReadLine(), out float floatValue))
            return (T)Convert.ChangeType(floatValue, typeof(T));
        
        Console.Write($"Помилка: введіть коректне значення. {prompt}");
    }
}

// Функція, яку потрібно інтегрувати
float Function(int variantNum, float x) =>
    variantNum switch
    {
        1 => (float)Math.Log(Math.Pow(x, 2) - 2 * x + 2),
        2 => (float)(3 * x / (Math.Pow(x, 2) + 1)),
        3 => (float)((2 * x - 1) / Math.Pow(x - 1, 2)),
        4 => (float)((x + 2) * Math.Exp(1 - x)),
        5 => (float)Math.Log(Math.Pow(x, 2) - 2 * x + 4),
        6 => (float)(Math.Pow(x, 3) / (Math.Pow(x, 2) - x + 1)),
        7 => (float)Math.Pow((x + 1) / x, 3),
        8 => (float)(x - Math.Pow(x, 3)),
        9 => (float)(4 - Math.Exp(-Math.Pow(x, 2))),
        10 => (float)((Math.Pow(x, 3) + 4) / Math.Pow(x, 2)),
        11 => (float)(x * Math.Exp(x)),
        12 => (float)((x - 2) * Math.Exp(x)),
        13 => (float)((x - 1) * Math.Exp(-x)),
        14 => (float)(x / (9 - Math.Pow(x, 2))),
        15 => (float)((1 + Math.Log(x)) / x),
        16 => (float)Math.Exp(4 * x - Math.Pow(x, 2)),
        17 => (float)((Math.Pow(x, 5) - 8) / Math.Pow(x, 4)),
        18 => (float)((Math.Exp(2 * x) + 1) / Math.Exp(x)),
        19 => (float)(x * Math.Log(x)),
        20 => (float)(Math.Pow(x, 3) * Math.Exp(x + 1)),
        21 => (float)((Math.Pow(x, 2) - 2 * x + 2) / (x - 1)),
        22 => (float)((x + 1) * Math.Pow(x, 2f/3)),
        23 => (float)Math.Exp(6 * x - Math.Pow(x, 2)),
        24 => (float)(Math.Log(x) / x),
        25 => (float)(3 * Math.Pow(x, 4) - 16 * Math.Pow(x, 3) + 2),
        26 => (float)(Math.Pow(x, 5) - 5 * Math.Pow(x, 4) + 5 * Math.Pow(x, 3) + 1),
        27 => (float)((3 - x) * Math.Exp(-x)),
        28 => (float)(Math.Sqrt(3) / 2 + Math.Cos(x)),
        29 => (float)(108 * x - Math.Pow(x, 4)),
        30 => (float)(Math.Pow(x, 4) / 4 - 6 * Math.Pow(x, 3) + 7),
        _ => throw new ArgumentOutOfRangeException(nameof(variantNum), "Немає такого варіанту")
    };