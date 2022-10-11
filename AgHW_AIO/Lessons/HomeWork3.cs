using System;
using System.Collections.Generic;
using AgHW_AIO.AgClasses;

namespace AgHW_AIO.Lessons
{
    internal static class HomeWork3
    {
        private static Random rnd = new Random();
        private static List<string> tasks = new List<string>();
        delegate void methods();
        private static readonly methods[] methodTasks = new methods[] {DoTask1, DoTask2, DoTask3,
                                                               DoTask4};
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
        }
        private static void DoTask1()
        {
            Console.WriteLine("Задача 1");
            Console.WriteLine("Вывод цифр 0...9 (используется for)");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("\nВывод чисел 10...1 (используется for)");
            for (int i = 10; i > 0; i--)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("\nВывод целых положительных чисел (кратных 3 или 5) до заданного числа (используется for)");
            ushort maxUshot = AgGetInput.GetUint16("заданное число");
            for (ushort i = 1; i < maxUshot; i++)
            {
                if (i % 3 == 0)
                {
                    Console.WriteLine($"{i}\t- кратно 3");
                }
                if (i % 5 == 0)
                {
                    Console.WriteLine($"{i}\t- кратно 5");
                }
            }
            Console.WriteLine("\nВывод случайного ряда целых чисел");
            short minValue = AgGetInput.GetInt16("нижнюю границу диапазона:");
            short maxValue = AgGetInput.GetInt16("верхнюю границу диапазона:");
            byte numberDice = AgGetInput.GetByte("число повторений:");
            if (minValue > maxValue)
            {
                Console.WriteLine("Нижняя граница диапазона больше верхней (границы будут взаимозаменены)");
                short val = maxValue;
                maxValue = minValue;
                minValue = val;
            }
            for (int i = 0; i < numberDice; i++)
            {
                Console.WriteLine(rnd.Next(minValue, (maxValue + 1)));
            }
        }
        private static void DoTask2()
        {
            Console.WriteLine("Задача 2");
            Console.WriteLine("Введите строку символов и нажмите Enter");
            string inputString = Console.ReadLine();
            bool isEvenNumber = false;
            foreach (char ch in inputString)
            {
                if (isEvenNumber)
                    Console.Write(Char.ToUpper(ch));
                else
                    Console.Write(Char.ToLower(ch));
                isEvenNumber = !isEvenNumber;
            }
            Console.WriteLine();
        }
        private static void DoTask3()
        {
            Console.WriteLine("Задача 3");
            Console.WriteLine("Поиск близжайшего простого числа " +
                "в пределах 0...65535 от введённого");
            ushort minPrime = 0;
            ushort maxPrime = 0;
            ushort value = AgGetInput.GetUint16("число");

            for (int i = (value - 1); i > 0; i--)
            {
                if (IsPrime(i))
                {
                    minPrime = (ushort)i;
                    break;
                }
            }
            for (int i = (value + 1); i <= ushort.MaxValue; i++)
            {
                if (IsPrime(i))
                {
                    maxPrime = (ushort)i;
                    break;
                }
            }
            Console.Write("Близжайшее простое число: ");
            if (minPrime == 0)
            {
                Console.WriteLine(maxPrime);
                return;
            }
            if (maxPrime == 0)
            {
                Console.WriteLine(minPrime);
                return;
            }
            if ((value - minPrime) < (maxPrime - value))
                Console.WriteLine(minPrime);
            if ((value - minPrime) > (maxPrime - value))
                Console.WriteLine(maxPrime);
            if ((value - minPrime) == (maxPrime - value))
                Console.WriteLine($"слева - {minPrime}, справа - {maxPrime}");
            return;

            bool IsPrime(int number)
            {
                for (int i = 2; i <= number / 2; i++)
                {
                    if (number % i == 0)
                        return false;
                }
                return true;
            }
        }
        private static void DoTask4()
        {
            Console.WriteLine("Задача 4");
            byte numberTypes = AgGetInput.GetByte("количество томов");
            int[] types = new int[numberTypes];
            int volumeAllPages = rnd.Next(8000, 10001);
            int volumeSet = 0;
            int tail = 0;
            int tailCount = 0;
            Console.WriteLine($"Всего в наличии {volumeAllPages} листов");
            Console.WriteLine($"Один набор состоит из {numberTypes} томов");
            for (int i = 0; i < numberTypes; i++)
            {
                types[i] = rnd.Next(50, 201);
                Console.WriteLine($"Том {i + 1} - {types[i]} листов.");
                volumeSet += types[i];
            }
            Console.WriteLine($"Всего набор состоит из {volumeSet} листов");
            Console.WriteLine($"Можно изготовить {volumeAllPages / volumeSet} полных наборов");
            tail = volumeAllPages % volumeSet;
            if (tail > types[0])
            {
                Console.WriteLine("Дополнительно можно напечатать:");
                while (tail > 0)
                {
                    tail -= types[tailCount];
                    if (tail > 0)
                    {
                        tailCount++;
                        Console.WriteLine($"Том {tailCount}");
                    }
                }
                Console.WriteLine($"Остаток: {tail + types[tailCount]} листов");
            }
        }
    }
}
