using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Text gameOverText;
    [SerializeField] private GameObject NextLVLButton;
    [SerializeField] private GameObject RestartButton;

    [Header("Reference")]
    [SerializeField] private LVLSO lvlSO;

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
        Instantiate(lvlSO.GetCurrentLVL());
    }
    #endregion

    #region End Game
    public void EndGame(bool win)
    {
        gameOver.SetActive(true);
        NextLVLButton.SetActive(win);
        RestartButton.SetActive(!win);

        gameOverText.text = "Game Over\n\nYou " + (win ? "win" : "lose"); 
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
        lvlSO.currentLVL++;
        if (lvlSO.HaveCurentLVL())
            ReloadScene();
        else GoHome();
    }
    #endregion
}
