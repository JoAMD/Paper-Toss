using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Data.instance.paperBallControllerNew = GetComponent<PaperBallControllerNew>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
