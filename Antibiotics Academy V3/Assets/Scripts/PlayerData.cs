using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int coin;

    public int receptionistStage;
    public int doctorStage;
    public int pharmacistStage;
    public int surgeonStage;
    public int npcdadStage;
    public int npcmalStage;
    public int npcbffStage;
    public int npcnqxStage;
    public int npctimStage;
    public int npcjunoStage;
    public int npcseanStage;
    public int npcauntyStage;
    public int npclawyerStage;

    public System.DateTime lastLogin;

    public int life;

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

        lastLogin = player.tLastLogin;

        life = player.life;
    }
}
