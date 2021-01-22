using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomizationScreen : MonoBehaviour
{
    public bool IsGenderSelected; // bool to check if player has selected any character

    public GameObject AdvisePopUp; // warning message that pops up if player clicks ok without selecting a character

    public Sprite pastelMenuBg; //pastel or bold theme background
    public Sprite classicMenuBg; //classic theme background

    public GameObject femaleSelection; //female selection option
    public GameObject maleSelection; //male selection option

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("Theme") == "Pastel" || PlayerPrefs.GetString("Theme") == "Bold")
        {
            gameObject.GetComponent<Image>().sprite = pastelMenuBg;
        }
        else if(PlayerPrefs.GetString("Theme") == "Classic")
        {
            gameObject.GetComponent<Image>().sprite = classicMenuBg;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectMale() // function that triggers when player clicks on the male character in the character selection screen
    {
        //PlayerCharacterCustomization.IsMale = true; // set bool IsMale in the PlayerCharacterCustomization script to be true
        IsGenderSelected = true; // set bool to true since player has selected a character

        PlayerPrefs.SetString("Gender", "Male");

        maleSelection.GetComponent<Outline>().enabled = true; //outline male selection
        femaleSelection.GetComponent<Outline>().enabled = false; //remove outline on female selection
    }

    public void SelectFemale() // function that triggers when player clicks on the female character in the character selection screen
    {
        //PlayerCharacterCustomization.IsMale = false; // set bool IsMale in the PlayerCharacterCustomization script to be false
        IsGenderSelected = true; // set bool to true since player has selected a character

        PlayerPrefs.SetString("Gender", "Female");

        maleSelection.GetComponent<Outline>().enabled = false; //remove outline on male selection
        femaleSelection.GetComponent<Outline>().enabled = true; //outline female selection
    }

    public void GoToMainScene() // function to go to main scene after selecting a character
    {
        if (IsGenderSelected == true) // if player has selected a character
        {
            SceneManager.LoadScene("Main OG"); // go to main scene
        }
        else // if player has not selected a character
        {
            StartCoroutine(AdvisePopUpTime()); // warning message appear
        }
    }

    //public void GoToMainScene() // function to go to main scene after selecting a character
    //{
    //    if (IsGenderSelected == true && ThemeSelectScreen.IsClassic == true) // if player has selected classic theme
    //    {
    //        SceneManager.LoadScene("Main OG"); // go to main og scene
    //    }
    //    else if (IsGenderSelected == true && ThemeSelectScreen.IsYJ == true)
    //    {
    //        SceneManager.LoadScene("Main YJ");
    //    }
    //    else if (IsGenderSelected == true && ThemeSelectScreen.IsTrixy == true)
    //    {
    //        SceneManager.LoadScene("Main Trixy");
    //    }
    //    else // if player has not selected a character
    //    {
    //        StartCoroutine(AdvisePopUpTime()); // warning message appear
    //    }
    //}

    IEnumerator AdvisePopUpTime() // Coroutine function to let the warning text hover for a while before disappearing
    {
        AdvisePopUp.SetActive(true); // set the warning text to appear
        yield return new WaitForSeconds(2); // wait for 2 seconds 
        AdvisePopUp.SetActive(false); // make the warning text disappear
    }
}
