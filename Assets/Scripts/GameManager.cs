using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Text gameOverText;
    [SerializeField] private GameObject NextLVLButton;
    [SerializeField] private GameObject RestartButton;


    #region Awake OnDestroy Start
    private void Awake()
    {
        Exit.Finish += EndGame;
        Savior.RestartLvl += EndGame;
    }

    private void OnDestroy()
    {
        Exit.Finish -= EndGame;
        Savior.RestartLvl -= EndGame;
    }

    private void Start()
    {
        gameOver.SetActive(false);
    }
    #endregion

    public void EndGame(bool win)
    {
        gameOver.SetActive(true);
        NextLVLButton.SetActive(win);
        RestartButton.SetActive(!win);

        gameOverText.text = "Game Over\n\nYou " + (win ? "win" : "lose"); 
    }
}
