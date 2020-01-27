using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlServerCe;


namespace LitOrg
{
    public partial class Form1 : Form
    {
        private string author, title, isbn, yearOi, publisher, binding, nmbrOfPgs;
        private string selectedSort;
        private SqlCeCommand _command;
        private SqlCeConnection _connection;
        private SqlCeDataAdapter _adapter;
        private string _connectionString = @"Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "dbLitOrg.sdf";
        //private string _connectionString = @"Data Source=C:\Users\Matej\source\repos\LitOrg\dbLitOrg.sdf";
        public Form1()
        {
            InitializeComponent();
            OpenConnection();
            UpdateTable();
            cbSort.SelectedIndex = 0;
            selectedSort = cbSort.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //SORT
        private void btnSort_Click(object sender, EventArgs e)
        {
            selectedSort = cbSort.SelectedText;
            GetDataFromTextFields();
            TrimAll();
            FieldIsEmpty();
            FixQuery();
            SelectedSortFix();
            SortData();
        }
        //DELETE
        private void btnDelete_Click(object sender, EventArgs e)
        {
            GetDataFromTextFields();
            TrimAll();
            FieldIsEmpty();
            FixQuery();
            DeleteData();
        }
        //UPDATE
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            GetDataFromTextFields();
            TrimAll();
            FieldIsEmpty();
            FixQuery();
            UpdateData();
        }
        //FIND
        private void btnFind_Click(object sender, EventArgs e)
        {
            GetDataFromTextFields();
            TrimAll();
            FixQuery();
            FindData();
        }
        //ADD
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            GetDataFromTextFields();
            TrimAll();
            FixQuery();
            FieldIsEmpty();
            AddData();
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
        private void TrimAll()
        {
            author = author.Trim();
            title = title.Trim();
            isbn = isbn.Trim();
            yearOi = yearOi.Trim();
            publisher = publisher.Trim();
            binding = binding.Trim();
            nmbrOfPgs = nmbrOfPgs.Trim();
        }
        private void FieldIsEmpty()
        {
            if (yearOi.Length == 0) { yearOi = "0"; };
            if (nmbrOfPgs.Length == 0) { nmbrOfPgs = "0"; };
        }
        public void OpenConnection()
        {
            _connection = new SqlCeConnection(_connectionString);
            _connection.Open();
        }
        private void UpdateTable()
        {
            _adapter = new SqlCeDataAdapter("SELECT isbn AS ISBN, title AS Title, author AS Author, year AS Year, publisher AS Publisher, binding AS Binding, pagenumber AS \"Page number\" FROM books;", _connection);
            DataTable table = new DataTable();
            _adapter.Fill(table);
            dataGridView.DataSource = table;
        }
        private void AddData()
        {
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
                UpdateTable();
                RemoveDataFromTextFields();
            }
        }
        private void FindData()
        {
            if (author.Length > 0)
            {
                _adapter = new SqlCeDataAdapter("SELECT isbn AS ISBN, title AS Title, author AS Author, year AS Year, publisher AS Publisher, binding AS Binding, pagenumber AS \"Page number\" FROM books WHERE author='" + author + "'", _connection);
                DataTable table = new DataTable();
                _adapter.Fill(table);
                dataGridView.DataSource = table;
                RemoveDataFromTextFields();
            }
            else if (title.Length > 0)
            {
                _adapter = new SqlCeDataAdapter("SELECT isbn AS ISBN, title AS Title, author AS Author, year AS Year, publisher AS Publisher, binding AS Binding, pagenumber AS \"Page number\" FROM books WHERE title='" + title + "'", _connection);
                DataTable table = new DataTable();
                _adapter.Fill(table);
                dataGridView.DataSource = table;
                RemoveDataFromTextFields();
            }
            else if (isbn.Length > 0)
            {
                _adapter = new SqlCeDataAdapter("SELECT isbn AS ISBN, title AS Title, author AS Author, year AS Year, publisher AS Publisher, binding AS Binding, pagenumber AS \"Page number\" FROM books WHERE isbn='" + isbn + "'", _connection);
                DataTable table = new DataTable();
                _adapter.Fill(table);
                dataGridView.DataSource = table;
                RemoveDataFromTextFields();
            }
            else if (yearOi.Length > 0)
            {
                _adapter = new SqlCeDataAdapter("SELECT isbn AS ISBN, title AS Title, author AS Author, year AS Year, publisher AS Publisher, binding AS Binding, pagenumber AS \"Page number\" FROM books WHERE year='" + yearOi + "'", _connection);
                DataTable table = new DataTable();
                _adapter.Fill(table);
                dataGridView.DataSource = table;
                RemoveDataFromTextFields();
            }
            else if (publisher.Length > 0)
            {
                _adapter = new SqlCeDataAdapter("SELECT isbn AS ISBN, title AS Title, author AS Author, year AS Year, publisher AS Publisher, binding AS Binding, pagenumber AS \"Page number\" FROM books WHERE publisher='" + publisher + "'", _connection);
                DataTable table = new DataTable();
                _adapter.Fill(table);
                dataGridView.DataSource = table;
                RemoveDataFromTextFields();
            }
            else if (binding.Length > 0)
            {
                _adapter = new SqlCeDataAdapter("SELECT isbn AS ISBN, title AS Title, author AS Author, year AS Year, publisher AS Publisher, binding AS Binding, pagenumber AS \"Page number\" FROM books WHERE binding='" + binding + "'", _connection);
                DataTable table = new DataTable();
                _adapter.Fill(table);
                dataGridView.DataSource = table;
                RemoveDataFromTextFields();
            }
            else if (nmbrOfPgs.Length > 0)
            {
                _adapter = new SqlCeDataAdapter("SELECT isbn AS ISBN, title AS Title, author AS Author, year AS Year, publisher AS Publisher, binding AS Binding, pagenumber AS \"Page number\" FROM books WHERE pagenumber='" + nmbrOfPgs + "'", _connection);
                DataTable table = new DataTable();
                _adapter.Fill(table);
                dataGridView.DataSource = table;
                RemoveDataFromTextFields();
            }
            else { UpdateTable(); }
        }
        private void UpdateData()
        {
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
                UpdateTable();
                RemoveDataFromTextFields();
            }
        }
        private void SortData()
        {
                if (rbtnAsc.Checked && !rbtnDsc.Checked)
                {
                    //filtrira uzlazno
                    _adapter = new SqlCeDataAdapter("SELECT isbn AS ISBN, title AS Title, author AS Author, year AS Year, publisher AS Publisher, binding AS Binding, pagenumber AS \"Page number\" FROM books ORDER BY " + selectedSort + " ASC", _connection);
                    DataTable table = new DataTable();
                    _adapter.Fill(table);
                    dataGridView.DataSource = table;
                }
                else
                {
                //filtrira silazno
                _adapter = new SqlCeDataAdapter("SELECT isbn AS ISBN, title AS Title, author AS Author, year AS Year, publisher AS Publisher, binding AS Binding, pagenumber AS \"Page number\" FROM books ORDER BY " + selectedSort + " DESC", _connection);
                DataTable table = new DataTable();
                _adapter.Fill(table);
                dataGridView.DataSource = table;
            }
        }
        private void DeleteData()
        {
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
                UpdateTable();
                RemoveDataFromTextFields();
            }
        }
        private void SelectedSortFix()
        {
            if (cbSort.Text == "Author")
            {
                selectedSort = "author";
            }
            if (cbSort.Text == "Title")
            {
                selectedSort = "title";
            }
            if (cbSort.Text == "ISBN")
            {
                selectedSort = "isbn";
            }
            if (cbSort.Text == "Year of Issue")
            {
                selectedSort = "year";
            }
            if (cbSort.Text == "Publisher")
            {
                selectedSort = "publisher";
            }
            if (cbSort.Text == "Binding")
            {
                selectedSort = "binding";
            }
            if (cbSort.Text == "Number of Pages")
            {
                selectedSort = "pagenumber";
            }
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
