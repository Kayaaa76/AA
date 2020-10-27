using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
        //StartCoroutine(GetRequest("http://103.239.222.212/ALIVEService/api/login/Generate?username=player1&password=2020Alive"));
        //LoginPlayers();
    }

    IEnumerator LoginPlayer()
    {

        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        //WWW www = new WWW("http://localhost/sqlconnect/login.php", form);
        WWW www = new WWW("http://103.239.222.212/ALIVEService/api/login/Generate?username=player1&password=2020Alive", form);
        yield return www;
        if (www.text[0] == '0')
        {
            DBManager.username = nameField.text;
            //DBManager.score = int.Parse(www.text.Split('\t')[1]);
            SceneManager.LoadScene(15);
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }
    }
    //IEnumerator GetRequest(string uri)
    //{
    //    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    //    {
    //        // Request and wait for the desired page.
    //        yield return webRequest.SendWebRequest();

    //        string[] pages = uri.Split('/');
    //        int page = pages.Length - 1;

    //        if (webRequest.isNetworkError)
    //        {
    //            Debug.Log(pages[page] + ": Error: " + webRequest.error);
    //        }
    //        else
    //        {
    //            Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
    //        }
    //    }
    //}

    //void LoginPlayers()
    //{
    //    if (nameField.text == "testuser" && passwordField.text == "password")
    //    {
    //        SceneManager.LoadScene(12);
    //    }
    //    else
    //    {
    //        Debug.Log("Wrong credentials");
    //    }
    //}

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene(1);
    }

    public void BypassLogin()
    {
        SceneManager.LoadScene(15);
    }
}