using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SRC_ChangeScore : MonoBehaviour
{
    public string infoText;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    public void UpdateScore(int newScore)
    {
        text.text = infoText + newScore + "/8";
    }
}
