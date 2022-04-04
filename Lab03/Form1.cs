using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace Lab03
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxAutenticacion.Checked)
            {
                txtUsuario.Enabled = false;
                txtContraseña.Enabled = false; 
            }else
            {
                txtUsuario.Enabled = true;
                txtContraseña.Enabled=true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Declaracion
            string servidor = txtServidor.Text;
            string baseDatos = txtBaseDatos.Text;
            string usuario = txtUsuario.Text;
            string pass = txtContraseña.Text;

            string str = "Server=" + servidor + "; DataBase=" + baseDatos + ";";

            if (checkBoxAutenticacion.Checked)
            {
                str += "Integrated Security = true";
            }else
            {
                str += "User Id=" + usuario + "; Password=" + pass + ";";
            }

            try
            {
                conn = new SqlConnection(str);
                conn.Open();
                MessageBox.Show("Conectado satisfactoriamente");
                btnDesconectar.Enabled = true;
            }catch (Exception ex)
            {
                MessageBox.Show("Error al conectar el servidor: \n " + ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            try
            {
                if(conn.State == ConnectionState.Open)
                {
                    MessageBox.Show("Estado del servidor: " + conn.State + "\n Version: " + conn.ServerVersion + "\n Base de Datos: " + conn.State);
                }
                else
                {
                    MessageBox.Show("Estado del servidor: " + conn.State);
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Imposible determinar el estado del servidor: \n" + ex.ToString());
            }
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            try
            {
                if(conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    MessageBox.Show("Conexion cerrada satisfactoriamente");
                }else
                {
                    MessageBox.Show("La conexion ya esta cerrada");
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al cerrar la conexion: \n" + ex.ToString());
            }
        }

        private void btnPersonas_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona(conn);
            persona.Show();
        }
    }
}
