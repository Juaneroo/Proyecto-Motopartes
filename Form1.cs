using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ProyectoFinal
{
    public partial class Form1 : Form
    {
        //Creación de las listas para poder iniciar.

        List<string> codigo = new List<string>();
        List<string> nombre = new List<string>();
        List<int> cantidad = new List<int>();
        List<double> precio = new List<double>();

        List<string> codigoParaVenta = new List<string>();
        List<string> nombreParaVenta = new List<string>();
        List<int> cantidadParaVenta = new List<int>();
        List<double> precioParaVenta = new List<double>();

        int pos = 0;

        public void leer()
        {
            lvwDatos.Columns.Add("Código", 150, HorizontalAlignment.Center);
            lvwDatos.Columns.Add("Nombre", 250, HorizontalAlignment.Left);
            lvwDatos.Columns.Add("Cantidad", 150, HorizontalAlignment.Left);
            lvwDatos.Columns.Add("Precio", 150, HorizontalAlignment.Left);

            int i = 0;
            for (i = 0; i < codigo.Count(); i++)
            { //Creamos los items de la tabla como filas
                ListViewItem item = new ListViewItem(codigo[i]);
                item.SubItems.Add(nombre[i]);
                item.SubItems.Add(cantidad[i].ToString());
                item.SubItems.Add(precio[i].ToString());

                lvwDatos.Items.AddRange(new ListViewItem[] { item }); // Este lo usamos para añadir los items a la lvwdatos
            }

            lvwDatosVenta.Columns.Add("Código", 150, HorizontalAlignment.Center);
            lvwDatosVenta.Columns.Add("Nombre", 250, HorizontalAlignment.Left);
            lvwDatosVenta.Columns.Add("Cantidad", 150, HorizontalAlignment.Left);
            lvwDatosVenta.Columns.Add("Precio", 150, HorizontalAlignment.Left);




            for (i = 0; i < codigoParaVenta.Count; i++)
            {
                //proceso de creación de items de la tabla como filas y se añaden los diferentes items a lvwdatos
                ListViewItem item = new ListViewItem(codigoParaVenta[i]);
                item.SubItems.Add(nombreParaVenta[i]);
                item.SubItems.Add(cantidadParaVenta[i].ToString());
                item.SubItems.Add(precioParaVenta[i].ToString());
                lvwDatosVenta.Items.AddRange(new ListViewItem[] { item });
                //lvwDatosdos.Items.Add(item);

            }

        }

        public void limpiar()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
            lvwDatos.Clear();
            lvwDatosVenta.Clear();


            lblReferencia.Text = "Referencia";
        }

        public void seleccionar(int i) // para llenar los txt en la parte de abajo con los datos
        {
            pos = i;
            txtCodigo.Text = codigo[pos];
            txtNombre.Text = nombre[pos];
            txtCantidad.Text = cantidad[pos].ToString();
            txtPrecio.Text = precio[pos].ToString();

            lblReferencia.Text = codigo[pos] + " - " + nombre[pos];
        }

        public Form1() //Para crear las columnas en la lvwdatos
        {
            InitializeComponent();
            lvwDatos.Columns.Add("Código", 150, HorizontalAlignment.Center);
            lvwDatos.Columns.Add("Nombre", 250, HorizontalAlignment.Left);
            lvwDatos.Columns.Add("Cantidad", 150, HorizontalAlignment.Left);
            lvwDatos.Columns.Add("Precio", 150, HorizontalAlignment.Left);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                codigo.Add(txtCodigo.Text);
                nombre.Add(txtNombre.Text);
                cantidad.Add(int.Parse(txtCantidad.Text));
                precio.Add(double.Parse(txtPrecio.Text));
                limpiar();
                leer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lvwDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvwDatos.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lvw in lvwDatos.SelectedItems)
                {
                    seleccionar(lvw.Index);
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e) //Para modificar el elemento que seleccionemos
        {
            codigo[pos] = txtCodigo.Text;
            nombre[pos] = txtNombre.Text;
            cantidad[pos] = int.Parse(txtCantidad.Text);
            precio[pos] = double.Parse(txtPrecio.Text);

            limpiar();
            leer();
        }

        private void btnEliminar_Click(object sender, EventArgs e) //para eliminar los elementos que seleccionemos
        {
            codigo.RemoveAt(pos);
            nombre.RemoveAt(pos);
            cantidad.RemoveAt(pos);
            precio.RemoveAt(pos);

            limpiar();
            leer();
        }

        private void btnnBuscar_Click(object sender, EventArgs e)
        { // para buscar un elemento y al final se rompe ciclo cuando lo encontremos
            int i = 0;
            for (i = 0; i < codigo.Count(); i++)
            {
                if (txtCodigo.Text == codigo[i])
                {
                    seleccionar(i);
                    break;
                }
            }
            if (i == codigo.Count())
            {
                MessageBox.Show("Producto no encontrado, vuelve a intentarlo con el código.");
            }
        }

        public void btnExportar_Click(object sender, EventArgs e)
        {
            try  // Escribimos el flujo de datos a traves de un string line y leyendo cada subitems de los items del listview
            {
                StreamWriter archivo = new StreamWriter("C:\\MotoSoft\\archivo.txt");
                foreach (ListViewItem item in lvwDatos.Items)
                {
                    string line = item.SubItems[0].Text + "|" + item.SubItems[1].Text + "|"
                        + item.SubItems[2].Text + "|" + item.SubItems[3].Text;
                    archivo.WriteLine(line);
                }
                archivo.Close();
                limpiar();
                MessageBox.Show("Archivo Exportado con éxito");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar el archivo \n " + ex.Message);
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            try // Añadimos la string line para que lea desde un archivo, dividimos y separamos por el PIPE y agregamos a las listas
            {
                codigo.Clear();

                StreamReader archivo = new StreamReader("C:\\MotoSoft\\archivo.txt");

                while (!archivo.EndOfStream)
                {
                    string[] line = archivo.ReadLine().Split("|");
                    codigo.Add(line[0]); nombre.Add(line[1]); cantidad.Add(int.Parse(line[2])); precio.Add(double.Parse(line[3]));
                }
                leer();
                archivo.Close();
                MessageBox.Show("Archivo importando con éxito", "FELICIDADES", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al importar el archivo \n " + ex.Message);
            }
        }

        private void lvwDatos_MouseDoubleClick(object sender, MouseEventArgs e) //cuando le demos doble click se nos descuente de a uno, buscando el item.
        {
            foreach (ListViewItem item in lvwDatos.SelectedItems)
            {
                int index = item.Index;
                if (cantidad[index] > 0)
                {
                    cantidad[index] -= 1;
                }
                else
                {
                    MessageBox.Show("Producto agotado");
                    break;
                }

                agregarOActulizarEnContizacion(item);
                break;
            }

            limpiar();
            leer();
        }

        void agregarOActulizarEnContizacion(ListViewItem item) //Para agregar los items a la listview y llamamos la lista para llenar los campos
        {

            string codigoParaAgregar = item.SubItems[0].Text;
            for (int i = 0; i < codigoParaVenta.Count; i++)
            {
                if (codigoParaVenta[i] == codigoParaAgregar)
                {
                    cantidadParaVenta[i] += 1;

                    return;
                }
            }

            codigoParaVenta.Add(item.SubItems[0].Text);
            nombreParaVenta.Add(item.SubItems[1].Text);
            cantidadParaVenta.Add(1);
            precioParaVenta.Add(double.Parse(item.SubItems[3].Text));
        }

        private void lvwDatosVenta_MouseDoubleClick(object sender, MouseEventArgs e) //Realizamos el mismo procedimiento de dar doble click para agregar a la listview
        {
            foreach (ListViewItem item in lvwDatosVenta.SelectedItems)
            {
                int index = item.Index;
                if (cantidadParaVenta[index] > 1)
                {
                    cantidadParaVenta[index] -= 1;
                }
                else
                {
                    DesagregarItem(item);
                    cantidadParaVenta.RemoveAt(index);
                    precioParaVenta.RemoveAt(index);
                    nombreParaVenta.RemoveAt(index);
                    codigoParaVenta.RemoveAt(index);
                    break;
                }

                DesagregarItem(item);
                break;
            }

            limpiar();
            leer();
        }

        void DesagregarItem(ListViewItem item) //Para quitar los items de la listview de venta por si el cliente ya no quiere esa compra
        {
            string codigoParaAgregar = item.SubItems[0].Text;
            for (int i = 0; i < codigo.Count; i++)
            {
                if (codigo[i] == codigoParaAgregar)
                {
                    cantidad[i] += 1;

                    return;
                }
            }

            codigo.Add(item.SubItems[0].Text);
            nombre.Add(item.SubItems[1].Text);
            cantidad.Add(1);
            precio.Add(double.Parse(item.SubItems[3].Text));
        }

        public void Export()
        {

            try  // repetimos el exportar para poder llamarlo en otro método.
            {
                StreamWriter archivo = new StreamWriter("C:\\MotoSoft\\archivo.txt");
                foreach (ListViewItem item in lvwDatos.Items)
                {
                    string line = item.SubItems[0].Text + "|" + item.SubItems[1].Text + "|"
                        + item.SubItems[2].Text + "|" + item.SubItems[3].Text;
                    archivo.WriteLine(line);
                }
                archivo.Close();
                limpiar();
                MessageBox.Show("Venta exitosa, archivo actualizado.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de venta y al exportar el archivo \n " + ex.Message);
            }

        }


        private void horafecha_Tick(object sender, EventArgs e) //Hora y fecha de la manera larga y completa.
        {
            lblHora.Text = DateTime.Now.ToLongTimeString();
            lblFecha.Text = DateTime.Now.ToLongDateString();
        }

        private void btnVender_Click(object sender, EventArgs e) //Verificamos que hayan objetos en la lvw y ya si vendemos actualizamos la lista mediante exportar.
        {
            if (lvwDatosVenta.Items.Count == 0)
            {
                MessageBox.Show("No hay objetos en cotización");
            }
            else
            {
                Export();
                lvwDatosVenta.Clear();
                codigoParaVenta.Clear();
                nombreParaVenta.Clear();
                cantidadParaVenta.Clear();
                precioParaVenta.Clear();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
