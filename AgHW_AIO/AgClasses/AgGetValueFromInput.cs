using System;


namespace AgHW_AIO.AgClasses
{
    internal static class AgGetInput
    {
        internal static int GetInt32(string prompt = "", int minValue = Int32.MinValue, int maxValue = Int32.MaxValue)
        {
            string inputStr = string.Empty;
            while (true)
            {
                Console.WriteLine($"Введите {prompt}");
                Console.Write($"(Целое число от {minValue} до {maxValue}): ");
                inputStr = Console.ReadLine();
                if (int.TryParse(inputStr, out int value))
                    if (value >= minValue && value <= maxValue)
                    {
                        return value;
                    }
            }
        }
        internal static byte GetByte(string prompt = "", byte minValue = byte.MinValue, byte maxValue = byte.MaxValue)
        {
            string inputStr = string.Empty;
            while (true)
            {
                Console.WriteLine($"Введите {prompt}");
                Console.Write($"(Целое число от {minValue} до {maxValue}): ");
                inputStr = Console.ReadLine();
                if (byte.TryParse(inputStr, out byte value))
                    if (value >= minValue && value <= maxValue)
                    {
                        return value;
                    }
            }
        }
        internal static short GetInt16(string prompt = "", short minValue = short.MinValue, short maxValue = short.MaxValue)
        {
            string inputStr = string.Empty;
            while (true)
            {
                Console.WriteLine($"Введите {prompt}");
                Console.Write($"(Целое число от {minValue} до {maxValue}): ");
                inputStr = Console.ReadLine();
                if (short.TryParse(inputStr, out short value))
                    if (value >= minValue && value <= maxValue)
                    {
                        return value;
                    }
            }
        }

        internal static ushort GetUint16(string prompt = "", ushort minValue = ushort.MinValue, ushort maxValue = ushort.MaxValue)
        {
            string inputStr = string.Empty;
            while (true)
            {
                Console.WriteLine($"Введите {prompt}");
                Console.Write($"(Целое число от {minValue} до {maxValue}): ");
                inputStr = Console.ReadLine();
                if (ushort.TryParse(inputStr, out ushort result))
                    return result;
            }
        }
        internal static double GetDouble(string prompt = "", double minValue = double.MinValue, double maxValue = double.MaxValue)
        {
            string inputStr = string.Empty;
            while (true)
            {
                Console.WriteLine($"Введите {prompt}");
                Console.Write($"(Число от {minValue} до {maxValue}): ");
                inputStr = Console.ReadLine();
                if (double.TryParse(inputStr, out double result))
                    return result;
            }
        }
        internal static int GetTimeHHMM()
        {
            string inputStr = string.Empty;
            int time = 0;
            int digit = 0;
            Console.WriteLine("Введите время в формате hh:mm");
            digit = GetDigit(0, 2);
            time = digit * 600;
            Console.Write(digit);
            if (digit == 2)
                digit = GetDigit(0, 3);
            else
                digit = GetDigit();
            time += digit * 60;
            Console.Write($"{digit} : ");
            digit = GetDigit(0, 5);
            Console.Write(digit);
            time += digit * 10;
            digit = GetDigit();
            Console.WriteLine(digit);
            time += digit;
            return time;
        }
        internal static int GetDigit(int minDigit = 0, int maxDigit = 9, bool isNeedPrompt = false)
        {
            char inChar = '0';
            int digit = 0;
            while (true)
            {
                if (isNeedPrompt)
                    Console.WriteLine($"Введите цифру от {minDigit} до {maxDigit}:");
                inChar = Console.ReadKey(true).KeyChar;
                if (Char.IsDigit(inChar))
                    digit = Convert.ToInt32(inChar) - 48;
                if (digit >= minDigit && digit <= maxDigit)
                    return digit;
            }
        }
    }
}
