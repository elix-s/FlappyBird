using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Scores: "+score;
    }
}