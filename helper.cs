using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp7
{
    class helper
    {
        public class TestModel
        {
            public string testName { get; set; }
            public int totalQuestions { get; set; }
            public List<Question> questions { get; set; }
        }

        public class Question
        {
            public int id { get; set; }
            public string subject { get; set; } // math/informatics
            public string type { get; set; }    // single/multiple/open
            public string text { get; set; }
            public List<string> answers { get; set; }
            public List<object> correctAnswers { get; set; }
            public int points { get; set; }
        }

        public class UserAnswer
        {
            public int questionId { get; set; }
            public List<int> selectedIndexes { get; set; } = new List<int>();
            public string openText { get; set; } = "";
        }

    }
}
