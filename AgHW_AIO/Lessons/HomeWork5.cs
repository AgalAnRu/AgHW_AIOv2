using System;
using System.Collections.Generic;
using AgHW_AIO.AgClasses;

namespace AgHW_AIO.Lessons
{
    enum Direction { To_dB, From_dB };
    enum Parameter { Voltage, Vibroacceleration }
    enum Threshold { Actual, Old }
    internal static class HomeWork5
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
            double result = 0;
            Direction direction = SelectDirection();
            Parameter parameter = SelectParameter();
            if (parameter == Parameter.Voltage)
                result = CalculateResult(direction, parameter);
            if (parameter == Parameter.Vibroacceleration)
            {
                Threshold threshold = SelectThreshold();
                result = CalculateResult(direction, parameter, threshold);
            }
            PrintResult(result, direction, parameter);
        }
        /*
        private static void _DoTask1()
        {
            List<string> direction = new List<string>() { "Пересчёт в дБ", "пересчёт из дБ" };
            List<string> parameter = new List<string>() { "Напряжение", "Виброускорение" };
            List<string> threshold = new List<string>() { "Актуальное (10⁻⁶ м/с²)",
                "Устаревшее (3·10⁻⁴ м/с²)" };
            Console.Write("Выберите какой пересчет необходимо сделать:");
            int directionSelected = AgMenu.CallHorizontal(direction, 0, 2, 0, false, false);
            Console.Write("Выберите физическую величину:");
            int parameterSelected = AgMenu.CallHorizontal(parameter, 0, 2, 0, false, false);
            if (parameterSelected == 1)
            {
                Console.Write("Выберите пороговое значение дБ:");
                int thresholdSelected = AgMenu.CallHorizontal(threshold, 0, 2, 0, false, false);
            }
        }
        */
        private static void DoTask2()
        {
            const byte MIN_COUNT_NUMBERS = 1;
            const int MIN_VALUE_TO_FILL = -1000;
            const int MAX_VALUE_TO_FILL = 1000;
            Console.WriteLine("Задача 1");
            byte countNumbers = AgGetInput.GetByte("количество генерируемых чисел", MIN_COUNT_NUMBERS);
            List<int> numbers = CreatListRandomNumbers(MIN_VALUE_TO_FILL, MAX_VALUE_TO_FILL, countNumbers);
            Console.WriteLine("\nНачальный список:");
            PrintAllList(numbers);
            numbers.Sort();
            Console.WriteLine($"Минимальное значение: {numbers[0]}");
            Console.WriteLine($"Максимальное значение: {numbers[numbers.Count - 1]}");
        }
        private static void DoTask3()
        {
            const int NUMBER_OF_DIGIT = 6;
            Console.WriteLine("Задача 2");
            List<int> listSixDigitNumbers = CreatListRandomFixDigitNumber(NUMBER_OF_DIGIT);
            Console.WriteLine("\nНачальный список:");
            PrintAllList(listSixDigitNumbers);
            List<string> toDeleted = new List<string>() { "Чётные", "Нечётные" };
            Console.Write("Выберите что надо удалить:");
            int selectedToDelete = AgMenu.CallHorizontal(toDeleted, 1, 0, 0, false, false);
            if (selectedToDelete == 0)
            {
                Console.WriteLine("\nСписок после удаления чётных чисел:");
                for (int i = 0; i < listSixDigitNumbers.Count; i++)
                {
                    if (listSixDigitNumbers[i] % 2 == 0)
                    {
                        listSixDigitNumbers.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (selectedToDelete == 1)
            {
                Console.WriteLine("\nСписок после удаления нечётных чисел:");
                for (int i = 0; i < listSixDigitNumbers.Count; i++)
                {
                    if (listSixDigitNumbers[i] % 2 != 0)
                    {
                        listSixDigitNumbers.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (selectedToDelete == -1)
                return;
            PrintAllList(listSixDigitNumbers);
        }
        private static void DoTask4()
        {
            const int NUMBER_OF_DIGIT = 6;
            Console.WriteLine("Задача 3");
            List<int> listSixDigitNumbers = CreatListRandomFixDigitNumber(NUMBER_OF_DIGIT);
            Console.WriteLine("\nНачальный список:");
            PrintAllList(listSixDigitNumbers);
            int digit = AgGetInput.GetDigit(0, 9, true);
            Console.WriteLine($"\nСписок после удаления чисел, содержащих {digit}:");
            for (int i = 0; i < listSixDigitNumbers.Count; i++)
            {
                if (IsNumberContainsDigit(NumberToListDigits(listSixDigitNumbers[i]), digit))
                {
                    listSixDigitNumbers.RemoveAt(i);
                    i--;
                }
            }
            PrintAllList(listSixDigitNumbers);
        }
        private static Direction SelectDirection()
        {
            List<string> directionList = new List<string>() { "Пересчёт в дБ", "пересчёт из дБ" };
            Console.Write("Выберите какой пересчет необходимо сделать:");
            int directionSelected = AgMenu.CallHorizontal(directionList, 0, 2, 0, false, false);
            if (directionSelected == 0)
            {
                return Direction.To_dB;
            }
            return Direction.From_dB;
        }
        private static Parameter SelectParameter()
        {
            List<string> parameterList = new List<string>() { "Напряжение", "Виброускорение" };
            Console.Write("Выберите физическую величину:");
            int parameterSelected = AgMenu.CallHorizontal(parameterList, 0, 2, 0, false, false);
            if (parameterSelected == 0)
                return Parameter.Voltage;
            return Parameter.Vibroacceleration;
        }
        private static Threshold SelectThreshold()
        {
            List<string> thresholdList = new List<string>() { "Актуальное (10⁻⁶ м/с²)",
                                                              "Устаревшее (3·10⁻⁴ м/с²)" };
            Console.Write("Выберите пороговое значение дБ:");
            int thresholdSelected = AgMenu.CallHorizontal(thresholdList, 0, 2, 0, false, false);
            if (thresholdSelected == 0)
                return Threshold.Actual;
            return Threshold.Old;
        }
        private static double CalculateResult(Direction direction, Parameter parameter, Threshold threshold = Threshold.Actual)
        {
            double result = 0;
            if (direction == Direction.To_dB && parameter == Parameter.Voltage)
                result = CalculateVoltageTo();
            if (direction == Direction.From_dB && parameter == Parameter.Voltage)
                result= CalculateVoltageFrom();
            if (direction == Direction.To_dB && parameter == Parameter.Vibroacceleration)
                result= CalculateVibroaccelerTo(threshold);
            if (direction == Direction.From_dB && parameter == Parameter.Vibroacceleration)
                result=CalculateVibroaccelerationFrom(threshold);
            return result;
        }
        private static double CalculateVoltageTo()
        {
            double voltageReference = AgGetInput.GetDouble("Введите опорное напряжение", 5.0, 24.0);
            double voltageMeasured = AgGetInput.GetDouble("Введите измеренное напряжение", 0.0, 24.0);
            double result = 20 * Math.Log10(voltageMeasured / voltageReference);
            return result;
        }
        private static double CalculateVoltageFrom()
        {
            double voltageReference = AgGetInput.GetDouble("Введите опорное напряжение", 5.0, 24.0);
            double voltageMeasuredDBV = AgGetInput.GetDouble("Введите измеренное dBV", 0, 100);
            double result = voltageReference * Math.Pow(10, (0.05 * voltageMeasuredDBV));
            return result;
        }
        private static double CalculateVibroaccelerTo(Threshold threshold)
        {
            const double THRESHOLD_ACTUAL = 0.000001;
            const double THRESHOLD_OLD = 0.0003;
            double vibroaccelerationMeasured = AgGetInput.GetDouble("Введите измеренное СКЗ виброускорения", 0.0, 10.0);
            double result = 0;
            if (threshold == Threshold.Actual)
                result = 20 * Math.Log10(vibroaccelerationMeasured / THRESHOLD_ACTUAL);
            if (threshold == Threshold.Old)
                result = 20 * Math.Log10(vibroaccelerationMeasured / THRESHOLD_OLD);
            return result;
        }
        private static double CalculateVibroaccelerationFrom(Threshold threshold)
        {
            const double THRESHOLD_ACTUAL = 0.000001;
            const double THRESHOLD_OLD = 0.0003;
            double vibroaccelerationMeasured = AgGetInput.GetDouble("Введите измеренный уровень виброускорения", 0.0, 100.0);
            double result = 0;
            if (threshold == Threshold.Actual)
            result = THRESHOLD_ACTUAL * Math.Pow(10, (0.05 * vibroaccelerationMeasured));
            if (threshold == Threshold.Old)
                result = THRESHOLD_OLD * Math.Pow(10, (0.05 * vibroaccelerationMeasured));
            return result;
        }
        private static void PrintResult(double result, Direction direction, Parameter parameter)
        {
            if (direction == Direction.To_dB && parameter == Parameter.Voltage)
                Console.WriteLine($"Результат = {result} dBV");
            if (direction == Direction.From_dB && parameter == Parameter.Voltage)
                Console.WriteLine($"Результат = {result} V");
            if (direction == Direction.To_dB && parameter == Parameter.Vibroacceleration)
                Console.WriteLine($"Результат = {result} dB");
            if (direction == Direction.From_dB && parameter == Parameter.Vibroacceleration)
                Console.WriteLine($"Результат = {result} м/с²");
        }
        private static List<int> CreatList(byte listMaxLength = 0)
        {
            const int LIST_MIN_LEHGHS = 1;
            if (listMaxLength <= 0)
            {
                listMaxLength = AgGetInput.GetByte("максимальную длину списка", LIST_MIN_LEHGHS);
            }
            List<int> list = new List<int>(listMaxLength);
            return list;
        }
        private static List<int> CreatListRandomNumbers(int minValue = int.MinValue, int maxValue = int.MaxValue, byte listMaxLength = 0)
        {
            const int LIST_MIN_LEHGHS = 1;
            if (listMaxLength <= 0)
            {
                listMaxLength = AgGetInput.GetByte("максимальную длину списка", LIST_MIN_LEHGHS);
            }
            List<int> list = new List<int>(listMaxLength);

            for (int i = 0; i < listMaxLength; i++)
            {
                list.Add(rnd.Next(minValue, maxValue + 1));
            }
            return list;
        }
        private static List<int> CreatListRandomFixDigitNumber(int numberDigits, byte listMaxLength = 0)
        {
            int minValue = (int)Math.Pow(10, numberDigits - 1);
            int maxValue = (int)Math.Pow(10, numberDigits);
            List<int> list = CreatListRandomNumbers(minValue, maxValue, listMaxLength);
            return list;
        }
        private static void PrintAllList(List<int> list)
        {
            string prefix = string.Empty;
            foreach (int i in list)
            {
                prefix = (i < 0) ? "\t" : "\t ";
                Console.WriteLine(prefix + i);
            }
        }
        private static List<int> NumberToListDigits(int number)
        {
            List<int> list = new List<int>();
            while (number > 0)
            {
                list.Add(number % 10);
                number /= 10;
            }
            return list;
        }
        private static bool IsNumberContainsDigit(List<int> number, int digit)
        {
            bool isNumberContainsDigit = (number.Contains(digit)) ? true : false;
            return isNumberContainsDigit;
        }
    }
}
