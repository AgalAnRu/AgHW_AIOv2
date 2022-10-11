using System;
using System.Collections.Generic;
using AgHW_AIO.AgClasses;
namespace AgHW_AIO.Lessons
{
    internal static class HomeWork4
    {
        private static readonly Random rnd = new Random();
        private static List<string> tasks = new List<string>();
        delegate void methods();
        private static readonly methods[] methodTasks = new methods[] {DoTask1, DoTask2, DoTask3,
                                                               DoTask4, DoTask5, DoTask6};
        internal static void SelectTask()
        {
            int selectedMenuItem = 0;
            tasks.Clear();
            InitTasksList();
            do
            {
                AgMenu.SetPrompt("(Выберите пункт меню и нажмите Enter " +
                "или нажмите Escape для возврата в предыдущее меню)");
                selectedMenuItem = AgMenu.CallVertical(tasks, 0, 5, 0, true);
                if (selectedMenuItem == -1)
                    continue;
                methodTasks[selectedMenuItem]();
                Program.NextScreen();
            } while (selectedMenuItem != -1);
        }
        private static void InitTasksList()
        {
            tasks.Add("Задача 1");
            tasks.Add("Задача 2");
            tasks.Add("Задача 3");
            tasks.Add("Задача 4");
            tasks.Add("Задача 5");
            tasks.Add("Задача 6");
        }
        private static void DoTask1()
        {
            Console.WriteLine("Задача 1");
            int boxTotal = rnd.Next(5, 51);
            int[] numberBoltsInBox = new int[boxTotal];
            int minValue;
            int maxValue;
            int boxNumber = 1;
            int count = 0;
            AgFillValues.ArrayRandomFill(numberBoltsInBox, 100, 400);
            AgFillValues.ArrayPrintAll(numberBoltsInBox);
            minValue = numberBoltsInBox[0];
            foreach (int bolts in numberBoltsInBox)
            {
                count++;
                if (bolts < minValue)
                {
                    minValue = bolts;
                    boxNumber = count;
                }
            }
            Console.WriteLine($"Минимальное количество {minValue} в {boxNumber}-м ящике");

            maxValue = numberBoltsInBox[0];
            count = 0;
            boxNumber = 1;
            foreach (int bolts in numberBoltsInBox)
            {
                count++;
                if (bolts > maxValue)
                {
                    maxValue = bolts;
                    boxNumber = count;
                }
            }
            Console.WriteLine($"Максимальное количество {maxValue} в {boxNumber}-м ящике");

            int boltsTotal = 0;
            foreach (int bolt in numberBoltsInBox)
                boltsTotal += bolt;
            double boltsAverage = (double) boltsTotal / boxTotal;
            Console.WriteLine($"Среднее количество болтов: {boltsAverage}");
            double boltDifference = 400;
            count = 0;
            boxNumber = 1;
            foreach (int bolt in numberBoltsInBox)
            {
                count++;
                if (boltDifference > (Math.Abs((double)bolt - boltsAverage)))
                {
                    boltDifference = (Math.Abs((double)bolt - boltsAverage));
                    boxNumber = count;
                }
            }
            Console.WriteLine($"Близжайшее значение {numberBoltsInBox[boxNumber - 1]} в {boxNumber}-м ящике");
        }
        private static void DoTask2()
        {
            Console.WriteLine("Задача 2");
            int sizeN = AgGetInput.GetInt32("N-размер матрицы", 1);
            int sizeM = AgGetInput.GetInt32("M-размер матрицы", 1);
            int[,] matrix = new int[sizeN, sizeM];
            AgFillValues.ArrayRandomFill(matrix, 1, 20);
            Console.WriteLine($"Исходная матрица [{sizeN}x{sizeM}]: ");
            AgFillValues.ArrayPrintMatrix(matrix);
            Console.WriteLine($"Зеркальная (относительно главной диагонали матрица: ");
            PrintMirrorMatrix(matrix);
        }
        private static void DoTask3()
        {
            Console.WriteLine("Задача 3");
            int vagonsTotal = AgGetInput.GetInt32("число вагонов", 0);
            int[] passangers = new int[vagonsTotal];
            int passangersTotal = 0;
            const int ROLL_LENGTH = 100;
            const double COMPLECT_LENGTH = 8;
            double rollTotal;
            AgFillValues.ArrayRandomFill(passangers, 0, 36);
            foreach (int passanger in passangers)
                passangersTotal += passanger;
            rollTotal = passangersTotal * COMPLECT_LENGTH / ROLL_LENGTH;
            Console.WriteLine($"Потребуется {rollTotal} рулонов ткани");
        }
        private static void DoTask4()
        {
            Console.WriteLine("Задача 4");
            int cellTotal = AgGetInput.GetInt32("количество ячеек в камере хранения", 1);
            int numberFirstCellToClear = AgGetInput.GetInt32("номер ячейки, с которой начать очистку", 1);
            double[] weightsCells = new double[cellTotal];
            AgFillValues.ArrayRandomFill(weightsCells, 0.0, 10.0);
            int count = 0;
            double weightTotal = 0;
            foreach (double weight in weightsCells)
            {
                count++;
                if (count != numberFirstCellToClear)
                    weightTotal += weight;
                else
                    count = 0;
            }
            Console.WriteLine($"Вес в ячейках после изъятия каждой {numberFirstCellToClear}-й ячейки стоставляет: {weightTotal} кг");
        }
        private static void DoTask5()
        {
            Console.WriteLine("Задача 5");
            int startTime = 9 * 60;
            int endTime = 16 * 60 + 10;
            int lessonsTotal = (endTime - startTime) / (45 + 10) + 1;
            int[,] lessonStartEnd = new int[2, lessonsTotal];
            for (int i = 0; i < lessonsTotal; i++)
            {

                lessonStartEnd[0, i] = (i > 0) ? lessonStartEnd[1, i - 1] + 10 : startTime;
                lessonStartEnd[1, i] = lessonStartEnd[0, i] + 45;
            }
            int usersTime = AgGetInput.GetTimeHHMM();
            if (usersTime < startTime)
            {
                Console.WriteLine($"Уроки ещё не начались");
                return;
            }
            if (usersTime > endTime)
            {
                Console.WriteLine($"Уроки уже закончились");
                return;
            }
            for (int i = 0; i < lessonsTotal; i++)
            {
                if (usersTime >= lessonStartEnd[0, i] && usersTime <= lessonStartEnd[1, i])
                {
                    Console.WriteLine($"Сейчас идёт {i + 1}-й урок");
                    return;
                }
                if (usersTime < lessonStartEnd[1, i])
                {
                    Console.WriteLine($"Сейчас идёт {i}-я перемена");
                    return;
                }
            }

        }
        private static void DoTask6()
        {
            Console.WriteLine("Задача 6");
            int sizeN = AgGetInput.GetInt32("N-размер матрицы", 1);
            int sizeM = AgGetInput.GetInt32("M-размер матрицы", 1);
            int[,] matrix = new int[sizeN, sizeM];
            int[] maxLineVolue = new int[sizeM];
            int[] minColumnVolue = new int[sizeN];
            AgFillValues.ArrayRandomFill(matrix, 1, 20);
            AgFillValues.ArrayRandomFill(maxLineVolue, 1, 1);
            AgFillValues.ArrayRandomFill(minColumnVolue, 20, 20);
            for (int i = 0; i < sizeM; i++)
            {
                for (int j = 0; j < sizeN; j++)
                {
                    if (maxLineVolue[i] < matrix[j, i])
                        maxLineVolue[i] = matrix[j, i];
                    if (minColumnVolue[j] > matrix[j, i])
                        minColumnVolue[j] = matrix[j, i];
                }
            }
            Console.WriteLine("Минор по столбцам и мажор по строкам:");
            PrintMatrixMinCMaxR(matrix, minColumnVolue, maxLineVolue);
        }
        private static void PrintMatrixMinCMaxR(int[,] matrix, int[] topLine, int[] leftColomn)
        {
            int nSize = matrix.GetLength(0);
            int mSize = matrix.GetLength(1);
            foreach (int minor in topLine)
                Console.Write($"\t{minor}");
            Console.WriteLine();
            foreach (int minor in topLine)
                Console.Write($"\tmin");
            Console.WriteLine();
            int row = 0;
            foreach (int major in leftColomn)
            {
                Console.Write($"{major} max ");
                for (int n = 0; n < nSize; n++)
                {
                    Console.Write($"\t{matrix[n, row]}");
                }
                Console.WriteLine();
                row++;
            }
        }
        private static void PrintMirrorMatrix(int[,] matrix)
        {
            int nSize = matrix.GetLength(0);
            int mSize = matrix.GetLength(1);
            int minSize = (mSize < nSize) ? mSize : nSize;
            for (int n = 1; n <= minSize; n++)
            {
                for (int m = 1; m <= minSize; m++)
                {
                    Console.Write($"{matrix[n - 1, m - 1]}\t");
                }
                if (nSize == minSize)
                    Console.WriteLine();
                else
                {
                    for (int i = minSize + 1; i <= nSize; i++)
                    {
                        Console.Write($"{matrix[i - 1, n - 1]}\t");
                    }
                    Console.WriteLine();
                }
            }
            if (mSize > minSize)
            {
                for (int m = minSize + 1; m <= mSize; m++)
                {
                    for (int n = 1; n <= nSize; n++)
                    {
                        Console.Write($"{matrix[n - 1, m - 1]}\t");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
