using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace FirstApp
{
    public partial class ManageUsers : Form
    {
        SQLiteConnection sqliteCon = Cnx.CreateConnection();
        public ManageUsers()
        {
            InitializeComponent();

        }

        private void getAccounts()
        {
            String query = "SELECT * from Accounts";
            SQLiteCommand cmd = new SQLiteCommand(query, sqliteCon);
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
            adapter.Fill(dt);

            this.dataGridView1.DataSource = dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String username = this.username.Text.Trim();
            String password = this.password.Text.Trim();
            String nom = this.nom.Text.Trim();
            String prenom = this.prenom.Text.Trim();
            String jobID = this.comboBox1.SelectedItem.ToString();

            System.Diagnostics.Debug.WriteLine(jobID);
            try
            {

                String query = "insert into accounts values('"+username+"','"+password+"','"+nom+"','"+prenom+"','"+jobID+"');";

                Cnx.InsertData(sqliteCon, query);

                getAccounts();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {

              this.comboBox1.Items.Add("MANAGER");
            this.comboBox1.Items.Add("EMPLOYEE");
            this.comboBox1.SelectedIndex = 0;

            getAccounts();

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach(DataGridViewRow dvr in dataGridView1.SelectedRows)
            {
                username.Text = dvr.Cells[0].Value.ToString();
                password.Text = dvr.Cells[1].Value.ToString();
                nom.Text = dvr.Cells[2].Value.ToString();
                prenom.Text = dvr.Cells[3].Value.ToString();
                this.comboBox1.SelectedItem = dvr.Cells[4].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String username = this.username.Text.Trim();
            String password = this.password.Text.Trim();
            String nom = this.nom.Text.Trim();
            String prenom = this.prenom.Text.Trim();
            String jobID = this.comboBox1.SelectedItem.ToString();

            String query = "update accounts set username = '" + username + "', password='" + password + "',nom='" + nom + "',prenom='" + prenom + "',job_id='" + jobID + "' where username='"+username+"';";

            Cnx.InsertData(sqliteCon, query);

            getAccounts();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String username = this.username.Text.Trim();
            String query = "Delete from accounts where username='"+username+"';";

            Cnx.InsertData(sqliteCon, query);

            getAccounts();
        }
    }
}
