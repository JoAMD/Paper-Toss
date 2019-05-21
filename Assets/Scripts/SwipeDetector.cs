using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    [SerializeField]
    private Vector2 fingerUpPos, fingerDownPos, sumDeltaPos = Vector2.zero;
    private float nextTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
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
            else if(touch.phase == TouchPhase.Moved)
            {
                sumDeltaPos += touch.deltaPosition;
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
            Debug.Log("sumDeltaPos = " + sumDeltaPos);
            Debug.Log("Distance = " + (fingerUpPos.y - fingerDownPos.y));
            sumDeltaPos = Vector2.zero;
        }
    }
}
