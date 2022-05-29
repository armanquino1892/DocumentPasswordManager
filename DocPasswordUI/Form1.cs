using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocPasswordUI
{
    public partial class DOCPassword : Form
    {
        public DOCPassword()
        {
            InitializeComponent();
        }

        private void btnPDFAdd_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DocPassword.PDF.PDFDocFunctions pdfPassword = new DocPassword.PDF.PDFDocFunctions(@openFileDialog1.FileName);
                bool result = pdfPassword.AddPDFPassword(txtPassword.Text, out string status);
                label3.ForeColor = result ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                label3.Text = status;
            }
        }

        private void btnPDFRemove_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DocPassword.PDF.PDFDocFunctions pdfPassword = new DocPassword.PDF.PDFDocFunctions(@openFileDialog1.FileName);
                bool result = pdfPassword.RemovePDFPassword(txtPassword.Text, out string status);
                label3.ForeColor = result ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                label3.Text = status;
            }
        }

        private void btnWordAdd_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DocPassword.Word.DocPassword docPassword = new DocPassword.Word.DocPassword(@openFileDialog1.FileName);
                var result = docPassword.AddPassword(txtPassword.Text, out string status);
                label3.ForeColor = result ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                label3.Text = status;
            }
        }

        private void btnWordRemove_Click(object sender, EventArgs e)
        {
            label3.Text = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DocPassword.Word.DocPassword docPassword = new DocPassword.Word.DocPassword(@openFileDialog1.FileName);
                var result = docPassword.RemovePassword(txtPassword.Text, out string status);
                label3.ForeColor = result ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                label3.Text = status;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = true;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
        }
    }
}
