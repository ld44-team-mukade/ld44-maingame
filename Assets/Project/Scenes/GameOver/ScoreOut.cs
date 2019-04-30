using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreOut : MonoBehaviour
{
    public Text scoreText;
    private int _score;
 
    // Start is called before the first frame update
    void Start()
    {
        _score = ScoreBoard.score;
        scoreText.text = _score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
