using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Player : MonoBehaviour
{
    public static int coins;
    public int coin;

    public static int treceptionistStage = 0;
    public static int tdoctorStage = 0;
    public static int tpharmacistStage = 0;
    public static int tsurgeonStage = 0;
    public static int tnpcdadStage = 0;
    public static int tnpcmalStage = 0;
    public static int tnpcbffStage = 0;
    public static int tnpcnqxStage = 0;
    public static int tnpctimStage = 0;
    public static int tnpcjunoStage = 0;
    public static int tnpcseanStage = 0;
    public static int tnpcauntyStage = 0;
    public static int tnpclawyerStage = 0;

    public static int tm3unlockedlevels;
    public static int m3unlockedlevels;

    public static int ttdunlockedlevels;
    public static int tdunlockedlevels;

    public static bool trunlocked;
    public static bool runlocked;

    public static System.DateTime tLastLogin;

    public static int lives;
    public int life;

    public static bool donePreQuiz = false;
    public static bool donePostQuiz = false;

    public static bool spunToday = true;

    public GameObject SpinMenu;

    public static int preGameQuizScore;
    public static int postGameQuizScore;
    //public static System.DateTime preGameQuizTime;
    //public static System.DateTime postGameQuizTime;
    public static string preGameQuizTime;
    public static string postGameQuizTime;

    public static System.DateTime dateStartM3;
    public static System.DateTime dateEndM3;
    public static string m3Duration;
    public static System.DateTime dateStartTD;
    public static System.DateTime dateEndTD;
    public static string TDDuration;
    public static System.DateTime dateStartRunner;
    public static System.DateTime dateEndRunner;
    public static string RunnerDuration;
    public static System.DateTime dateStart;
    public static System.DateTime dateEnd;
    public static string totalDuration;

    void Start()
    {
        if(spunToday == false)
        {
            SpinMenu.SetActive(true);
            spunToday = true;
        }
        //else if(spunToday == true)
        //{
        //    SpinMenu.SetActive(false);
        //}
        //else
        //{
        //    SpinMenu.SetActive(false);
        //}
    }

    void Update()
    {
        coin = coins;

        treceptionistStage = GameManager.receptionistStage;
        tdoctorStage = GameManager.doctorStage;
        tpharmacistStage = GameManager.pharmacistStage;
        tsurgeonStage = GameManager.surgeonStage;
        tnpcdadStage = GameManager.npcdadStage;
        tnpcmalStage = GameManager.npcmalStage;
        tnpcbffStage = GameManager.npcbffStage;
        tnpcnqxStage = GameManager.npcnqxStage;
        tnpctimStage = GameManager.npctimStage;
        tnpcjunoStage = GameManager.npcjunoStage;
        tnpcseanStage = GameManager.npcseanStage;
        tnpcauntyStage = GameManager.npcauntyStage;
        tnpclawyerStage = GameManager.npclawyerStage;

        tLastLogin = Login.lastLogin;

        life = lives;

        if(lives > 5)
        {
            lives = 5;
        }

        tm3unlockedlevels = m3unlockedlevels;

        ttdunlockedlevels = tdunlockedlevels;

        trunlocked = runlocked;

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    Save(); 
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Load();
        //}
    }

    public static void TotalCoins()
    {
        Debug.Log("Player has: " + coins + " coins!");
    }

    public static void TotalLives()
    {
        Debug.Log("Player has: " + lives + " lives!");
    }

    public static void Save()
    {
        JSONObject playerJson = new JSONObject();
        playerJson.Add("Coins", coins);
        playerJson.Add("Lives", lives);
        playerJson.Add("Receptionist Stage", treceptionistStage);
        playerJson.Add("Doctor Stage", tdoctorStage);
        playerJson.Add("Pharmacist Stage", tpharmacistStage);
        playerJson.Add("Surgeon Stage", tsurgeonStage);
        playerJson.Add("NPC Dad Stage", tnpcdadStage);
        playerJson.Add("NPC Mal Stage", tnpcmalStage);
        playerJson.Add("NPC Bff Stage", tnpcbffStage);
        playerJson.Add("NPC NQX Stage", tnpcnqxStage);
        playerJson.Add("NPC TIM Stage", tnpctimStage);
        playerJson.Add("NPC JUNO Stage", tnpcjunoStage);
        playerJson.Add("NPC SEAN Stage", tnpcseanStage);
        playerJson.Add("NPC Aunty Stage", tnpcauntyStage);
        playerJson.Add("NPC Lawyer Stage", tnpclawyerStage);
        playerJson.Add("Last Login", tLastLogin.ToString());
        playerJson.Add("Done Pre Game Quiz", donePreQuiz);
        playerJson.Add("Done Post Game Quiz", donePostQuiz);
        playerJson.Add("Spun Reward Wheel Today?", spunToday);
        playerJson.Add("Match 3 Levels Unlocked", tm3unlockedlevels);
        playerJson.Add("Tower Defense Levels Unlocked", ttdunlockedlevels);
        playerJson.Add("Runner Unlocked", trunlocked);
        playerJson.Add("Pre Game Quiz Score", preGameQuizScore);
        playerJson.Add("Pre Game Quiz Time Taken", preGameQuizTime);
        playerJson.Add("Post Game Quiz Score", postGameQuizScore);
        playerJson.Add("Post Game Quiz Time Taken", postGameQuizTime);
        playerJson.Add("Date Started", dateStart.ToString());
        playerJson.Add("Date Ended", dateEnd.ToString());
        playerJson.Add("Total Duration", totalDuration);
        playerJson.Add("Date Started M3", dateStartM3.ToString());
        playerJson.Add("Date Ended M3", dateEndM3.ToString());
        playerJson.Add("M3 Duration", m3Duration);
        playerJson.Add("Date Started TD", dateStartTD.ToString());
        playerJson.Add("Date Ended TD", dateEndTD.ToString());
        playerJson.Add("TD Duration", TDDuration);
        playerJson.Add("Date Started Runner", dateStartRunner.ToString());
        playerJson.Add("Date Ended Runner", dateEndRunner.ToString());
        playerJson.Add("Runner Duration", RunnerDuration);

        //Debug.Log(playerJson.ToString());
        string path = Application.persistentDataPath + "/PlayerSave.json";
        File.WriteAllText(path, playerJson.ToString());
    }

    public void CallSavePlayer()
    {
        Save();
    }

    public static void Load()
    {
        string path = Application.persistentDataPath + "/PlayerSave.json";
        string jsonString = File.ReadAllText(path);
        JSONObject playerJson = (JSONObject)JSON.Parse(jsonString);

        coins = playerJson["Coins"];
        lives = playerJson["Lives"];
        GameManager.receptionistStage = playerJson["Receptionist Stage"];
        GameManager.doctorStage = playerJson["Doctor Stage"];
        GameManager.pharmacistStage = playerJson["Pharmacist Stage"];
        GameManager.surgeonStage = playerJson["Surgeon Stage"];
        GameManager.npcdadStage = playerJson["NPC Dad Stage"];
        GameManager.npcmalStage = playerJson["NPC Mal Stage"];
        GameManager.npcbffStage = playerJson["NPC Bff Stage"];
        GameManager.npcnqxStage = playerJson["NPC NQX Stage"];
        GameManager.npctimStage = playerJson["NPC TIM Stage"];
        GameManager.npcjunoStage = playerJson["NPC JUNO Stage"];
        GameManager.npcseanStage = playerJson["NPC SEAN Stage"];
        GameManager.npcauntyStage = playerJson["NPC Aunty Stage"];
        GameManager.npclawyerStage = playerJson["NPC Lawyer Stage"];
        Login.lastLogin = System.DateTime.Parse(playerJson["Last Login"]);
        donePreQuiz = playerJson["Done Pre Game Quiz"];
        donePostQuiz = playerJson["Done Post Game Quiz"];
        spunToday = playerJson["Spun Reward Wheel Today?"];
        m3unlockedlevels = playerJson["Match 3 Levels Unlocked"];
        tdunlockedlevels = playerJson["Tower Defense Levels Unlocked"];
        runlocked = playerJson["Runner Unlocked"];
        preGameQuizScore = playerJson["Pre Game Quiz Score"];
        preGameQuizTime = playerJson["Pre Game Quiz Time Taken"];
        postGameQuizScore = playerJson["Post Game Quiz Score"];
        postGameQuizTime = playerJson["Post Game Quiz Time Taken"];
        dateStart = System.DateTime.Parse(playerJson["Date Started"]);
        dateEnd = System.DateTime.Parse(playerJson["Date Ended"]);
        totalDuration = playerJson["Total Duration"];
        dateStartM3 = System.DateTime.Parse(playerJson["Date Started M3"]);
        dateEndM3 = System.DateTime.Parse(playerJson["Date Ended M3"]);
        m3Duration = playerJson["M3 Duration"];
        dateStartTD = System.DateTime.Parse(playerJson["Date Started TD"]);
        dateEndTD = System.DateTime.Parse(playerJson["Date Ended TD"]);
        TDDuration = playerJson["TD Duration"];
        dateStartRunner = System.DateTime.Parse(playerJson["Date Started Runner"]);
        dateEndRunner = System.DateTime.Parse(playerJson["Date Ended Runner"]);
        RunnerDuration = playerJson["Runner Duration"];

        Debug.Log(playerJson);
    }

    //public void SavePlayer()
    //{
    //    SaveSystem.SavePlayer(this);
    //}

    public void CallLoadPlayer()
    {
        Load();
    }

    //public static void LoadPlayer()
    //{
    //    PlayerData data = SaveSystem.LoadPlayer();

    //    coins = data.coin;

    //    GameManager.receptionistStage = data.receptionistStage;
    //    GameManager.doctorStage = data.doctorStage;
    //    GameManager.pharmacistStage = data.pharmacistStage;
    //    GameManager.surgeonStage = data.surgeonStage;
    //    GameManager.npcdadStage = data.npcdadStage;
    //    GameManager.npcmalStage = data.npcmalStage;
    //    GameManager.npcbffStage = data.npcbffStage;
    //    GameManager.npcnqxStage = data.npcnqxStage;
    //    GameManager.npctimStage = data.npctimStage;
    //    GameManager.npcjunoStage = data.npcjunoStage;
    //    GameManager.npcseanStage = data.npcseanStage;
    //    GameManager.npcauntyStage = data.npcauntyStage;
    //    GameManager.npclawyerStage = data.npclawyerStage;

    //    Login.lastLogin = data.lastLogin;

    //    lives = data.life;

    //    Debug.Log("player data have been loaded");
    //}
}
