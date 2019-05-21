using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScript : MonoBehaviour
{
    public GameObject game;
    private bool once = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (once && !Data.instance.paperBallControllerNew.gameHasntStarted)
        {
            once = false;
            game.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
