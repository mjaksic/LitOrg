using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace LitOrg
{
    public partial class Form1 : Form
    {
        private string author, title, isbn, yearOi, publisher, binding, nmbrOfPgs;

        private SqlCeCommand _command;
        private SqlCeConnection _connection;
        private SqlCeDataAdapter _adapter;
        private string _connectionString = @"Data Source=C:\Users\Matej\source\repos\LitOrg\dbLitOrg.sdf";

        public Form1()
        {
            InitializeComponent();
            OpenConnection();
            UpdateData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //FILTER
        private void btnFilter_Click(object sender, EventArgs e)
        {
            String selectedFilter = cbFilters.SelectedText;

            GetDataFromTextFields();

            //query upit sa ovim podatcima
            if (rbtnAsc.Checked && !rbtnDsc.Checked)
            {
                //filtrira uzlazno
            }
            else if (!rbtnAsc.Checked && rbtnDsc.Checked)
            {
                //filtrira silazno
            }
        
        }

        //DELETE
        private void btnDelete_Click(object sender, EventArgs e)
        {
            GetDataFromTextFields();
            FixQuery();
            if (isbn.Length != 13)
            {
                MessageBox.Show("ISBN se mora sastojati od 13 brojeva");
            }
            else
            {
                _command = new SqlCeCommand("DELETE from books WHERE isbn = @isbn", _connection);
                BindData();
                _command.ExecuteNonQuery();
                MessageBox.Show("Delete successful");
                UpdateData();
            }
        }

        //UPDATE
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            GetDataFromTextFields();
            FixQuery();
            if (isbn.Length != 13)
            {
                MessageBox.Show("ISBN se mora sastojati od 13 brojeva");
            }
            else
            {
                _command = new SqlCeCommand("UPDATE books SET title = @title, author = @author, year = @year, publisher = @publisher, binding = @binding, pagenumber = @pagenumber WHERE isbn = @isbn", _connection);
                BindData();
                _command.ExecuteNonQuery();
                MessageBox.Show("Update successful");
                UpdateData();
            }
        }

        //FIND
        private void btnFind_Click(object sender, EventArgs e)
        {
            GetDataFromTextFields();
            //query upit sa ovim podatcima
        }

        //ADD
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            GetDataFromTextFields();
            FixQuery();
            if (isbn.Length != 13) 
            {
                MessageBox.Show("ISBN se mora sastojati od 13 brojeva"); 
            }
            else
            {
                _command = new SqlCeCommand("INSERT INTO books (isbn, title, author, year, publisher, binding, pagenumber) VALUES (@isbn, @title, @author, @year, @publisher, @binding, @pagenumber)", _connection);
                BindData();
                _command.ExecuteNonQuery();
                MessageBox.Show("Add successful");
                UpdateData();
                RemoveDataFromTextFields();
            }
        }
        private void GetDataFromTextFields()
        {
            author = txtboxAuthor.Text.ToString();
            title = txtboxTitle.Text.ToString();
            isbn = txtboxISBN.Text.ToString();
            yearOi = txtboxYearOI.Text.ToString();
            publisher = txtboxPublisher.Text.ToString();
            binding = txtboxBinding.Text.ToString();
            nmbrOfPgs = txtboxNmbrOfPages.Text.ToString();
        }
        private void RemoveDataFromTextFields()
        {
            txtboxAuthor.Text = "";
            txtboxTitle.Text = "";
            txtboxISBN.Text = "";
            txtboxYearOI.Text = "";
            txtboxPublisher.Text = "";
            txtboxBinding.Text = "";
            txtboxNmbrOfPages.Text = "";
        }

        public void OpenConnection()
        {
            _connection = new SqlCeConnection(_connectionString);
            _connection.Open();
        }
        private void UpdateData()
        {
            _adapter = new SqlCeDataAdapter("SELECT * FROM books", _connection);
            DataTable table = new DataTable();
            _adapter.Fill(table);
            dataGridView.DataSource = table;
        }
        private void FixQuery()
        {
            author.Replace("'", "''");
            title.Replace("'", "''");
        }
        private void BindData()
        {
            _command.Parameters.AddWithValue("@isbn", isbn);
            _command.Parameters.AddWithValue("@title", title);
            _command.Parameters.AddWithValue("@author", author);
            _command.Parameters.AddWithValue("@year", yearOi);
            _command.Parameters.AddWithValue("@publisher", publisher);
            _command.Parameters.AddWithValue("@binding", binding);
            _command.Parameters.AddWithValue("@pagenumber", nmbrOfPgs);
        }
    }
}
