using System;
using System.Collections;
using System.Collections.Generic;

namespace Examination
{
    [Serializable]
    public class Answer
    {
        private string result;
        private bool isCorrect;

        public string Result { get { return result; } }
        public bool IsCorrect { get { return isCorrect; } }

        public Answer(string result, bool isCorrect = false)
        {
            this.result = result;
            this.isCorrect = isCorrect;
        }
    }
}
