using System;
using System.Collections;
using System.Collections.Generic;

namespace Examination
{
    [Serializable]
    public class Exam
    {
        private Query[] queries;
        private int correctlyAnsweredQueries;
        private int queryIndex;

        public int CorrectlyAnsweredQueries { get { return correctlyAnsweredQueries; } set { correctlyAnsweredQueries = value; } }

        public Query[] Queries { get { return queries; } }

        public Exam(Query[] queries)
        {
            this.queries = queries;
            correctlyAnsweredQueries = 0;
        }

        public bool isCorrect(Answer[] answers)
        {
            if (queryIndex >= this.queries.Length) return false;
            int baseCounter = 0;
            int parseCounter = 0;

            foreach (Answer answer in queries[queryIndex].Answers)
            {
                if (answer.IsCorrect) baseCounter++;
            }
            foreach (Answer answer in answers)
            {
                if (answer.IsCorrect) parseCounter++;
            }

            return baseCounter == parseCounter;
        }
        
        public Query getCurrentQuery()
        {
            return (queryIndex < this.queries.Length) ? queries[queryIndex] : null;
        }
        public Query nextQuery()
        {
            queryIndex++;
            return (queryIndex < this.queries.Length) ? queries[queryIndex] : null;
        }

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this);
        }

        public static Exam FromJson(string json)
        {
            return UnityEngine.JsonUtility.FromJson<Exam>(json);
        }
    }
}