using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{

    public TMP_Text tmp;

    void Start()
    {
        tmp = GetComponent<TMP_Text>();
    }

    void Update()
    {
        tmp.text = "score: " + Main.S.scoreBoard.ToString();
    }
}
