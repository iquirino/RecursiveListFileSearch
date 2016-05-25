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
    public partial class Result : Form
    {
        public Action OnClose { get; set; }

        public Result()
        {
            InitializeComponent();
        }

        public void AddResult(string text)
        {
            txtResult.Invoke(new Action(()=> {
                txtResult.Text += text + Environment.NewLine;
            }));
        }

        private void Result_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (OnClose != null)
                OnClose();
        }
    }
}
