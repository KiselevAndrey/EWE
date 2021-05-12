using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectLVLBtn : MonoBehaviour
{
    static public Action<int> ClickMe;

    [SerializeField] private Text btnText;

    private int _lvl;

    public void SetLVL(int lvl)
    {
        _lvl = lvl;
        btnText.text = lvl.ToString();
    }

    public void OnClick() => ClickMe(_lvl);
}
