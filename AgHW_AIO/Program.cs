using System;
using System.Collections.Generic;
using AgHW_AIO.AgClasses;
using AgHW_AIO.Lessons;


namespace AgHW_AIO
{
    internal class Program
    {
        static readonly string promtPressAnyKey = "Нажмите любую клавишу для продолжения...";
        static List<string> lessons = new List<string>();
        delegate void methods();
        static readonly methods[] methodMain = new methods[] { DoLesson3, DoLesson4,
                                                               DoLesson5};
        static void Main(string[] args)
        {
            InitLessonsList();
            SelectLesson();
        }
        private static void SelectLesson()
        {
            int selectedMenuItem = 0;
            do
            {
                AgMenu.SetPrompt("(Выберите пункт меню и нажмите Enter " +
                "или нажмите Escape для выхода)");
                selectedMenuItem = AgMenu.CallVertical(lessons, 0, 5, 0, true);
                if (selectedMenuItem == -1)
                    continue;
                methodMain[selectedMenuItem]();
            } while (selectedMenuItem != -1);
        }
        private static void InitLessonsList()
        {
            lessons.Add("Урок 3");
            lessons.Add("Урок 4");
            lessons.Add("Урок 5");
            //lessons.Insert(0, "Урок 2");
        }
        private static void DoLesson3()
        {
            HomeWork3.SelectTask();
        }
        private static void DoLesson4()
        {
            HomeWork4.SelectTask();
        }
        private static void DoLesson5()
        {
            HomeWork5.SelectTask();
        }
        internal static void NextScreen()
        {
            Console.WriteLine(promtPressAnyKey);
            Console.ReadKey();
        }
    }
}
