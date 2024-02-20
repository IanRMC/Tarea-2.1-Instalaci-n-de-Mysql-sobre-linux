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
    public partial class Inventario : Form
    {
        Form1 inicio;
        oInventario inventario;
        int opcion;
        public Inventario(Form1 f, oInventario inventario, int op)
        {
            InitializeComponent();
            inicio = f;
            this.inventario = inventario;
            opcion = op;
            
            cmbArea.DataSource = new areasDAO().GetAll();
            
            if(op  == 1)
            {
                txtId.Text = "nuevo";
                cmbAd.SelectedIndex = 0;
                cmbArea.SelectedIndex = 0;
            }
            else
            {
                txtId.Text = inventario.id + "";
                cmbAd.SelectedItem = inventario.tipoAd;
                cmbArea.SelectedItem = inventario.area;
                txtNombre.Text = inventario.nombreCorto;
                txtDescripcion.Text = inventario.descripcion;
                txtSerie.Text = inventario.serie;
                txtColor.Text = inventario.color;
                dtpFecha.Text = inventario.fechaAd;
                txtObservacion.Text = inventario.observaciones;
            }
            cmbArea.DisplayMember = "Name";
        }

        private void Inventario_FormClosing(object sender, FormClosingEventArgs e)
        {
            inicio.Visible = true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if(txtNombre.Text == "")
            {
                MessageBox.Show("Rellene los campos");
                return;
            }
            if(txtSerie.Text == "")
            {
                MessageBox.Show("Rellene los campos");
                return;
            }
            inventario.nombreCorto = txtNombre.Text;
            inventario.descripcion = txtDescripcion.Text;
            inventario.serie = txtSerie.Text;
            inventario.color = txtColor.Text;
            inventario.fechaAd = dtpFecha.Text;
            inventario.tipoAd = cmbAd.SelectedItem.ToString();
            inventario.observaciones = txtObservacion.Text;
            inventario.id_area = ((oAreas)(cmbArea.SelectedItem)).id;
            if (opcion == 1)
            {
                new inventarioDAO().Insert(inventario);
                this.Close();
            }
            else
            {
                new inventarioDAO().Update(inventario);
                this.Close();
            }
        }
    }
}
