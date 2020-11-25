using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;

public class Login : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;

    public static System.DateTime lastLogin;
    System.DateTime currentLogin;

    public static string LoginToken;

    public void CallLogin()
    {
        //StartCoroutine(LoginPlayer());
        //StartCoroutine(GetRequest("http://103.239.222.212/ALIVEService/api/login/Generate?username=player1&password=2020Alive"));
        StartCoroutine(PostRequest());
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
            SceneManager.LoadScene(12);
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

    IEnumerator PostRequest()
    {
        WWWForm form = new WWWForm();
        WWW www = new WWW("http://103.239.222.212/ALIVEService/api/login/Generate?username=player1&password=2020Alive", form);
        yield return www;
        Debug.Log(www.text);
        LoginToken = www.text;

        currentLogin = System.DateTime.Now;
        string path = Application.persistentDataPath + "/PlayerSave.json";
        if (File.Exists(path))
        {
            Player.Load();
            if (currentLogin >= lastLogin.AddHours(18))
            {
                Player.coins += 10;
                Debug.Log("You got 10 coins for logging in today!");
                Player.lives += 3;
                Debug.Log("You got 3 lives for logging in today!");
            }
            else
            {
                Debug.Log("You have already logged in for the day!");
            }
        }
        else
        {
            Player.coins += 10;
            Debug.Log("You got 10 coins for logging in today!");
            Player.lives += 3;
            Debug.Log("You got 3 lives for logging in today!");
        }

        lastLogin = currentLogin;
        if (Player.donePreQuiz == false)
        {
            SceneManager.LoadScene(12);
            Player.donePreQuiz = true;
        }
        else if(Player.donePreQuiz == true)
        {
            SceneManager.LoadScene("Cutscene");
        }
        else
        {
            SceneManager.LoadScene("Cutscene");
        }
    }

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
        StartCoroutine(PostRequest());
        //SceneManager.LoadScene(12);
    }
}