using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public GameObject receptionistQuestIcon;
    public GameObject doctorQuestIcon;
    public GameObject pharmacistQuestIcon;
    public GameObject surgeonQuestIcon;
    public GameObject lawyerQuestIcon;

    float rIconPos;
    float dIconPos;
    float pIconPos;
    float sIconPos;
    float lIconPos;

    public GameObject questIcon;
    float iconPosition;

    public float hoverDistance;
    public float hoverSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        hoverDistance = 0.2f;
        hoverSpeed = 0.4f;

        rIconPos = receptionistQuestIcon.transform.position.y;
        dIconPos = doctorQuestIcon.transform.position.y;
        pIconPos = pharmacistQuestIcon.transform.position.y;
        sIconPos = surgeonQuestIcon.transform.position.y;
        lIconPos = lawyerQuestIcon.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        questIndicator();
    }

    void questIndicator()
    {
        if (GameManager.receptionistStage == 0 || GameManager.pharmacistStage == 2 && GameManager.receptionistStage == 2) //when players need to talk to the receptionist
        {
            questIcon = receptionistQuestIcon;
            iconPosition = rIconPos;
        }
        else if (GameManager.receptionistStage == 1 && GameManager.doctorStage == 0) //when players need to talk to the doctor
        {
            questIcon = doctorQuestIcon;
            iconPosition = dIconPos;
        }
        else if (GameManager.doctorStage == 1 && (GameManager.pharmacistStage == 0 || GameManager.pharmacistStage == 1)) //when players need to talk to the pharmacist
        {
            questIcon = pharmacistQuestIcon;
            iconPosition = pIconPos;
        }
        else if (GameManager.receptionistStage == 3 && (GameManager.surgeonStage == 0 || GameManager.surgeonStage == 1)) //when players need to talk to the surgeon
        {
            questIcon = surgeonQuestIcon;
            iconPosition = sIconPos;
        }
        else if (GameManager.npclawyerStage == 1) //when players need to talk to the lawyer
        {
            questIcon = lawyerQuestIcon;
            iconPosition = lIconPos;
        }

        //disable other quest icons if they are not the current quest
        if (questIcon != receptionistQuestIcon)
        {
            receptionistQuestIcon.SetActive(false);
        }
        if (questIcon != doctorQuestIcon)
        {
            doctorQuestIcon.SetActive(false);
        }
        if (questIcon != pharmacistQuestIcon)
        {
            pharmacistQuestIcon.SetActive(false);
        }
        if (questIcon != surgeonQuestIcon)
        {
            surgeonQuestIcon.SetActive(false);
        }
        if (questIcon != lawyerQuestIcon)
        {
            lawyerQuestIcon.SetActive(false);
        }

        questIcon.SetActive(true); //enable current quest icon
        questIcon.transform.position = new Vector3(questIcon.transform.position.x, Mathf.PingPong(Time.time * hoverSpeed, hoverDistance) + iconPosition, questIcon.transform.position.z); //oscillate quest icon
    }
}
