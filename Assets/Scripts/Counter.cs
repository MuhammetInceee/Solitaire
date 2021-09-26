using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    //[SerializeField] private TextMeshProUGUI _timerText;

    public static int _score;

    void Update()
    {
        _scoreText.text = "Score : " + _score;
    }
}
