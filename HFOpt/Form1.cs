using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HFOpt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            // TODO: This line of code loads data into the 'pdpdataDataSet.pdpdata' table. You can move, or remove it, as needed.
            this.pdpdataTableAdapter.Fill(this.pdpdataDataSet.pdpdata);
            StartForecast.Enabled = false;
            StartOptimize.Enabled = false;
            chartControl1.DataSource = null;
            chartControl2.DataSource = null;
            chartControl3.DataSource = null;
            chartControl4.DataSource = null;
            chartControl5.DataSource = null;
            chartControl6.DataSource = null;
            chartControl7.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            string filename = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open CSV File";
            dialog.Filter = "CSV Files (*.csv)|*.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filename = dialog.FileName;
                //code to load file in datatable.
                DataTable data = new DataTable();
                data = ConvertCSVtoDataTable(filename);
                dataGridView1.DataSource = data;
            }
            StartForecast.Enabled = true;
            StartOptimize.Enabled = false;
        }

        public static DataTable ConvertCSVtoDataTable(string sCsvFilePath)
        {
            DataTable dtTable = new DataTable();
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            using (StreamReader sr = new StreamReader(sCsvFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dtTable.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = CSVParser.Split(sr.ReadLine());
                    DataRow dr = dtTable.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i].Replace("\"", string.Empty);
                    }
                    dtTable.Rows.Add(dr);
                }
            }

            return dtTable;
        }

        private void StartForecast_Click(object sender, EventArgs e)
        {
            var cur = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            Thread.Sleep(3000);
            StartForecast.Enabled = false;
            StartOptimize.Enabled = false;
            chartControl1.DataSource = pdpdataBindingSource;
            chartControl2.DataSource = pdpdataBindingSource;
            chartControl1.RefreshData();
            chartControl2.RefreshData();
            chartControl1.Refresh();
            chartControl2.Refresh();
            dataGridView2.DataSource = pdpdataBindingSource;
            dataGridView3.DataSource = pdpdataBindingSource;
            dataGridView2.Refresh();
            dataGridView3.Refresh();
            StartForecast.Enabled = true;
            StartOptimize.Enabled = true;
            Cursor.Current = cur;
        }

        private void StartOptimize_Click(object sender, EventArgs e)
        {
            var cur = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            Thread.Sleep(5000);

            StartForecast.Enabled = false;
            StartOptimize.Enabled = false;
            chartControl3.DataSource = pdpdataBindingSource;
            chartControl4.DataSource = pdpdataBindingSource;
            chartControl5.DataSource = pdpdataBindingSource;
            chartControl6.DataSource = pdpdataBindingSource;
            chartControl7.DataSource = pdpdataBindingSource;

            chartControl7.RefreshData();
            chartControl3.RefreshData();
            chartControl4.RefreshData();
            chartControl5.RefreshData();
            chartControl6.RefreshData();
            chartControl7.Refresh();
            chartControl3.Refresh();
            chartControl4.Refresh();
            chartControl5.Refresh();
            chartControl6.Refresh();

            StartForecast.Enabled = true;
            StartOptimize.Enabled = true;

            Cursor.Current = cur;

       
        }
    }
}
