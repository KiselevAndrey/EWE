using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Action<bool> GameOver;

    [Header("UI")]
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Text gameOverText;
    [SerializeField] private GameObject NextLVLButton;
    [SerializeField] private GameObject RestartButton;

    [Header("Reference")]
    [SerializeField] private LVLSO lvlSO;

    private bool _gameOver;

    #region Awake OnDestroy Start
    private void Awake()
    {
        Exit.Finish += EndGame;
        Savior.RestartLvl += EndGame;
        Trap.ImCatch += EndGame;
    }

    private void OnDestroy()
    {
        Exit.Finish -= EndGame;
        Savior.RestartLvl -= EndGame;
        Trap.ImCatch -= EndGame;
    }

    private void Start()
    {
        gameOver.SetActive(false);
        Instantiate(lvlSO.GetCurrentLVL());
    }
    #endregion

    #region End Game
    public void EndGame(bool win)
    {
        GameOver(win);

        if (_gameOver) return;
        _gameOver = true;

        StartCoroutine(ShowMenu());
        NextLVLButton.SetActive(win);
        RestartButton.SetActive(!win);

        gameOverText.text = "Game Over\n\nYou " + (win ? "win" : "lose");

        if (win)
        {
            lvlSO.currentLVL++;
            lvlSO.CheckMaxLVL();
            SaveSystem.SaveLVLSO(lvlSO);
        }
    }

    private IEnumerator ShowMenu()
    {
        yield return new WaitForSeconds(2f);

        gameOver.SetActive(true);
    }
    #endregion

    #region SceneManager
    public void LoadScene(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoHome() => LoadScene(0);

    public void NextLVL()
    {
        if (lvlSO.HaveCurentLVL())
            ReloadScene();
        else GoHome();
    }
    #endregion
}
