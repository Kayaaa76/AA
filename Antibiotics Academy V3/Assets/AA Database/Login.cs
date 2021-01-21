using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;
using SimpleJSON;
using System;

public class Login : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public static InputField tnameField;
    public static InputField tpasswordField;

    public Button submitButton;

    public static System.DateTime lastLogin;
    System.DateTime currentLogin;

    public static string LoginToken;

    public static string GameSave;

    public static System.DateTime dateModified;

    void Update()
    {
        tnameField = nameField;
        tpasswordField = passwordField;
    }

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
            Debug.Log(wwwCheckVersion.error);
            Debug.Log(wwwCheckVersion.url);
            File.WriteAllText(tpath, GameSave);
            Debug.Log("game save written");

            string jsonString = File.ReadAllText(tpath);
            JSONObject gamesaveJson = (JSONObject)JSON.Parse(jsonString);

            dateModified = System.DateTime.Parse(gamesaveJson["dateModified"]);

            Debug.Log(dateModified);
            Debug.Log(System.DateTime.Now);
            #endregion

            #region if current game version is older
            if (dateModified > System.DateTime.Now/*.AddYears(-1)*/)
            {
                Debug.Log("Current Game Version is outdated!");
                WWW wwwGetAllNugQ = new WWW("http://103.239.222.212/ALIVE2Service/api/game/AllNugQ");
                yield return wwwGetAllNugQ;
                Debug.Log(wwwGetAllNugQ.text);
                Debug.Log(wwwGetAllNugQ.error);
                Debug.Log(wwwGetAllNugQ.url);

                File.WriteAllText(Application.persistentDataPath + "/AllNugQ.json", wwwGetAllNugQ.text);
                Debug.Log("AllNugQ written");

                string tjsonString = File.ReadAllText(Application.persistentDataPath + "/AllNugQ.json");
                Debug.Log(tjsonString);
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

                    WWWForm formPostCoinActivity = new WWWForm();
                    WWW wwwPostCoinActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Coins&" + "username=" + nameField.text + "&ActivityDataValue=" + "Player Coins", formPostCoinActivity);
                    yield return wwwPostCoinActivity;
                    Debug.Log(wwwPostCoinActivity.text);
                    Debug.Log(wwwPostCoinActivity.error);
                    Debug.Log(wwwPostCoinActivity.url);

                    WWWForm formPostLifeActivity = new WWWForm();
                    WWW wwwPostLifeActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Lifes&" + "username=" + nameField.text + "&ActivityDataValue=" + "Player Lifes", formPostLifeActivity);
                    yield return wwwPostLifeActivity;
                    Debug.Log(wwwPostLifeActivity.text);
                    Debug.Log(wwwPostLifeActivity.error);
                    Debug.Log(wwwPostLifeActivity.url);
                }
                else
                {
                    Debug.Log("You have already logged in for the day!");
                }
            }
            else
            {
                Debug.Log("Player save not detected!");

                WWWForm formInsertCoin = new WWWForm();
                WWW wwwInsertCoin = new WWW("http://103.239.222.212/ALIVE2Service/api/game/InsertUserInventory?InventoryCategoryName=Coins&username=" + nameField.text + "&UserInventoryValue=1", formInsertCoin);
                yield return wwwInsertCoin;
                Debug.Log(wwwInsertCoin.text);
                Debug.Log(wwwInsertCoin.error);
                Debug.Log(wwwInsertCoin.url);

                WWWForm formInsertLife = new WWWForm();
                WWW wwwInsertLife = new WWW("http://103.239.222.212/ALIVE2Service/api/game/InsertUserInventory?InventoryCategoryName=Life&username=" + nameField.text + "&UserInventoryValue=1", formInsertLife);
                yield return wwwInsertLife;
                Debug.Log(wwwInsertLife.text);
                Debug.Log(wwwInsertLife.error);
                Debug.Log(wwwInsertLife.url);

                Player.coins += 10;
                Debug.Log("You got 10 coins for logging in today!");
                Player.lives += 3;
                Debug.Log("You got 3 lives for logging in today!");
                Player.spunToday = false;
                Debug.Log("You get to spin the Reward Wheel!");

                #region update api
                //byte[] myData = System.Text.Encoding.UTF8.GetBytes("This is some test data");
                //UnityWebRequest www = UnityWebRequest.Put("http://103.239.222.212/ALIVE2Service/api/game/Update", myData);
                //www.method = "PATCH";
                //{
                //    yield return www.Send();

                //    if (www.isNetworkError || www.isHttpError)
                //    {
                //        Debug.Log(www.error);
                //    }
                //    else
                //    {
                //        Debug.Log("Upload complete!");
                //    }
                //}
                #endregion

                WWWForm formPostCoinActivity = new WWWForm();
                //WWW wwwPostActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=Player Coins&username=tony&ActivityDataValue=Player Coins", formPostActivity);
                WWW wwwPostCoinActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Coins&" + "username=" + nameField.text + "&ActivityDataValue=" + "Player Coins", formPostCoinActivity);
                yield return wwwPostCoinActivity;
                Debug.Log(wwwPostCoinActivity.text);
                Debug.Log(wwwPostCoinActivity.error);
                Debug.Log(wwwPostCoinActivity.url);

                WWWForm formPostLifeActivity = new WWWForm();
                WWW wwwPostLifeActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Lifes&" + "username=" + nameField.text + "&ActivityDataValue=" + "Player Lifes", formPostLifeActivity);
                yield return wwwPostLifeActivity;
                Debug.Log(wwwPostLifeActivity.text);
                Debug.Log(wwwPostLifeActivity.error);
                Debug.Log(wwwPostLifeActivity.url);
            }

            lastLogin = currentLogin;
            NotificationManager.CreateNotifChannel();
            NotificationManager.SendNotification();

            if (Player.donePreQuiz == false)
            {
                WWWForm formPostPreActivity = new WWWForm();
                WWW wwwPostPreActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Pre Quiz&" + "username=" + nameField.text + "&ActivityDataValue=" + "Pre Quiz", formPostPreActivity);
                yield return wwwPostPreActivity;
                Debug.Log(wwwPostPreActivity.text);
                Debug.Log(wwwPostPreActivity.error);
                Debug.Log(wwwPostPreActivity.url);

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