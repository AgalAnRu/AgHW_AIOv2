using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgHW_AIO.AgClasses
{
    internal class AgMenu
    {
        private static ConsoleColor AgMenuBackgroundColor;
        private static ConsoleColor AgMenuForegroundColor;
        private static string[] menuItems;
        private static int selectedMenuItem;
        private static int menuOffsetLeft = 0;
        private static int menuOffsetTop = 0;
        private static string menuPrompt = string.Empty;
        private static bool isMenuVerticalType = true;
        /// <summary>
        /// Вызывает вертикальное меню
        /// </summary>
        /// <returns>Возвращает выбранный пользователем пункт меню</returns>
        ///<param name="listItems">Список пунктов меню</param>
        ///<param name="selectedItem">Выбранный пункт меню по умолчанию</param>
        ///<param name="offsetLeft">отступ меню слева</param>
        ///<param name="offsetTop">отступ меню сверху</param>
        ///<param name="isClearScreen">true - очистить экран перед выводом меню</param>
        ///<param name="isNewRow">true - вывод меню с новой строки</param>
        internal static int CallVertical(List<string> listItems, int selectedItem = 0, int offsetLeft = 0, int offsetTop = 0, bool isClearScreen = false, bool isNewRow = true)
        {
            menuItems = new string[listItems.Count];
            listItems.CopyTo(menuItems);
            selectedMenuItem = selectedItem;
            CorrectSelectedIndex();
            menuOffsetLeft = offsetLeft;
            menuOffsetTop = offsetTop;
            isMenuVerticalType = true;
            if (isClearScreen)
                Console.Clear();
            if (!isClearScreen)
            {
                if (isNewRow)
                    Console.WriteLine();
                menuOffsetLeft += Console.CursorLeft;
                menuOffsetTop += Console.CursorTop;
            }
            AgMenuBackgroundColor = Console.BackgroundColor;
            AgMenuForegroundColor = Console.ForegroundColor;
            Console.CursorVisible = false;
            DrawVerticalMenu();
            GetSelectedVertical();
            if (menuPrompt != string.Empty)
            {
                menuPrompt = string.Empty;
            }
            if (!isClearScreen)
            {
                menuOffsetLeft = 0;
                menuOffsetTop += menuItems.Length;
                Console.SetCursorPosition(menuOffsetLeft, menuOffsetTop);
            }
            if (isClearScreen)
                Console.Clear();
            Console.CursorVisible = true;
            return selectedMenuItem;
        }
        /// <summary>
        /// Вызывает вертикальное меню
        /// </summary>
        /// <returns>Возвращает выбранный пользователем пункт меню</returns>
        ///<param name="listItems">Список пунктов меню</param>
        ///<param name="selectedItem">Выбранный пункт меню по умолчанию</param>
        ///<param name="offsetLeft">отступ меню слева</param>
        ///<param name="offsetTop">отступ меню сверху</param>
        ///<param name="isClearScreen">true - очистить экран перед выводом меню</param>
        ///<param name="isNewRow">true - вывод меню с новой строки</param>
        internal static int CallHorizontal(List<string> listItems, int selectedItem = 0, int offsetLeft = 0, int offsetTop = 0, bool isClearScreen = false, bool isNewRow = true)
        {
            menuItems = new string[listItems.Count];
            listItems.CopyTo(menuItems);
            selectedMenuItem = selectedItem;
            CorrectSelectedIndex();
            menuOffsetLeft = offsetLeft;
            menuOffsetTop = offsetTop;
            isMenuVerticalType = true;
            if (isClearScreen)
                Console.Clear();
            if (!isClearScreen)
            {
                if (isNewRow)
                    Console.WriteLine();
                menuOffsetLeft += Console.CursorLeft;
                menuOffsetTop += Console.CursorTop;
            }
            AgMenuBackgroundColor = Console.BackgroundColor;
            AgMenuForegroundColor = Console.ForegroundColor;
            Console.CursorVisible = false;
            DrawHorizontallMenu();
            GetSelectedHorisontal();
            if (menuPrompt != string.Empty)
            {
                menuPrompt = string.Empty;
            }
            if (!isClearScreen)
            {
                menuOffsetLeft = 0;
                menuOffsetTop++;
                Console.SetCursorPosition(menuOffsetLeft, menuOffsetTop);
            }
            Console.CursorVisible = true;
            return selectedMenuItem;
        }

        private static void CorrectSelectedIndex(bool around = false)
        {
            if (selectedMenuItem > menuItems.Length - 1)
                selectedMenuItem = (around) ? 0 : menuItems.Length - 1;
            if (selectedMenuItem < 0)
                selectedMenuItem = (around) ? menuItems.Length - 1 : 0;
        }

        internal static int _GetSelectedMenuItem(int selectedMenuItem = 0)
        {
            return selectedMenuItem;
        }
        internal static void SetPrompt(string prompt)
        {
            menuPrompt = prompt;
        }
        private static void DrawVerticalMenu()
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.SetCursorPosition(menuOffsetLeft, menuOffsetTop + i);
                if (i == selectedMenuItem)
                {
                    SetNegativeColor(true);
                    Console.WriteLine(menuItems[i]);
                    SetNegativeColor(false);
                }
                if (i != selectedMenuItem)
                    Console.WriteLine(menuItems[i]);
            }
            if (menuPrompt != string.Empty)
                Console.WriteLine(menuPrompt);
        }
        private static void DrawHorizontallMenu()
        {
            Console.SetCursorPosition(menuOffsetLeft, menuOffsetTop);
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedMenuItem)
                {
                    SetNegativeColor(true);
                    Console.Write(menuItems[i]);
                    SetNegativeColor(false);
                    Console.Write(" ");
                }
                if (i != selectedMenuItem)
                    Console.Write(menuItems[i] + " ");
            }
            Console.WriteLine();
            if (menuPrompt != string.Empty)
                Console.WriteLine(menuPrompt);
        }
        private static void RedrawMenuItems(int itemSelected, int itemDeselected)
        {
            Console.SetCursorPosition(menuOffsetLeft, itemSelected + menuOffsetTop);
            SetNegativeColor(true);
            Console.WriteLine(menuItems[itemSelected]);
            SetNegativeColor(false);
            Console.SetCursorPosition(menuOffsetLeft, itemDeselected + menuOffsetTop);
            Console.WriteLine(menuItems[itemDeselected]);
        }
        private static void SetNegativeColor(bool setNegative = false)
        {
            if (setNegative)
            {
                Console.BackgroundColor = AgMenuForegroundColor;
                Console.ForegroundColor = AgMenuBackgroundColor;
            }
            if (!setNegative)
            {
                Console.BackgroundColor = AgMenuBackgroundColor;
                Console.ForegroundColor = AgMenuForegroundColor;
            }
        }
        private static int GetSelectedVertical()
        {
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.DownArrow)
                {
                    selectedMenuItem++;
                    if (selectedMenuItem == menuItems.Length)
                    {
                        CorrectSelectedIndex(true);
                        RedrawMenuItems(selectedMenuItem, menuItems.Length - 1);
                    }
                    else
                        RedrawMenuItems(selectedMenuItem, selectedMenuItem - 1);
                }
                if (key == ConsoleKey.UpArrow)
                {
                    selectedMenuItem--;
                    if (selectedMenuItem == -1)
                    {
                        CorrectSelectedIndex(true);
                        RedrawMenuItems(selectedMenuItem, 0);
                    }
                    else
                        RedrawMenuItems(selectedMenuItem, selectedMenuItem + 1);
                }
            } while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);
            Console.CursorVisible = true;
            if (key == ConsoleKey.Escape)
                selectedMenuItem = - 1;
            return selectedMenuItem;
        }
        private static int GetSelectedHorisontal()
        {
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.RightArrow)
                {
                    selectedMenuItem++;
                    CorrectSelectedIndex(true);
                    DrawHorizontallMenu();
                }
                if (key == ConsoleKey.LeftArrow)
                {
                    selectedMenuItem--;
                    CorrectSelectedIndex(true);
                    DrawHorizontallMenu();
                }
            } while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);
            Console.CursorVisible = true;
            if (key == ConsoleKey.Escape)
                selectedMenuItem = - 1;
            return selectedMenuItem;
        }
    }
}
