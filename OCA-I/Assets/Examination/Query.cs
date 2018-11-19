using System;
using System.Collections;
using System.Collections.Generic;

namespace Examination
{
    [Serializable]
    public class Query
    {
        private string situation; //applies to query (not required)
        private string question; //given query
        private string explaination; //result
        private Answer[] answers;
        private QueryType type;

        public string Situation { get { return situation; } }
        public string Question { get { return question; } }
        public string Explaination { get { return explaination; } }
        public Answer[] Answers { get { return answers; } }
        public QueryType Type { get { return type; } }

        public Query(string situation, string question, string explaination, Answer[] answers, QueryType type)
        {
            this.situation = situation;
            this.question = question;
            this.explaination = explaination;
            this.answers = answers;
            this.type = type;
        }
    }
}