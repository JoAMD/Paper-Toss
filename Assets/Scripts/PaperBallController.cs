using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBallController : MonoBehaviour
{
    //later//private float windSpeed; //Add force using this 
    private float forwardForce, swipePower;

    //throw ball up
    private float pos, prevPos, prevScale, targetScale;

    [SerializeField]
    private bool isKeepBallMoving = true;//, isReachedMaxHeight = false;

    public float /*arbitrary = 500f / 168f,*/ initialSpeed = 500f;

    [SerializeField]
    public bool isPassedOnce = false;

    public float scaleDecrementStep = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        prevScale = transform.localScale.x;
        prevPos = transform.position.y;
        targetScale = prevScale - scaleDecrementStep;
    }

    // Update is called once per frame
    void Update()
    {
        if (isKeepBallMoving)
        {
            pos = (initialSpeed * Time.time) + (-250f * Time.time * Time.time / 2);
            //transform.position = new Vector3(0, pos, 0);
            transform.position = Vector3.MoveTowards(new Vector3(300, prevPos, 0), new Vector3(300, pos, 0), Time.deltaTime);
            prevPos = pos;

            if (targetScale >= 0.5f)
            {
                targetScale = prevScale - scaleDecrementStep;
            }
            transform.localScale = Vector3.Lerp(new Vector3(prevScale, prevScale, prevScale), new Vector3(targetScale, targetScale, targetScale), Time.deltaTime);
            prevScale = targetScale;
        }
            
        //Debug.Log(transform.localScale.x + " " + transform.position.y);
        Debug.Log(transform.position.y / transform.localScale.x);

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

        if(transform.position.y > (1.32f * initialSpeed) - 358)
        {
            isPassedOnce = true;
        }

        if (isPassedOnce && transform.position.y < (1.32f * initialSpeed) - 358)
        {
            isKeepBallMoving = false;
        }

    }
}
