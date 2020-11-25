using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Player : MonoBehaviour
{
    public static int coins;
    public int coin;

    public int treceptionistStage = 0;
    public int tdoctorStage = 0;
    public int tpharmacistStage = 0;
    public int tsurgeonStage = 0;
    public int tnpcdadStage = 0;
    public int tnpcmalStage = 0;
    public int tnpcbffStage = 0;
    public int tnpcnqxStage = 0;
    public int tnpctimStage = 0;
    public int tnpcjunoStage = 0;
    public int tnpcseanStage = 0;
    public int tnpcauntyStage = 0;
    public int tnpclawyerStage = 0;

    public System.DateTime tLastLogin;

    public static int lives;
    public int life;

    public static bool donePreQuiz = false;
    public static bool donePostQuiz = false;

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

    public void Save()
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

        //Debug.Log(playerJson.ToString());
        string path = Application.persistentDataPath + "/PlayerSave.json";
        File.WriteAllText(path, playerJson.ToString());
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
