using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionDeStock
{
    public partial class GestionDeCategorie : Form
    {
        SQLiteConnection sqliteCon = Cnx.CreateConnection();
        
        private void setNeWCatId()
        {
            
            String lastCategoryQuery = "SELECT * FROM categories WHERE id = (SELECT MAX(id) FROM categories);";

            SQLiteDataReader dr = Cnx.getData(sqliteCon, lastCategoryQuery);

            if (dr.Read())
            {
                int newId = dr.GetInt32(0) + 1;

                categorieID.Text = newId.ToString();
            }
        }
        public GestionDeCategorie()
        {
            InitializeComponent();
        }

        private void GestionDeCategorie_Load(object sender, EventArgs e)
        {
            setNeWCatId();
            String query = "select * from categories";

            Cnx.populateTable(sqliteCon, query, this.dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            foreach (DataGridViewRow dvr in dataGridView1.SelectedRows)
            {
                categorieID.Text = dvr.Cells[0].Value.ToString();
                categorie.Text = dvr.Cells[1].Value.ToString();
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int categorieId = int.Parse(this.categorieID.Text.Trim());
            String categorieNom = this.categorie.Text.Trim();
       
                      
            try
            {

               
                if (categorieNom == "")
                {
                    MessageBox.Show("Please Type a New Category Name to be added.");
                    Cnx.populateTable(sqliteCon, "select * from categories", this.dataGridView1);
                }
                else
                {
                    String query = "insert into categories values(" + categorieId + ",'" + categorieNom + "')";
                    Cnx.InsertData(sqliteCon, query);
                    Cnx.populateTable(sqliteCon, "select * from categories", this.dataGridView1);
                    setNeWCatId();
                    this.categorie.Text = "";
                }

                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int categorieId = int.Parse(this.categorieID.Text.Trim());
            String categorieNom = this.categorie.Text.Trim();


            try
            {


                if (categorieNom == "")
                {
                    MessageBox.Show("Please Type a New Category Name to be added.");
                    Cnx.populateTable(sqliteCon, "select * from categories", this.dataGridView1);
                }
                else
                {
                    String query = "update categories set id="+categorieId+", nom_cat='"+categorieNom+"' where id="+categorieId+";";
                    Cnx.InsertData(sqliteCon, query);
                    Cnx.populateTable(sqliteCon, "select * from categories", this.dataGridView1);
                    setNeWCatId();
                    this.categorie.Text = "";
                }


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int categorieId = int.Parse(this.categorieID.Text.Trim());
  


            try
            {
                String query = "Delete from categories where id=" + categorieId + ";";
                Cnx.InsertData(sqliteCon, query);
                Cnx.populateTable(sqliteCon, "select * from categories", this.dataGridView1);
                setNeWCatId();
                this.categorie.Text = "";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}
