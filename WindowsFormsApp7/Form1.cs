using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void readFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files|*.txt|All Files|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileContent = File.ReadAllText(ofd.FileName);
                textBox1.Text = fileContent;
            }
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files|*.txt|All Files|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, textBox1.Text);
                MessageBox.Show("File saved successfully!");
            }
        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files|*.txt|All Files|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                File.Delete(ofd.FileName);
                MessageBox.Show("File deleted successfully!");
                textBox1.Clear();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Optional: handle live changes (e.g., enable Save button)
        }

        private void authorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Aлександър Козлов 11Д";
        }

        private void clearTextBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}
