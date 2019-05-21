using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PaperBallControllerNew : MonoBehaviour
{
    //throw ball up
    private float posY, posX, prevYPos, prevXPos, prevScale, targetScale, maxY, accel = -250f;

    [SerializeField]
    private bool isKeepBallMoving = true;//, isReachedMaxHeight = false;

    public float  initialSpeed = 700f;

    [SerializeField]
    public bool isPassedOnce = false;

    //[SerializeField]
    private float scaleDecrementStep = 0.07f;//0.013f;

    //Get slope from swipeDetector
    private float slope = 1f, slope2 = 1f, startTime = 0f, slopeDiff = 0f;

    private bool isFirstTime = true;
    private float x0 = 0, y0 = 0;

    //[SerializeField]
    private float speed = 3f, dist = 250f;

    public Text scoreText, highScoreText;
    public bool isInDustbin = false, isInstantiated = false;
    public Vector2 initialPos, initialScale;
    //public GameObject paperBall;

    [SerializeField]
    private Text windText;
    private float windSpeed = 0f;
    [SerializeField]
    private Image windArrowImg;

    public bool gameHasntStarted = true;

    // Start is called before the first frame update
    void Start()
    {
        windArrowImg = GameObject.Find("WindArrow").GetComponent<Image>();
        windText = GameObject.Find("WindText").GetComponent<Text>();

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();


        Data.instance.paperBallControllerNew = this;
        slope = Data.instance.slope;
        startTime = Data.instance.startTime;

        prevScale = transform.localScale.x;
        initialScale = transform.localScale;
        //transform.position = 
        initialPos = new Vector2(giveY(0), 500);
        prevYPos = transform.position.y;
        prevXPos = prevYPos / slope;
        targetScale = prevScale - scaleDecrementStep;
        maxY = -initialSpeed * initialSpeed / (2 * accel);
        //Debug.Log("maxY = " + maxY);
        slopeDiff = (Mathf.PI / 180f) * (windSpeed * 75f / 5.89f);
    }

    // Update is called once per frame
    void Update()
    {

        //if near dustbin
        //Debug.Log("Data.instance.dustbinPos" + Data.instance.dustbinPos);
        //Debug.Log("transform.position" + transform.position);

        if (isPassedOnce && Mathf.Abs(Data.instance.dustbinPos.y - transform.position.y) < 130 && Mathf.Abs(Data.instance.dustbinPos.x - transform.position.x) < 70)
        {
            //Debug.Log("sdf$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
            isInDustbin = true;
            int currentScore = int.Parse(scoreText.text);
            scoreText.text = (currentScore + 1).ToString();
            if (currentScore + 1 > int.Parse(highScoreText.text))
            {
                highScoreText.text = scoreText.text;
                PlayerPrefs.SetInt("HighScore", currentScore + 1);
            }
            /*
            enabled = false;
            isKeepBallMoving = true;
            isPassedOnce = false;
            tag = "Paper Ball";
            isInstantiated = false;
            doWindStuff();
            transform.position = initialPos;
            transform.localScale = initialScale;
            */

            isKeepBallMoving = true;
            isPassedOnce = false;
            GameObject gb = Instantiate(gameObject, initialPos, Quaternion.identity, GameObject.Find("Canvas").transform);
            gb.tag = "Paper Ball";
            //Debug.Log("############################################");
            Data.instance.paperBallControllerNew = gb.GetComponent<PaperBallControllerNew>();
            Data.instance.paperBallControllerNew.enabled = false;
            Data.instance.paperBallControllerNew.isInstantiated = true;
            Data.instance.paperBallControllerNew.isInstantiated = false;
            Data.instance.paperBallControllerNew.doWindStuff();
            Data.instance.paperBallControllerNew.isPassedOnce = false;
            gb.transform.position = initialPos;
            gb.transform.localScale = initialScale;
            this.enabled = false;
            Destroy(gameObject);
        }


        float t = (Time.time - startTime) * speed;
        if (isKeepBallMoving)
        {
            posY = giveY(t);
            //Debug.Log(posY);
            if (t < -initialSpeed / accel)
            {
                posX = posY / slope + 500;
            }
            else
            {
                slope2 = Mathf.Tan(Mathf.PI / 2 + slopeDiff); //Mathf.Tan(Mathf.PI - Mathf.Atan(slope)); 
                //Debug.Log("maxY - posY = " + (maxY - posY));
                //Debug.Log("slope2 = " + (slope2));
                if (isFirstTime)
                {
                    y0 = transform.position.y;
                    //x0 = y0 / slope;
                    x0 = transform.position.x;
                    isFirstTime = false;
                }
                posX = ((posY - y0)) / slope2 + (x0) ;
                //posX = (slope2 * (maxY - posY)) + (maxY / slope2);
                //Debug.Log("posX = " + posX);
            }
            //transform.position = new Vector3(0, pos, 0);
            transform.position = Vector3.MoveTowards(new Vector2(prevXPos, prevYPos), new Vector2(posX, posY), Time.deltaTime);
            prevYPos = posY;
            prevXPos = posX;

            if (targetScale >= 0.5f)
            {
                targetScale = prevScale - scaleDecrementStep;
            }
            transform.localScale = Vector3.Lerp(new Vector3(prevScale, prevScale, prevScale), new Vector3(targetScale, targetScale, targetScale), Time.deltaTime);
            prevScale = targetScale;
        }
        else
        {
            Debug.Log(transform.position);
            if (gameHasntStarted)
            {
                scoreText.text = "0";
                gameHasntStarted = false;
            }


            gameObject.tag = "Old Ball";
            gameObject.GetComponent<SendData>().enabled = false;
            //SceneManager.LoadScene(0);
            //if (!isInDustbin)
            {
                isInDustbin = false;
                scoreText.text = "0";
                if (!isInstantiated)
                {
                    isKeepBallMoving = true;
                    isPassedOnce = false;
                    GameObject gb = Instantiate(gameObject, initialPos, Quaternion.identity, GameObject.Find("Canvas").transform);
                    gb.tag = "Paper Ball";
                    //Debug.Log("############################################");
                    Data.instance.paperBallControllerNew = gb.GetComponent<PaperBallControllerNew>();
                    Data.instance.paperBallControllerNew.enabled = false;
                    Data.instance.paperBallControllerNew.isInstantiated = true;
                    Data.instance.paperBallControllerNew.isInstantiated = false;
                    Data.instance.paperBallControllerNew.doWindStuff();
                    gb.transform.position = initialPos;
                    gb.transform.localScale = initialScale;
                    gb.name = "Paper Ball(Clone)";
                }

            }

            if (transform.position.x <= 230 || transform.position.x >= 890)
            {
                Destroy(gameObject);
            }
            this.enabled = false;
        }
            
        //Debug.Log(transform.localScale.x + " " + transform.position.y);
        //Debug.Log(transform.position.y / transform.localScale.x);

        /*
        if (transform.position.y / transform.localScale.x > (initialSpeed/2.38f))
        {
            isReachedMaxHeight = true;
        }

        if (isReachedMaxHeight && transform.position.y / transform.localScale.x < initialSpeed / arbitrary /*(400/2.38f)*//*)
        {
            isKeepBallMoving = false;
        }
        */

        if(transform.position.y > (1.32f * initialSpeed + dist + 100))
        {
            isPassedOnce = true;
        }

        if (isPassedOnce && transform.position.y < (1.32f * initialSpeed + dist - 160))
        {
            isKeepBallMoving = false;
        }

    }
    private float giveY(float t)
    {
        return (initialSpeed * t) + (accel * t * t / 2) + 300;
    }

    public void getSlope(float slope, float startTime)
    {
        this.slope = slope;
        this.startTime = startTime;
    }

    private void doWindStuff()
    {
        //Wind Stuff
        windSpeed = Random.Range(-5f, 5f);
        windText.text = windSpeed.ToString();
        if (windSpeed < 0)
        {
            windArrowImg.transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            windArrowImg.transform.localScale = new Vector2(1, 1);
        }
    }

    public void getWindSpeed(float windSpeed)
    {
        this.windSpeed = windSpeed;
    }
    
}
