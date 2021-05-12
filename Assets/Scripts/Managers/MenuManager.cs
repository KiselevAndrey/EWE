using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private LVLSO lvlSO;
    [SerializeField] private Transform scrollContent;
    [SerializeField] private GameObject buttonPrefab;

    #region Awake OnDestroy Start
    private void Awake()
    {
        SelectLVLBtn.ClickMe += StartLVL;
    }

    private void OnDestroy()
    {
        SelectLVLBtn.ClickMe -= StartLVL;
    }

    private void Start()
    {
        SaveSystem.LoadLVLSO().LoadData(ref lvlSO);
    }
    #endregion

    public void CreateLVLsButton()
    {
        for (int i = 0; i < lvlSO.LVLObjects.Count; i++)
        {
            Instantiate(buttonPrefab, scrollContent).GetComponent<SelectLVLBtn>().SetLVL(i + 1);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void StartLVL(int lvl)
    {
        lvlSO.currentLVL = lvl - 1;

        SceneManager.LoadScene(1);
    }
}
