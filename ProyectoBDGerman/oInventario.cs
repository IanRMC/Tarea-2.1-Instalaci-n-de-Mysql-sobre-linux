using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ProyectoBDGerman
{
    public class oInventario
    {
        public int id { get; set; }
        public string nombreCorto { get; set; }
        public string descripcion {  get; set; }
        public string serie {  get; set; }
        public string color {  get; set; }
        public string fechaAd {  get; set; }
        public string tipoAd {  get; set; }
        public string observaciones {  get; set; }
        public int id_area {  get; set; }
        public string nom_area { get; set; }
        public oAreas area { get; set; }

        public oInventario() { }

        public oInventario(int id, string nombreCorto, string descripcion, string serie, string color, string fechaAd, string tipoAd, string observaciones, int area)
        {
            this.id = id;
            this.nombreCorto = nombreCorto;
            this.descripcion = descripcion;
            this.serie = serie;
            this.color = color;
            this.fechaAd = fechaAd;
            this.tipoAd = tipoAd;
            this.observaciones = observaciones;
            this.id_area = area;
        }
    }
}
