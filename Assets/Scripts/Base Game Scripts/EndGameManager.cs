using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum GameType
{
    Moves,
    Time,
}
[System.Serializable]
public class EndGameRequirement
{
    public GameType gameType;
    public int counterValue;
}
public class EndGameManager : MonoBehaviour
{
    public GameObject youWinPanel;
    public GameObject tryAgainPanel;
    public GameObject moveLabel;
    public GameObject timeLabel;
    public TextMeshProUGUI counter;
    public EndGameRequirement requirement;
    public int currentCounterValue;
    private Board board;
    private float timerSeconds;
    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.FindWithTag("Board").GetComponent<Board>();
        SetGameType();
        SetUpGame();
    }

    void SetGameType()
    {
        if (board.world != null)
        {
            if (board.level < board.world.levels.Length)
            {
                if (board.world.levels[board.level] != null)
                {
                    requirement = board.world.levels[board.level].endGameRequirement;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (requirement.gameType == GameType.Time && currentCounterValue > 0)
        {
            timerSeconds -= Time.deltaTime;
            if (timerSeconds <= 0)
            {
                DecreaseCounterValue();
                timerSeconds = 1;
            }
        }
    }
    void SetUpGame()
    {
        currentCounterValue = requirement.counterValue;
        if (requirement.gameType == GameType.Moves)
        {
            moveLabel.SetActive(true);
            timeLabel.SetActive(false);
        }
        else
        {
            timerSeconds = 1;
            moveLabel.SetActive(false);
            timeLabel.SetActive(true);
        }
        counter.text = "" + currentCounterValue;
    }
    public void DecreaseCounterValue()
    {
        if (board.currentState != GameState.pause)
        {
            currentCounterValue--;
            counter.text = "" + currentCounterValue;
            if (currentCounterValue <= 0)
            {
                LoseGame();
            }
        }
    }
    public void LoseGame()
    {
        tryAgainPanel.SetActive(true);
        board.currentState = GameState.lose;
        Debug.Log("You lose !");
        currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }
    public void WinGame()
    {
        youWinPanel.SetActive(true);
        board.currentState = GameState.win;
        Debug.Log("You win !");
        currentCounterValue = 0;
        counter.text = "" + currentCounterValue;
        FadePanelController fade = FindObjectOfType<FadePanelController>();
        fade.GameOver();
    }

}
