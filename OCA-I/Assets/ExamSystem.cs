using Examination;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ExamSystem : MonoBehaviour {

    [SerializeField]
    private GameObject examSelectPanel;
    [SerializeField]
    private Text questionField;
    [SerializeField]
    private Text situationField;
    [SerializeField]
    private GameObject answerFieldPrefab;
    [SerializeField]
    private Transform answerArea;
    [SerializeField]
    private GameObject btnCheckAndContinue;

    private Exam[] exams;

    int examIndex = 0;
    [SerializeField]
    int itemsCheck = 0;
    [SerializeField]
    int answerCheck = 0;

    List<AnswerField> answerFieldList = new List<AnswerField>();

    // Use this for initialization
    void Start () {
        Answer[] q1answers = {
            new Answer("Compiler error on line 1."),
            new Answer("Compiler error on line 2."),
            new Answer("Compiler error on line 4."),
            new Answer("Compiler error on line 5."),
            new Answer("Compiler error on line 6.", true),
            new Answer("0null."),
            new Answer("nullnull")
        };

        Query[] queries = {
            new Query(
                "1: public class _C {\n2: \tprivate static int $;\n3: \tpublic static void main(String[] main) {\n4: \t\tString a_b;\n5: \t\tSystem.out.print($);\n6: \t\tSystem.out.print(a_b);\n7: \t} }",
                "What is the result of the following class? (Choose all that apply)",
                "",
                q1answers,
                QueryType.CLOSED_MULTIPLE
            ),
            new Query(
                "public class _C {\n\tprivate static int $;\n\tpublic static void main(String[] main) {\n\t\tString a_b;\n\t\tSystem.out.print($);\n\t\tSystem.out.print(a_b);\n\t} }",
                "What is the result of the following code?",
                "",
                q1answers,
                QueryType.CLOSED_SINGLE
            )
        };


        Exam assesmentTest = new Exam(queries);

        exams = new Exam[]{
            assesmentTest
        };
    }

    [ContextMenu("Start")]
    public void TakeExam()
    {
        examSelectPanel.SetActive(false);
        Exam exam = exams[examIndex];
        RenderScreen(exam);
    }

    public void TakeExam0()
    {
        examIndex = 0;
        TakeExam();
    }
    
    public void RenderScreen(Exam exam)
    {
        answerFieldList.Clear();
        answerCheck = 0;
        Query query = exam.getCurrentQuery();
        for (int i = 0; i < query.Answers.Length; i++)
        {
            Answer answer = query.Answers[i];

            if(answer.IsCorrect) answerCheck |= 1 << i;

            GameObject go = GameObject.Instantiate(answerFieldPrefab.gameObject, answerArea);
            RectTransform rectTransform = go.GetComponent<RectTransform>();
            AnswerField answerField = go.GetComponent<AnswerField>();

            rectTransform.localPosition = rectTransform.localPosition - new Vector3(0, rectTransform.rect.height * i);
            answerField.Index = i;
            answerField.Answer = answer;
            answerField.AnswerType = query.Type;
            Toggle toggle = answerField.GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(delegate {
                AnswerField af = toggle.gameObject.GetComponent<AnswerField>();
                if(af.AnswerType == QueryType.CLOSED_SINGLE && toggle.isOn)
                {
                    resetAllButtons(af);
                }

                if (toggle.isOn) itemsCheck |= 1 << af.Index; else itemsCheck &= ~(1 << af.Index);
                if (itemsCheck > 0) btnCheckAndContinue.SetActive(true); else btnCheckAndContinue.SetActive(false);
            });

            answerFieldList.Add(answerField);
        }

        situationField.text = query.Situation;
        questionField.text = query.Question;
    }

    public void resetAllButtons(AnswerField answerField)
    {
        foreach(AnswerField af in this.answerFieldList)
        {
            if (af.Index != answerField.Index)
            {
                af.GetComponent<Toggle>().isOn = false;
            }
        }
    }

    public void Check()
    {
        bool isSet = (((itemsCheck) & (1 << (2))) >= 1) ? true : false;
    }

    public void Next()
    {
        Exam exam = exams[examIndex];
        RenderScreen(exam);
    }
}
