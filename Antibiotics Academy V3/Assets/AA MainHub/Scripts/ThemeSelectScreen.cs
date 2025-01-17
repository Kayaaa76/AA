﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThemeSelectScreen : MonoBehaviour
{
    public bool IsThemeSelected; // bool to check if player has selected any theme

    public static bool IsClassic = false;
    public static bool IsYJ = false;
    public static bool IsTrixy = false;

    public GameObject AdvisePopUp; // warning message that pops up if player clicks ok without selecting a theme

    public GameObject pastelSelection;
    public GameObject classicSelection;
    public GameObject boldSelection;

    public void SelectPastel() // function that triggers when player clicks on the classic theme in the theme selection screen
    {
        IsThemeSelected = true; // set bool to true since player has selected a theme

        PlayerPrefs.SetString("Theme", "Pastel");

        pastelSelection.GetComponent<Outline>().enabled = true; //outline pastel selection
        classicSelection.GetComponent<Outline>().enabled = false; //remove outline on classic selection
        boldSelection.GetComponent<Outline>().enabled = false; //remove outline on bold selection
    }

    public void SelectClassic() // function that triggers when player clicks on the YJ's theme in the theme selection screen
    {
        IsThemeSelected = true; // set bool to true since player has selected a theme

        PlayerPrefs.SetString("Theme", "Classic");

        pastelSelection.GetComponent<Outline>().enabled = false; //remove outline on pastel selection
        classicSelection.GetComponent<Outline>().enabled = true; //outline classic selection
        boldSelection.GetComponent<Outline>().enabled = false; //remove outline on bold selection
    }

    public void SelectBold() // function that triggers when player clicks on the Trixy's theme in the theme selection screen
    {
        IsThemeSelected = true; // set bool to true since player has selected a theme

        PlayerPrefs.SetString("Theme", "Bold");

        pastelSelection.GetComponent<Outline>().enabled = false; //remove outline on pastel selection
        classicSelection.GetComponent<Outline>().enabled = false; //remove outline on classic selection
        boldSelection.GetComponent<Outline>().enabled = true; //outline bold selection
    }

    public void GoToCharacterSelect()
    {
        if(IsThemeSelected == true)
        {
            SceneManager.LoadScene(17); //character customization scene
        }
        else
        {
            StartCoroutine(AdvisePopUpTime());
        }
    }

    IEnumerator AdvisePopUpTime() // Coroutine function to let the warning text hover for a while before disappearing
    {
        AdvisePopUp.SetActive(true); // set the warning text to appear
        yield return new WaitForSeconds(2); // wait for 2 seconds 
        AdvisePopUp.SetActive(false); // make the warning text disappear
    }
}
