using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMenuOnStart : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    private void Awake()
    {
        menu.SetActive(false);
    }
}
