using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectedModule", menuName = "ScriptableObject/SelectedModule", order = 0)]
public class SelectedModule : ScriptableObject
{
    [SerializeField] private int selectedModule = 0;

    public int SelectedModuleValue
    {
        get => selectedModule;
        set => selectedModule = value;
    }
}