using Examination;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerField : MonoBehaviour {

    [SerializeField]
    private Toggle answerToggle;
    [SerializeField]
    private Text answerText;
    [SerializeField]
    private Image answerTypeImage;
    [SerializeField]
    private Image answerCheckImage;
    [SerializeField]
    private Sprite radialSprite;
    [SerializeField]
    private Sprite radialTickedSprite;
    [SerializeField]
    private Sprite tickboxSprite;
    [SerializeField]
    private Sprite tickboxTickedSprite;

    private int index;
    [SerializeField]
    private QueryType answerType;
    private Answer answer;

    public int Index { get { return index; } set { index = value; } }
    public string Value { get { return answerText.text; } set { answerText.text = value; } }
    public Answer Answer { get { return answer; } set { answer = value; answerText.text = answer.Result; } }
    public QueryType AnswerType { get { return answerType; } set
        {
            answerType = value;

            switch (answerType)
            {
                case QueryType.CLOSED_SINGLE:
                    if (radialSprite != null && radialTickedSprite != null)
                    {
                        answerTypeImage.sprite = radialSprite;
                        answerCheckImage.sprite = radialTickedSprite;
                    }
                    break;
                case QueryType.CLOSED_MULTIPLE:
                    if (tickboxSprite != null && tickboxTickedSprite != null)
                    {
                        answerTypeImage.sprite = tickboxSprite;
                        answerCheckImage.sprite = tickboxTickedSprite;
                    }
                    break;
            }
        } }
}
