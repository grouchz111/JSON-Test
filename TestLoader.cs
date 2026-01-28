using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Drawing;


namespace WindowsFormsApp7
{
    public partial class TestLoader : Form
    {
        private TestData loadedTest;
        private int currentQuestionIndex = 0;


        // Stores user answers: questionID → "A"/"B"/"C"/"D"
        private Dictionary<int, string> userAnswers = new Dictionary<int, string>();

        public TestLoader()
        {
            InitializeComponent();
            this.MinimumSize = new Size(0, this.Height);
            this.MaximumSize = new Size(int.MaxValue, this.Height);

            // Disable answer buttons until JSON is loaded
            A.Enabled = false;
            B.Enabled = false;
            C.Enabled = false;
            D.Enabled = false;

            // 3D button borders
            Next.FlatStyle = FlatStyle.Flat;
            Next.FlatAppearance.BorderSize = 0;
            Next.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder3D(e.Graphics, Next.ClientRectangle, Border3DStyle.Raised);
            };

            Previous.FlatStyle = FlatStyle.Flat;
            Previous.FlatAppearance.BorderSize = 0;
            Previous.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder3D(e.Graphics, Previous.ClientRectangle, Border3DStyle.Raised);
            };
            Exit.FlatStyle = FlatStyle.Flat;
            Exit.FlatAppearance.BorderSize = 0;
            Exit.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder3D(e.Graphics, Exit.ClientRectangle, Border3DStyle.Raised);
            };



        }

        // ---------------------------------------------------------
        // LOAD A QUESTION INTO THE UI
        // ---------------------------------------------------------
        private void LoadQuestion(int index)
        {
            if (loadedTest == null || loadedTest.questions == null) return;

            var q = loadedTest.questions[index];

            Question.Text = q.questionText;

            A.Text = "A: " + q.answers["A"];
            B.Text = "B: " + q.answers["B"];
            C.Text = "C: " + q.answers["C"];
            D.Text = "D: " + q.answers["D"];

            // Restore previously selected answer
            A.Checked = userAnswers.ContainsKey(q.id) && userAnswers[q.id] == "A";
            B.Checked = userAnswers.ContainsKey(q.id) && userAnswers[q.id] == "B";
            C.Checked = userAnswers.ContainsKey(q.id) && userAnswers[q.id] == "C";
            D.Checked = userAnswers.ContainsKey(q.id) && userAnswers[q.id] == "D";
        }

        // ---------------------------------------------------------
        // LOAD QUESTION BY ID
        // ---------------------------------------------------------
        private void LoadQuestionById(int id)
        {
            if (loadedTest == null) return;

            var q = loadedTest.questions.FirstOrDefault(x => x.id == id);

            if (q != null)
            {
                currentQuestionIndex = loadedTest.questions.IndexOf(q);
                LoadQuestion(currentQuestionIndex);
            }
            else
            {
                MessageBox.Show($"Question with ID {id} not found.");
            }
        }

        // ---------------------------------------------------------
        // READ JSON FILE
        // ---------------------------------------------------------
        private void readFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JSON Files (*.json)|*.json|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileContent = File.ReadAllText(ofd.FileName);

                try
                {
                    loadedTest = JsonConvert.DeserializeObject<TestData>(fileContent);

                    if (loadedTest == null || loadedTest.questions == null)
                    {
                        MessageBox.Show("JSON loaded but contains no questions.");
                        return;
                    }

                    // Enable answer buttons now that test is loaded
                    A.Enabled = true;
                    B.Enabled = true;
                    C.Enabled = true;
                    D.Enabled = true;

                    currentQuestionIndex = 0;
                    LoadQuestion(currentQuestionIndex);
                }
                catch
                {
                    MessageBox.Show("Invalid JSON format!");
                }
            }
        }

        // ---------------------------------------------------------
        // SAVE FILE (OPTIONAL)
        // ---------------------------------------------------------
        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JSON Files (*.json)|*.json|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("File saved successfully!");
            }
        }

        // ---------------------------------------------------------
        // DELETE FILE
        // ---------------------------------------------------------
        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JSON Files (*.json)|*.json|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (MessageBox.Show("Are you sure you want to delete this file?",
                    "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    File.Delete(ofd.FileName);
                    MessageBox.Show("File deleted successfully!");
                }
            }
        }

        // ---------------------------------------------------------
        // ANSWER BUTTONS — SAVE USER ANSWERS
        // ---------------------------------------------------------
        private void A_CheckedChanged(object sender, EventArgs e)
        {
            if (loadedTest == null) return;
            if (A.Checked)
                userAnswers[loadedTest.questions[currentQuestionIndex].id] = "A";
        }

        private void B_CheckedChanged(object sender, EventArgs e)
        {
            if (loadedTest == null) return;
            if (B.Checked)
                userAnswers[loadedTest.questions[currentQuestionIndex].id] = "B";
        }

        private void C_CheckedChanged(object sender, EventArgs e)
        {
            if (loadedTest == null) return;
            if (C.Checked)
                userAnswers[loadedTest.questions[currentQuestionIndex].id] = "C";
        }

        private void D_CheckedChanged(object sender, EventArgs e)
        {
            if (loadedTest == null) return;
            if (D.Checked)
                userAnswers[loadedTest.questions[currentQuestionIndex].id] = "D";
        }

        // ---------------------------------------------------------
        // NEXT / PREVIOUS NAVIGATION
        // ---------------------------------------------------------
        private void Next_Click(object sender, EventArgs e)
        {
            if (loadedTest == null) return;

            if (currentQuestionIndex < loadedTest.questions.Count - 1)
            {
                currentQuestionIndex++;
                LoadQuestion(currentQuestionIndex);
            }
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            if (loadedTest == null) return;

            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                LoadQuestion(currentQuestionIndex);
            }
        }

        // ---------------------------------------------------------
        // CALCULATE SCORE
        // ---------------------------------------------------------
        private int CalculateScore()
        {
            int score = 0;

            foreach (var q in loadedTest.questions)
            {
                if (userAnswers.ContainsKey(q.id) &&
                    userAnswers[q.id] == q.correct)
                {
                    score++;
                }
            }

            return score;
        }

        // Example: call this from a "Finish" button
        private void Finish_Click(object sender, EventArgs e)
        {
            int score = CalculateScore();
            MessageBox.Show($"Your score: {score} / {loadedTest.questions.Count}");
        }
        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // optional: open main menu
        }

        private void authorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // optional: show author info
        }

        private void clearTextBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // optional: clear fields
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            int score = CalculateScore(); MessageBox.Show($"Your score: {score} / {loadedTest.questions.Count}");
           ID.Text = "Points:" + score.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ID_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TestLoader_Load(object sender, EventArgs e)
        {

        }
    }
}
