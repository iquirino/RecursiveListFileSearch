using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecursiveListFileSearch
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtFiles.MaxLength = int.MaxValue;
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            if (dlgFolder.ShowDialog() == DialogResult.OK)
                txtWhere.Text = dlgFolder.SelectedPath;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWhere.Text))
                return;
            if (string.IsNullOrWhiteSpace(txtFiles.Text))
                return;

            string[] lines = txtFiles.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Result result = new Result();
            result.Show();

            bool running = true;

            Task t = new Task(() =>
            {
                foreach (string line in lines)
                {
                    if (!running) return;
                    IEnumerable<string> ret = System.IO.Directory.EnumerateFiles(txtWhere.Text, line + (line.Contains(".") ? "" : ".*"), System.IO.SearchOption.AllDirectories);

                    foreach (string item in ret)
                    {
                        if (!running) return;
                        result.AddResult(item);
                    }
                }

                result.AddResult("Finish...");
            });

            result.OnClose = () =>
            {
                running = false;
                t.Wait();
            };

            t.Start();

        }
    }
}
