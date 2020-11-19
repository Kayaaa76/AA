using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

    public static void TotalCoins()
    {
        Debug.Log("Player has: " + coins + " coins!");
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        coins = data.coin;

        GameManager.receptionistStage = data.receptionistStage;
        GameManager.doctorStage = data.doctorStage;
        GameManager.pharmacistStage = data.pharmacistStage;
        GameManager.surgeonStage = data.surgeonStage;
        GameManager.npcdadStage = data.npcdadStage;
        GameManager.npcmalStage = data.npcmalStage;
        GameManager.npcbffStage = data.npcbffStage;
        GameManager.npcnqxStage = data.npcnqxStage;
        GameManager.npctimStage = data.npctimStage;
        GameManager.npcjunoStage = data.npcjunoStage;
        GameManager.npcseanStage = data.npcseanStage;
        GameManager.npcauntyStage = data.npcauntyStage;
        GameManager.npclawyerStage = data.npclawyerStage;
    }
}
