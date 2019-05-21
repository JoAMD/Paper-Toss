using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustbinScript : MonoBehaviour
{
    public PaperBallControllerNew paperBallControllerNew;
    public Text highScoreText, scoreText;

    private void Start()
    {
        Data.instance.dustbinPos = transform.position;
    }

    private void Update()
    {
        //if()
    }
    /*
    public void sss()
    {
        paperBallControllerNew.isInDustbin = true;
        paperBallControllerNew.transform.position = paperBallControllerNew.initialPos;
        paperBallControllerNew.transform.localScale = paperBallControllerNew.initialScale;
        int currentScore = int.Parse(scoreText.text);
        scoreText.text = (currentScore + 1).ToString();
        if (currentScore + 1 > int.Parse(highScoreText.text))
        {
            highScoreText.text = scoreText.text;
        }
    }
    */
}
