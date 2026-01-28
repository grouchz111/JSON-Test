using System;
using System.Collections.Generic;

namespace WindowsFormsApp7
{
    public class TestData
    {
        public string testTitle { get; set; }
        public List<Question> questions { get; set; }
    }

    public class Question
    {
        public int id { get; set; }
        public string questionText { get; set; }
        public Dictionary<string, string> answers { get; set; }
        public string correct { get; set; }
    }
}
