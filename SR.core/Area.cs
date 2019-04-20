using System;
using System.Linq;

namespace SR.core
{
    public class Area {

        private Area(int id , string nombre, ConsoleColor color )
        {
            Id = id;
            Nombre = nombre;
            Color = color;
        }

        public static Area[] GetAreas() {
            return new Area[5] {
                new Area(1, "A", ConsoleColor.Red),
                new Area(2, "B", ConsoleColor.Green),
                new Area(3, "C", ConsoleColor.Yellow),
                new Area(4, "D", ConsoleColor.Blue),
                new Area(5, "E", ConsoleColor.Cyan),
             };
        }

        public int Id { get; }

        public string Nombre { get; }

        public ConsoleColor Color { get; set; }

        public static Area GetArea(int id)
        {
            return GetAreas().ToList().FirstOrDefault(e => e.Id == id);
        }

        public static Area GetRamdom()
        {
            var index = new Random().Next(0, GetAreas().Count());
            return GetAreas()[index]; 
        }

    }

}
