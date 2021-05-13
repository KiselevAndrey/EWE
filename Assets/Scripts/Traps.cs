using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    [SerializeField] private List<Trap> traps;

    public void ActiveTrap(bool value)
    {
        foreach (Trap trap in traps)
        {
            if (value) trap.Hunting();
            else trap.Hide();
        }
    }
}
