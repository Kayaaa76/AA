using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coin;

    public int receptionistStage = 0;
    public int doctorStage = 0;
    public int pharmacistStage = 0;
    public int surgeonStage = 0;
    public int npcdadStage = 0;
    public int npcmalStage = 0;
    public int npcbffStage = 0;
    public int npcnqxStage = 0;
    public int npctimStage = 0;
    public int npcjunoStage = 0;
    public int npcseanStage = 0;
    public int npcauntyStage = 0;
    public int npclawyerStage = 0;

    public PlayerData(Player player)
    {
        coin = player.coin;

        receptionistStage = player.treceptionistStage;
        doctorStage = player.tdoctorStage;
        pharmacistStage = player.tpharmacistStage;
        surgeonStage = player.tsurgeonStage;
        npcdadStage = player.tnpcdadStage;
        npcmalStage = player.tnpcmalStage;
        npcbffStage = player.tnpcbffStage;
        npcnqxStage = player.tnpcnqxStage;
        npctimStage = player.tnpctimStage;
        npcjunoStage = player.tnpcjunoStage;
        npcseanStage = player.tnpcseanStage;
        npcauntyStage = player.tnpcauntyStage;
        npclawyerStage = player.tnpclawyerStage;
}
}
