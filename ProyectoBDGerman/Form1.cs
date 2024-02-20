using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoBDGerman
{
    public partial class Form1 : Form
    {
        Conexion co = new Conexion();
        List<oInventario> inventario = new List<oInventario>();
        public Form1()
        {
            InitializeComponent();
            string host = "172.174.228.193", user = "redian", pass = "delunoalocho";

            co.newSesion(host, user, pass);

            inventario = new inventarioDAO().GetAll();

            dgvInventario.DataSource = inventario;
            dgvInventario.Columns["id"].Visible = false;
            dgvInventario.Columns["id_area"].Visible = false;
            dgvInventario.Columns["area"].Visible = false;

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            Inventario tab = new Inventario(this, new oInventario(), 1);
            this.Visible = false;
            tab.Visible = true;
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            inventario = new inventarioDAO().GetAll();

            dgvInventario.DataSource = inventario;
            dgvInventario.Columns["id"].Visible = false;
            dgvInventario.Columns["id_area"].Visible = false;
            dgvInventario.Columns["area"].Visible = false;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvInventario.SelectedRows[0].Cells[0].Value.ToString());
            oInventario compra = new inventarioDAO().obtenerCompra(id);
            Inventario tab = new Inventario(this, compra, 2);
            this.Visible = false;
            tab.Visible = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvInventario.SelectedRows[0].Cells[0].Value.ToString());
            new inventarioDAO().Delete(id);

            inventario = new inventarioDAO().GetAll();

            dgvInventario.DataSource = inventario;
            dgvInventario.Columns["id"].Visible = false;
            dgvInventario.Columns["id_area"].Visible = false;
            dgvInventario.Columns["area"].Visible = false;
        }
    }
}
