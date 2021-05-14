using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDeStock
{
    public partial class ManageProducts : Form
    {
        SQLiteConnection sqliteCon = Cnx.CreateConnection();
        public ManageProducts()
        {
            InitializeComponent();
        }

        private void ManageProducts_Load(object sender, EventArgs e)
        {

            SQLiteDataReader dr = Cnx.getData(sqliteCon, "select * from categories");
            SQLiteDataReader maxId = Cnx.getData(sqliteCon, "select MAX(id) from produit");

            while (dr.Read())
            {
                this.comboBox1.Items.Add(dr.GetString(1));
            }
                        
            Cnx.populateTable(sqliteCon, "select * from produit", this.dataGridView1);
            if (maxId.Read())
            {
                int newId = maxId.GetInt32(0) + 1;
                this.ID.Text = newId.ToString();
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        
            string selectedItem = this.comboBox1.SelectedItem.ToString();
            SQLiteDataReader dr2 = Cnx.getData(sqliteCon, "select * from categories where nom_cat='"+selectedItem+"'");
            if (dr2.Read())
            {
                this.catID.Text = dr2.GetInt32(0).ToString();
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            foreach (DataGridViewRow dvr in dataGridView1.SelectedRows)
            {
                this.ID.Text = dvr.Cells[0].Value.ToString();
                this.nom_du_produit.Text = dvr.Cells[1].Value.ToString();
                this.prix.Text = dvr.Cells[2].Value.ToString();
                this.description.Text = dvr.Cells[4].Value.ToString();

                int selectedCatId = int.Parse(dvr.Cells[3].Value.ToString());

                SQLiteDataReader dr2 = Cnx.getData(sqliteCon, "select * from categories where id=" + selectedCatId + "");
                if (dr2.Read())
                {
                    this.comboBox1.SelectedItem = dr2.GetString(1);
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            int catId = int.Parse(catID.Text);
            int productId = int.Parse(ID.Text);
            string productName = this.nom_du_produit.Text.Trim();
            double prix = double.Parse(this.prix.Text);
            string description = this.description.Text.Trim();

            string query = "insert into produit values(" + productId + ",'" + productName + "'," + prix + " ," + catId + " ,'" + description + "')";

            Cnx.InsertData(sqliteCon, query);
            Cnx.populateTable(sqliteCon, "select * from produit", this.dataGridView1);
            
            SQLiteDataReader maxId = Cnx.getData(sqliteCon, "select MAX(id) from produit");
            if (maxId.Read())
            {
                int newId = maxId.GetInt32(0) + 1;
                this.ID.Text = newId.ToString();
            }


            this.ID.Text = "";
            this.nom_du_produit.Text = "";
            this.prix.Text = "";
            this.description.Text = "";

            MessageBox.Show("Product Successfully Added");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int catId = int.Parse(catID.Text);
            int productId = int.Parse(ID.Text);
            string productName = this.nom_du_produit.Text.Trim();
            double prix = double.Parse(this.prix.Text);
            string description = this.description.Text.Trim();

            string query = "update produit set nom_produit='"+productName+"', prix="+prix+" ,categories="+catId+" ,description='"+description+"' where id="+productId+"";

            Cnx.InsertData(sqliteCon, query);
            Cnx.populateTable(sqliteCon, "select * from produit", this.dataGridView1);

            SQLiteDataReader maxId = Cnx.getData(sqliteCon, "select MAX(id) from produit");
            if (maxId.Read())
            {
                int newId = maxId.GetInt32(0) + 1;
                this.ID.Text = newId.ToString();
            }


            this.ID.Text = "";
            this.nom_du_produit.Text = "";
            this.prix.Text = "";
            this.description.Text = "";

            MessageBox.Show("Product Successfully Updated");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(ID.Text);
            String query = "Delete from produit where id=" + productId + ";";

            Cnx.InsertData(sqliteCon, query);
            Cnx.populateTable(sqliteCon, "select * from produit", this.dataGridView1);

            this.ID.Text = "";
            this.nom_du_produit.Text = "";
            this.prix.Text = "";
            this.description.Text = "";

            MessageBox.Show("Product Successfully Deleted");
        }
    }
}
