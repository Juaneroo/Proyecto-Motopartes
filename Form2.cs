using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class Form2 : Form
    {
        string usuario = "admin";
        string contraseña = "admin";

        public Form2()
        {
            InitializeComponent();

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;
            string password = txtPass.Text;

            if (user.Equals(usuario)) 
                //Usamos el equals para un "igual a" si ingresa llamamos al form1 sino, mostramos mensaje de error
            {
                if (password.Equals(contraseña))
                {
                    MessageBox.Show("FELICIDADES", "ACCESO EXITOSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    this.Hide();

                    Form1 f1 = new Form1();
                    f1.Show();
                }

                else
                {
                    MessageBox.Show("Contraseña incorrecta");
                }
            }
            else
            {
                MessageBox.Show("Nombre de usuario incorrecto.");
            }

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
