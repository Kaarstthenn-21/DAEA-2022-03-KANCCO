using Lab03;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Lab02_01
{

    public partial class frmLogin : Form
    {
        SqlConnection conn;
        public frmLogin(SqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            int rpta = 0;
            if(conn.State == System.Data.ConnectionState.Open)
            {
                String Username = txtUsuario.Text;
                String Password = txtPassword.Text;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_loginpersona";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;

                SqlParameter param = new SqlParameter();
                param.ParameterName = "@username";
                param.SqlDbType = System.Data.SqlDbType.NVarChar;
                param.Value = Username;

                SqlParameter param2 = new SqlParameter();
                param2.ParameterName = "@password";
                param2.SqlDbType = System.Data.SqlDbType.NVarChar;
                param2.Value = Password;            

                cmd.Parameters.Add(param);
                cmd.Parameters.Add(param2);         

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Persona person = new Persona(conn);
                    person.Show();
                    this.Hide();
                    reader.Close();
                }
                else
                {
                    txtPassword.Clear();
                    txtUsuario.Clear();
                    txtPassword.ForeColor = Color.LightBlue;
                    txtInfo.ForeColor = Color.Red;
                    txtInfo.Text = "Credenciales incorrectas!";
                    reader.Close();
                }
            }
            else
            {
                MessageBox.Show("La conexion esta cerrada");
            }
            
            
            string[] usernames = { "usuario1", "usuario2", "usuario3" };
            string[] password  = { "password1", "password2", "password3" };
            bool wasFound = false;

            for (int i = 0; i < usernames.Length; i++)
                if (txtUsuario.Text == usernames[i] && txtPassword.Text == password[i])
                {
                    wasFound = true;
                    break;
                }

            if (wasFound)
            {
                Form1 principal = new Form1();
                principal.Show();
                this.Hide();
            }
            else
            {
                txtPassword.Clear();
                txtUsuario.Clear();
                txtPassword.ForeColor = Color.LightBlue;
                txtInfo.ForeColor = Color.Red;
                txtInfo.Text = "Credenciales incorrectas!";
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            if(txtUsuario.Text == "")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.Black;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if(txtPassword.Text == "")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.LightBlue;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            txtInfo.Text = "usuario1 / password1";
        }
    }
}
