using System;

namespace SR.core
{
    public class MaterieelVirtueel
    {
        public int id { get; set; }
        public int spoortak { get; set; }
        public string lintnaam { get; set; }
        public int kmwaarde { get; set; }
        public int spoor { get; set; }
        public int meters { get; set; }
        public string nummer { get; set; }
        public int emplacement_id { get; set; }
    }

    public class Areas {

        private Areas(int id , string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public static Areas[] GetAreas() {
            return new Areas[4] {
                new Areas(1, "A"),
                new Areas(2, "B"),
                new Areas(3, "C"),
                new Areas(4, "D")
             };
        }

        public int Id { get; }

        public string Nombre { get; }
    }

}
