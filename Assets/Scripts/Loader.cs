using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public GameObject data;
    public Text highScoreText;

    private void Awake()
    {
        if(Data.instance == null)
        {
            Instantiate(data);
        }
    }

    private void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
}
