using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;
using SimpleJSON;

public class Login : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;

    public static System.DateTime lastLogin;
    System.DateTime currentLogin;

    public static string LoginToken;

    public static string GameSave;

    public static System.DateTime dateModified;

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
        //StartCoroutine(GetRequest("http://103.239.222.212/ALIVEService/api/login/Generate?username=player1&password=2020Alive"));
        //StartCoroutine(PostRequest());
        //LoginPlayers();
    }

    //IEnumerator LoginPlayer()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField("name", nameField.text);
    //    form.AddField("password", passwordField.text);
    //    //WWW www = new WWW("http://localhost/sqlconnect/login.php", form);
    //    WWW www = new WWW("http://103.239.222.212/ALIVEService/api/login/Generate?username=player1&password=2020Alive", form);
    //    yield return www;
    //    if (www.text[0] == '0')
    //    {
    //        DBManager.username = nameField.text;
    //        //DBManager.score = int.Parse(www.text.Split('\t')[1]);
    //        SceneManager.LoadScene(12);
    //    }
    //    else
    //    {
    //        Debug.Log("User login failed. Error #" + www.text);
    //    }
    //}

    IEnumerator LoginPlayer()
    {
        WWWForm formLogin = new WWWForm();
        WWW wwwLogin = new WWW("http://103.239.222.212/ALIVE2Service/api/login/Generate?username=" + nameField.text + "&password=" + passwordField.text, formLogin);
        yield return wwwLogin;

        Debug.Log(wwwLogin.text);
        Debug.Log(wwwLogin.error);
        Debug.Log(wwwLogin.url);

        currentLogin = System.DateTime.Now;

        string path = Application.persistentDataPath + "/PlayerSave.json";
        string tpath = Application.persistentDataPath + "/GameSave.json";
        
        if(wwwLogin.error == null)
        {
            Debug.Log("Login Successful!");

            #region Update Login Activity
            WWWForm formUpdateLogin = new WWWForm();
            WWW wwwUpdateLogin = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostLogin?ActivityTypeName=Login&Username=" + nameField.text,formUpdateLogin);
            yield return wwwUpdateLogin;
            Debug.Log(wwwUpdateLogin.text);
            Debug.Log(wwwUpdateLogin.error);
            Debug.Log(wwwUpdateLogin.url);
            #endregion

            #region Check Game Version
            //WWWForm formCheckVersion = new WWWForm();
            WWW wwwCheckVersion = new WWW("http://103.239.222.212/ALIVE2Service/api/game/GameSave");
            yield return wwwCheckVersion;
            GameSave = wwwCheckVersion.text;
            //Debug.Log(GameSave);
            Debug.Log(wwwCheckVersion.text);
            File.WriteAllText(tpath, GameSave);
            Debug.Log("game save written");

            string jsonString = File.ReadAllText(tpath);
            JSONObject gamesaveJson = (JSONObject)JSON.Parse(jsonString);

            dateModified = System.DateTime.Parse(gamesaveJson["dateModified"]);

            Debug.Log(dateModified);
            Debug.Log(System.DateTime.Now);
            #endregion

            #region if current game version is older
            if (dateModified > System.DateTime.Now      /*.AddYears(-1)*/)
            {
                Debug.Log("Current Game Version is outdated!");
                WWW wwwGetAllNugQ = new WWW("http://103.239.222.212/ALIVE2Service/api/game/AllNugQ");
                yield return wwwGetAllNugQ;
                Debug.Log(wwwGetAllNugQ.text);
                Debug.Log(wwwGetAllNugQ.error);
                Debug.Log(wwwGetAllNugQ.url);
            }
            else if (dateModified < System.DateTime.Now)
            {
                Debug.Log("Game is running the correct version!");
            }
            #endregion

            if (File.Exists(path))
            {
                Player.Load();
                if (currentLogin >= lastLogin.AddHours(18))
                {
                    Player.coins += 10;
                    Debug.Log("You got 10 coins for logging in today!");
                    Player.lives += 3;
                    Debug.Log("You got 3 lives for logging in today!");
                    Player.spunToday = false;
                    Debug.Log("You get to spin the Reward Wheel!");
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
                Player.spunToday = false;
                Debug.Log("You get to spin the Reward Wheel!");
            }

            lastLogin = currentLogin;
            NotificationManager.CreateNotifChannel();
            NotificationManager.SendNotification();

            if (Player.donePreQuiz == false)
            {
                SceneManager.LoadScene(12);
                Player.donePreQuiz = true;
            }
            else if (Player.donePreQuiz == true)
            {
                SceneManager.LoadScene("Cutscene");
            }
            else
            {
                SceneManager.LoadScene("Cutscene");
            }
        }
        else
        {
            Debug.Log("Login Failed!");
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