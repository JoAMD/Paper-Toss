using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeDetectorNew : MonoBehaviour
{
    [SerializeField]
    private Vector2 fingerUpPos, fingerDownPos;
    private float nextTime = 0;

    public Text windText;
    private float windSpeed = 0f;
    public Image windArrowImg;

    // Start is called before the first frame update
    void Start()
    {
        windSpeed = Random.Range(-5.89f, 5.89f);
        windText.text = "0";
        if(Data.instance.paperBallControllerNew == null)
        {
            windSpeed = 0;
        }
        else
        {
            Data.instance.paperBallControllerNew.getWindSpeed(windSpeed);
        }
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

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                //Just resetting and initialising the downPos
                fingerDownPos = touch.position;
                fingerUpPos = touch.position;
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                fingerUpPos = touch.position;
                if(Time.time > nextTime)
                {
                    processSwipeData();
                    nextTime = Time.time + 2f;
                }
            }
        }
    }

    private void processSwipeData()
    {
        if(Mathf.Abs(fingerUpPos.y - fingerDownPos.y) > 50f)
        {
            //Debug.Log("Direction = " + (fingerUpPos - fingerDownPos));
            Data.instance.slope = (fingerUpPos.y - fingerDownPos.y) / (fingerUpPos.x - fingerDownPos.x);
            Data.instance.startTime = Time.time;
            Data.instance.paperBallControllerNew.enabled = true;
        }
    }
}
