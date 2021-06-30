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
    public InputField nameField;                //Input Username
    public InputField passwordField;            //Input Password

    public static InputField tnameField;        
    public static InputField tpasswordField;    

    public Button submitButton;
    public GameObject loginError;

    public static System.DateTime dateModified;
    public static System.DateTime lastLogin;
    System.DateTime currentLogin;

    public static string LoginToken;
    public static string GameSave;

    string nuggetName;
    int i = 1;

    string questionName;
    int x = 1;

    public static bool outdated = false;

    public static string PlayerID;
    public static string PlayerIDCoin;
    public static string PlayerIDLive;

    void Start() //Originally in update, but used in start to reduce load, but switch back to update if suddenly not working.
    {
        tnameField = nameField;
        tpasswordField = passwordField;
    }

    //void Update()
    //{
    //    tnameField = nameField;
    //    tpasswordField = passwordField;
    //}

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    IEnumerator LoginPlayer()
    {
        WWWForm formLogin = new WWWForm();
        //WWW wwwLogin = new WWW("http://103.239.222.212/ALIVE2Service/api/login/Generate?username=" + nameField.text + "&password=" + passwordField.text, formLogin);
        WWW wwwLogin = new WWW("http://www.stewards.com.sg/ALIVE2Service/api/login/Generate?username=" + nameField.text + "&password=" + passwordField.text, formLogin);
        yield return wwwLogin;

        currentLogin = System.DateTime.Now;

        string path = Application.persistentDataPath + "/PlayerSave.json";
        string tpath = Application.persistentDataPath + "/GameSave.json";

        if (wwwLogin.error == null)
        {
            Debug.Log("Login Successful!");

            #region Update Login Activity
            WWWForm formUpdateLogin = new WWWForm();
            //WWW wwwUpdateLogin = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostLogin?ActivityTypeName=Login&Username=" + nameField.text, formUpdateLogin);
            WWW wwwUpdateLogin = new WWW("http://www.stewards.com.sg/ALIVE2Service/api/game/PostLogin?ActivityTypeName=Login&Username=" + nameField.text, formUpdateLogin);
            yield return wwwUpdateLogin;
            //Debug.Log(wwwUpdateLogin.text);
            //Debug.Log(wwwUpdateLogin.error);
            //Debug.Log(wwwUpdateLogin.url);

            File.WriteAllText(Application.persistentDataPath + "/PlayerInformation.json", wwwUpdateLogin.text);

            string userInfoString = File.ReadAllText(Application.persistentDataPath + "/PlayerInformation.json");
            JSONObject userInfoJson = (JSONObject)JSON.Parse(userInfoString);

            PlayerID = userInfoJson["userID"];
            //Debug.Log(PlayerID);
            #endregion

            #region Check Game Version
            
            WWW wwwCheckVersion = new WWW("http://www.stewards.com.sg/ALIVE2Service/api/game/GameSave");
            yield return wwwCheckVersion;
            GameSave = wwwCheckVersion.text;
            File.WriteAllText(tpath, GameSave);
            Debug.Log("game save written");

            //WWWForm formCheckVersion = new WWWForm();
            //WWW wwwCheckVersion = new WWW("http://103.239.222.212/ALIVE2Service/api/game/GameSave");
            //Debug.Log(GameSave);
            //Debug.Log(wwwCheckVersion.text);
            //Debug.Log(wwwCheckVersion.error);
            //Debug.Log(wwwCheckVersion.url);

            string jsonString = File.ReadAllText(tpath);
            JSONObject gamesaveJson = (JSONObject)JSON.Parse(jsonString);

            dateModified = System.DateTime.Parse(gamesaveJson["dateModified"]);

            Debug.Log(dateModified);
            Debug.Log(System.DateTime.Now);
            #endregion

            if (File.Exists(path)) //if player's saved data exists
            {
                Player.Load();
                if (currentLogin >= lastLogin.AddHours(18))
                {
                    Player.coins += 10;
                    Debug.Log("You got 10 coins for logging in today!");

                    Player.lives += 3;
                    Debug.Log("You got 3 lives for logging in today!");

                    if (Player.lives > 5)
                    {
                        Player.lives = 5;
                        Debug.Log("You can only have 5 lives!");
                    }

                    Player.spunToday = false;
                    Debug.Log("You get to spin the Reward Wheel!");

                    string userInvIDCoinString = File.ReadAllText(Application.persistentDataPath + "/userInventoryIDCoin.json");
                    JSONObject userInvIDCoinJson = (JSONObject)JSON.Parse(userInvIDCoinString);
                    PlayerIDCoin = userInvIDCoinJson["userInventoryID"];

                    string userInvIDLiveString = File.ReadAllText(Application.persistentDataPath + "/userInventoryIDLive.json");
                    JSONObject userInvIDLiveJson = (JSONObject)JSON.Parse(userInvIDLiveString);
                    PlayerIDLive = userInvIDLiveJson["userInventoryID"];

                    WWWForm formPostCoinActivity = new WWWForm();
                    //WWW wwwPostCoinActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Coins&" + "username=" + nameField.text + "&ActivityDataValue=" + "Player Coins", formPostCoinActivity);
                    WWW wwwPostCoinActivity = new WWW("http://www.stewards.com.sg/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Coins&" + "username=" + nameField.text + "&ActivityDataValue=" + "Player Coins", formPostCoinActivity);
                    yield return wwwPostCoinActivity;

                    WWWForm formPostLifeActivity = new WWWForm();
                    //WWW wwwPostLifeActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Lifes&" + "username=" + nameField.text + "&ActivityDataValue=" + "Player Lifes", formPostLifeActivity);
                    WWW wwwPostLifeActivity = new WWW("http://www.stewards.com.sg/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Lifes&" + "username=" + nameField.text + "&ActivityDataValue=" + "Player Lifes", formPostLifeActivity);
                    yield return wwwPostLifeActivity;

                    StartCoroutine(UpdateCoins());
                    StartCoroutine(UpdateLives());
                    Player.Save();
                }
                else
                {
                    string userInvIDCoinString = File.ReadAllText(Application.persistentDataPath + "/userInventoryIDCoin.json");
                    JSONObject userInvIDCoinJson = (JSONObject)JSON.Parse(userInvIDCoinString);
                    PlayerIDCoin = userInvIDCoinJson["userInventoryID"];

                    string userInvIDLiveString = File.ReadAllText(Application.persistentDataPath + "/userInventoryIDLive.json");
                    JSONObject userInvIDLiveJson = (JSONObject)JSON.Parse(userInvIDLiveString);
                    PlayerIDLive = userInvIDLiveJson["userInventoryID"];
                    Debug.Log("You have already logged in for the day!");
                }
            }
            else //if player saved file doesn't exist
            {
                Debug.Log("Player save not detected!");

                Player.coins += 10;
                Debug.Log("You got 10 coins for logging in today!");
                Player.lives += 3;
                Debug.Log("You got 3 lives for logging in today!");
                Player.spunToday = false;
                Debug.Log("You get to spin the Reward Wheel!");
                Player.preGameQuizTime = "";
                Player.postGameQuizTime = "";
                Player.m3Duration = "";
                Player.TDDuration = "";
                Player.RunnerDuration = "";
                Player.totalDuration = "";
                Player.dateStart = DateTime.Now;
                //Player.tLastLogin = Player.dateStart;
                lastLogin = Player.dateStart;
                Player.Save();

                WWWForm formPostCoinActivity = new WWWForm();
                WWW wwwPostCoinActivity = new WWW("http://www.stewards.com.sg/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Coins&" + "username=" + nameField.text + "&ActivityDataValue=" + "Player Coins", formPostCoinActivity);
                yield return wwwPostCoinActivity;
                
                //WWW wwwPostActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=Player Coins&username=tony&ActivityDataValue=Player Coins", formPostActivity);
                //WWW wwwPostCoinActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Coins&" + "username=" + nameField.text + "&ActivityDataValue=" + "Player Coins", formPostCoinActivity);
                //Debug.Log(wwwPostCoinActivity.text);
                //Debug.Log(wwwPostCoinActivity.error);
                //Debug.Log(wwwPostCoinActivity.url);

                WWWForm formInsertPlayerCoin = new WWWForm();            
                WWW wwwInsertPlayerCoin = new WWW("http://www.stewards.com.sg/ALIVE2Service/api/game/InsertUserInventory?UserInventoryValue=10&userID=" + PlayerID + "&inventoryCategoryID=324dcb99-4e2c-4282-d5a2-08d8966455ad", formInsertPlayerCoin);
                yield return wwwInsertPlayerCoin;
                
                //WWW wwwInsertPlayerCoin = new WWW("http://103.239.222.212/ALIVE2Service/api/game/InsertUserInventory?UserInventoryValue=10&userID=" + PlayerID + "&inventoryCategoryID=324dcb99-4e2c-4282-d5a2-08d8966455ad", formInsertPlayerCoin);
                //Debug.Log(wwwInsertPlayerCoin.text);
                //Debug.Log(wwwInsertPlayerCoin.error);
                //Debug.Log(wwwInsertPlayerCoin.url);

                
                File.WriteAllText(Application.persistentDataPath + "/userInventoryIDCoin.json", wwwInsertPlayerCoin.text);

                WWWForm formPostLifeActivity = new WWWForm();
                WWW wwwPostLifeActivity = new WWW("http://www.stewards.com.sg/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Lifes&" + "username=" + nameField.text + "&ActivityDataValue=" + "Player Lifes", formPostLifeActivity);
                yield return wwwPostLifeActivity;

                //WWW wwwPostLifeActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Lifes&" + "username=" + nameField.text + "&ActivityDataValue=" + "Player Lifes", formPostLifeActivity);
                //Debug.Log(wwwPostLifeActivity.text);
                //Debug.Log(wwwPostLifeActivity.error);
                //Debug.Log(wwwPostLifeActivity.url);

                WWWForm formInsertPlayerLive = new WWWForm();
                WWW wwwInsertPlayerLive = new WWW("http://www.stewards.com.sg/ALIVE2Service/api/game/InsertUserInventory?UserInventoryValue=3&userID=" + PlayerID + "&inventoryCategoryID=4428d6b7-07e2-4d6a-a0b0-e2bf9efdaaf6", formInsertPlayerLive);
                yield return wwwInsertPlayerLive;

                //WWW wwwInsertPlayerLive = new WWW("http://103.239.222.212/ALIVE2Service/api/game/InsertUserInventory?UserInventoryValue=3&userID=" + PlayerID + "&inventoryCategoryID=4428d6b7-07e2-4d6a-a0b0-e2bf9efdaaf6", formInsertPlayerLive);
                //Debug.Log(wwwInsertPlayerLive.text);
                //Debug.Log(wwwInsertPlayerLive.error);
                //Debug.Log(wwwInsertPlayerLive.url);

                File.WriteAllText(Application.persistentDataPath + "/userInventoryIDLive.json", wwwInsertPlayerLive.text);

                string userInvIDCoinString = File.ReadAllText(Application.persistentDataPath + "/userInventoryIDCoin.json");
                JSONObject userInvIDCoinJson = (JSONObject)JSON.Parse(userInvIDCoinString);
                PlayerIDCoin = userInvIDCoinJson["userInventoryID"];

                string userInvIDLiveString = File.ReadAllText(Application.persistentDataPath + "/userInventoryIDLive.json");
                JSONObject userInvIDLiveJson = (JSONObject)JSON.Parse(userInvIDLiveString);
                PlayerIDLive = userInvIDLiveJson["userInventoryID"];
            }

            #region if current game version is older
            Debug.Log(lastLogin);
            if (dateModified > lastLogin)
            {
                Debug.Log("Current Game Version is outdated!");
                outdated = true;
                WWW wwwGetAllNugQ = new WWW("http://103.239.222.212/ALIVE2Service/api/game/AllNugQ");
                yield return wwwGetAllNugQ;
                //Debug.Log(wwwGetAllNugQ.text);
                //Debug.Log(wwwGetAllNugQ.error);
                //Debug.Log(wwwGetAllNugQ.url);

                File.WriteAllText(Application.persistentDataPath + "/AllNugQ.json", wwwGetAllNugQ.text);
                //Debug.Log("AllNugQ written");

                string tjsonString = File.ReadAllText(Application.persistentDataPath + "/AllNugQ.json");
                //Debug.Log(tjsonString);

                RootObject myObject = new RootObject();

                myObject = JsonUtility.FromJson<RootObject>("{\"sets\":" + tjsonString + "}");

                foreach (AllNugQ set in myObject.sets)
                {
                    nuggetName = "InfoNug " + i;
                    Debug.Log(nuggetName);
                    questionName = "Q" + x;
                    Debug.Log(questionName);
                    //Debug.Log("infoNuggetName: " + set.infoNugget.infoNuggetName + "\n infoNuggetDescription: " + set.infoNugget.infoNuggetDescription + "\n infoNuggetValue: " + set.infoNugget.infoNuggetValue);
                    if (set.infoNugget.infoNuggetName == nuggetName)
                    {
                        //Debug.Log(set.infoNugget.infoNuggetName + "\n" + set.infoNugget.infoNuggetDescription + "\n"  + set.infoNugget.infoNuggetValue);

                        string filename = "InfoNug " + i;
                        InfoNuggetA Nugget = new InfoNuggetA();
                        Nugget.infoNuggetName = set.infoNugget.infoNuggetName;
                        Nugget.infoNuggetDescription = set.infoNugget.infoNuggetDescription;
                        Nugget.infoNuggetValue = set.infoNugget.infoNuggetValue;
                        string nuggetJson = JsonUtility.ToJson(Nugget);
                        File.WriteAllText(Application.persistentDataPath + "/" + filename + ".json", nuggetJson);
                        i++;
                    }

                    if (set.question.questionName == questionName)
                    {
                        string filename = "Q" + x;
                        InfoNuggetB Question = new InfoNuggetB();
                        Question.questionName = set.question.questionName;
                        Question.questionDescription = set.question.questionDescription;
                        Question.questionValue = set.question.questionValue;
                        string questionJson = JsonUtility.ToJson(Question);
                        File.WriteAllText(Application.persistentDataPath + "/" + filename + ".json", questionJson);
                        x++;
                    }
                }
            }
            else if (dateModified < System.DateTime.Now)
            {
                Debug.Log("Game is running the correct version!");
            }
            #endregion

            if (Player.donePreQuiz == false)
            {
                WWWForm formPostPreActivity = new WWWForm();
                //WWW wwwPostPreActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Pre Quiz&" + "username=" + nameField.text + "&ActivityDataValue=" + "Pre Quiz", formPostPreActivity);
                WWW wwwPostPreActivity = new WWW("http://www.stewards.com.sg/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Pre Quiz&" + "username=" + nameField.text + "&ActivityDataValue=" + "Pre Quiz", formPostPreActivity);
                yield return wwwPostPreActivity;

                SceneManager.LoadScene(12);
                Player.donePreQuiz = true;
            }
            else if (Player.donePreQuiz == true)
            {
                SceneManager.LoadScene("Cutscene");
            }
            if (Player.donePreQuiz == true && File.Exists(path))
            {
                SceneManager.LoadScene(13);
            }

            lastLogin = currentLogin;
            NotificationManager.CreateNotifChannel();
            NotificationManager.SendNotification();
        }
        else
        {
            Debug.Log("Login Failed!");
            loginError.SetActive(true);
        }
    }

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
        nameField.text = "player1";
        passwordField.text = "2020Alive";
    }

    [Serializable]
    public class RootObject
    {
        public AllNugQ[] sets;
    }

    [Serializable]
    public class InfoNuggetA
    {
        public string infoNuggetID;
        public string infoNuggetName;
        public string infoNuggetValue;
        public string infoNuggetDescription;
        public string gameCompetencyID;
        public string gameCompetency;
    }

    [Serializable]
    public class InfoNuggetB
    {
        public string questionID;
        public string questionName;
        public string questionValue;
        public string questionDescription;
        public string gameCompetencyID;
        public string gameCompetency;
        public string answer;
    }

    [Serializable]
    public class AllNugQ
    {
        public string gameQuestionInfoID;
        public string gameDetailID;
        public string infoNuggetID;
        public string questionID;
        public string dateModified;
        public InfoNuggetA infoNugget;
        public InfoNuggetB question;
        //public string question;
        public string gameDetail;
    }

    [Serializable]
    public class InventoryRoot
    {
        public InventoryID[] inventoryIDs;
    }

    [Serializable]
    public class InventoryID
    {
        public string userInventoryID;
        public int userInventoryValue;
        public string userID;
        public string inventoryCategoryID;
        public UserInformation user;
        public InventoryInformation inventoryCategory;
    }

    [Serializable]
    public class UserInformation
    {
        public string userID;
        public int accessLevel;
        public string username;
        public string password;
        public string salt;
        public string surname;
        public string givenName;
        public string contactNo;
        public string gender;
        public string email;
        public bool isDeleted;
        public string yearOfBirth;
    }

    [Serializable]
    public class InventoryInformation
    {
        public string inventoryCategoryID;
        public string inventoryCategoryName;
        public string inventoryCategoryDesc;
    }

    public static IEnumerator UpdateCoins()
    {
        byte[] coinData = System.Text.Encoding.UTF8.GetBytes("Player coin data");
        UnityWebRequest wwwUpdateCoin = UnityWebRequest.Put("http://103.239.222.212/ALIVE2Service/api/game/UpdateUserInven?userInventoryID=" + PlayerIDCoin + "&userInventoryValue=" + Player.coins + "&userID=" + PlayerID + "&inventoryCategoryID=324dcb99-4e2c-4282-d5a2-08d8966455ad", coinData);
        yield return wwwUpdateCoin.SendWebRequest();
        if (wwwUpdateCoin.isNetworkError || wwwUpdateCoin.isHttpError)
        {
            Debug.Log(wwwUpdateCoin.error);
        }
        else
        {
            Debug.Log("Update complete!");
            Debug.Log(wwwUpdateCoin.url);
        }
    }

    public static IEnumerator UpdateLives()
    {
        byte[] liveData = System.Text.Encoding.UTF8.GetBytes("Player live data");
        UnityWebRequest wwwUpdateLive = UnityWebRequest.Put("http://103.239.222.212/ALIVE2Service/api/game/UpdateUserInven?userInventoryID=" + PlayerIDLive + "&userInventoryValue=" + Player.lives + "&userID=" + PlayerID + "&inventoryCategoryID=4428d6b7-07e2-4d6a-a0b0-e2bf9efdaaf6", liveData);
        yield return wwwUpdateLive.SendWebRequest();
        if (wwwUpdateLive.isNetworkError || wwwUpdateLive.isHttpError)
        {
            Debug.Log(wwwUpdateLive.error);
        }
        else
        {
            Debug.Log("Update complete!");
            Debug.Log(wwwUpdateLive.url);
        }
    }    
}