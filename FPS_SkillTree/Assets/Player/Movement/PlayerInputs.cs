using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Other actions that can make the player, like toggling menu etc etc
//(Name doesn't fit well)
public class PlayerInputs : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] KeyCode toggleUIKey = KeyCode.Tab;

    [SerializeField] GameObject skillTree;

    private void Update()
    {
        if (Input.GetKeyDown(toggleUIKey))
            ToggleUI();
    }

    void ToggleUI()
    {
        Debug.Log("toggle");
        skillTree.SetActive(!skillTree.activeSelf);
    }
}
