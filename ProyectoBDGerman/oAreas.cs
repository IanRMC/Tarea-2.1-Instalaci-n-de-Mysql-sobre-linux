using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBDGerman
{
    public class oAreas
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string ubicacion { get; set; }

        public oAreas(int id, string nombre, string ubicacion)
        {
            this.id = id;
            this.name = nombre;
            this.ubicacion = ubicacion;
        }

    }
}
