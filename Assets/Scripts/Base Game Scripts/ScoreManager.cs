using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private Board board;
    public TextMeshProUGUI scoreText;
    public int score;
    public Image scoreBar;
    private GameData gameData;
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + score;
    }
    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        if (gameData != null)
        {
            int highScores = gameData.saveData.highScores[board.level];
            if (score > highScores)
            {
                gameData.saveData.highScores[board.level] = score;
            }
            gameData.Save();
        }
        UpdateBar();
    }
    private void UpdateBar()
    {
        if (board != null && scoreBar != null)
        {
            int length = board.scoreGoals.Length;
            scoreBar.fillAmount = (float)score / (float)board.scoreGoals[length - 1];
        }
    }
}
