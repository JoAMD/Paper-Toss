using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data instance;
    public float slope, startTime;
    public PaperBallControllerNew paperBallControllerNew;
    public Vector2 dustbinPos;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        paperBallControllerNew = GameObject.FindGameObjectWithTag("Paper Ball").GetComponent<PaperBallControllerNew>();
    }
}
