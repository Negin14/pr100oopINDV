using System;
using System.Collections.Generic;

namespace KindergartenApp
{
    public class Child
    {
        string number;
        public string Number
        {
            get { return number; }
        }
        public Child(string number)
        {
            this.number = number;
        }
    }

    public class Manageress
    {
        string name;
        public string Name
        {
            get { return name; }
        }
        public Manageress(string name)
        {
            this.name = name;
        }

        public void Queue()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Завідувач: Місць немає! Пропоную стати в чергу");
            PlaySchool.OnZapys();
        }
    }

    public class Department
    {
        string name;
        public string Name
        {
            get { return name; }
        }
        public Department(string name)
        {
            this.name = name;
        }

        public void Place()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Районо: Інші діти записуються в чергу");
        }
    }

    public class PlaySchool
    {
        public delegate void NotPlacesEventHandler();
        public static event NotPlacesEventHandler NotPlaces;

        public delegate void ZapysEventHandler();
        public static event ZapysEventHandler Zapys;

        int capacity;
        List<Child> children;
        bool therePlaces;

        public bool TherePlaces
        {
            get { return therePlaces; }
        }

        public PlaySchool(int capacity)
        {
            this.capacity = capacity;
            children = new List<Child>();
            this.therePlaces = true;
        }

        public static void OnZapys()
        {
            if (Zapys != null)
            {
                Zapys();
            }
        }

        public void PushChild(Child child)
        {
            if (children.Count < capacity)
            {
                children.Add(child);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Дитина {child.Number} зарахована");

                if (children.Count == capacity)
                {
                    therePlaces = false;
                    if (NotPlaces != null)
                    {
                        NotPlaces();
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Write("Місць у дитячому садку: ");
            int placesCount = int.Parse(Console.ReadLine() ?? "5");

            PlaySchool school = new PlaySchool(placesCount);
            Manageress manager = new Manageress("Ірина");
            Department dept = new Department("Центральний");

            PlaySchool.NotPlaces += manager.Queue;
            PlaySchool.Zapys += dept.Place;

            int i = 1;
            while (school.TherePlaces)
            {
                Child child = new Child(i.ToString());
                school.PushChild(child);
                i++;
            }

            Console.ResetColor();
            Console.ReadKey();
        }
    }
}