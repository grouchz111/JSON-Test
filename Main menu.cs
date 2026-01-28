using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace WindowsFormsApp7
{
    public partial class Main_menu : Form
    {
        // Missing fields added here
        private TestData loadedTest;
        private int currentQuestionIndex = 0;

        public Main_menu()
        {
            InitializeComponent();
        }

        private void testLoaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestLoader loaderForm = new TestLoader();
            loaderForm.Show();
        }

        private void Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JSON Files (*.json)|*.json|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileContent = File.ReadAllText(ofd.FileName);

                try
                {
                    loadedTest = JsonConvert.DeserializeObject<TestData>(fileContent);
                    currentQuestionIndex = 0;
                    LoadQuestion(currentQuestionIndex);
                }
                catch
                {
                    MessageBox.Show("Invalid JSON format!");
                }
            }
        }

        // Missing method added here
        private void LoadQuestion(int index)
        {
            if (loadedTest == null || loadedTest.questions == null)
            {
                MessageBox.Show("No test loaded!");
                return;
            }

            if (index < 0 || index >= loadedTest.questions.Count)
            {
                MessageBox.Show("Question index out of range!");
                return;
            }

            Question q = loadedTest.questions[index];

            // TODO: Update your UI here
            // Example:
            // questionLabel.Text = q.questionText;
            // radioA.Text = q.answers["A"];
            // radioB.Text = q.answers["B"];
            // radioC.Text = q.answers["C"];
            // radioD.Text = q.answers["D"];
        }
    }
}
