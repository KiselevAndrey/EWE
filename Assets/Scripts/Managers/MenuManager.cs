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
        Load();
    }
    #endregion

    #region For Buttons
    public void CreateLVLsButton()
    {
        for (int i = 0; i < lvlSO.maxLVL; i++)
        {
            Instantiate(buttonPrefab, scrollContent).GetComponent<SelectLVLBtn>().SetLVL(i + 1);
        }
    }

    public void DelLVLsButton()
    {
        foreach (var child in scrollContent.GetComponentsInChildren<SelectLVLBtn>())
        {
            Destroy(child.gameObject);
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
    #endregion

    #region Load
    private void Load()
    {
        SaveSystem.LoadLVLSO().LoadData(ref lvlSO);
    }
    #endregion
}
