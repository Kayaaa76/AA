using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //bool lifeDeducted = false;

    public Transform hospitalSpawn;             // place where the player spawns after walking into the hospital

    public GameObject pauseBtn;                 // pause button
    public GameObject menu;                     // menu that appears after clicking on the pause button

    public GameObject gamesBtn;

    public static GameObject player;            // store the player game object
    public static Vector3 currentPosition;      // store the current position of the player
    public static int sceneCounter;             // store the sceneCounter as integer so that when player completes a mini game and comes back to the main scene, the position of the player would be the same as where the player was last at before the minigame.

    GameObject obj; //npc game object

    public GameObject dialogueM;                // get the dialogue manager game object
    public DialogueManager dm;                 // get the DialogueManager script

    List<string> inventory = new List<string>();

    public static int receptionistStage = 0;    // 0, task player go doctor office
    public static int doctorStage = 0;          // if receptionistStage = 1, go to pharmacist
    public static int pharmacistStage = 0;      // if 1, trigger Match 3 ( yes btn )

    // if match 3 complete, receptionist stage = 2
    //The receptionist tells him that there is a lot of commitments involve in wanting to be a doctor and advises the player to walk around the hub and to ask more doctors and surgeons about the matter at hand.

    public static int surgeonStage = 0;         // if receptionistStage = 3, the surgeon asks "what are you doing here little one?, next dialogue "you want to go to medical school?", "do you want to take a free course from me now?"
                                                // if player clicks ( yes btn ), trigger Tower Defense
    public static int npcdadStage = 0;
    public static int npcmalStage = 0;
    public static int npcbffStage = 0;
    public static int npcnqxStage = 0;
    public static int npctimStage = 0;
    public static int npcjunoStage = 0;
    public static int npcseanStage = 0;
    public static int npcauntyStage = 0;
    public static int npcaunty1Stage = 0;
    public static int npclawyerStage = 0;

    private DialogueTrigger dt; // get DialogueTrigger script
    Quest quest;

    public GameObject ReceptionistObject;   // get the receptionist game object
    public GameObject PharmacistObject;     // get the pharmacist game object
    public GameObject DoctorObject;         // get the doctor game object
    public GameObject SurgeonObject;        // get the surgeon game object
    public GameObject NPCDadObject;         // get the NPC_Dad game object
    public GameObject NPCMHObject;          // get the NPC_MH game object
    public GameObject NPCBFFObject;         // get the NPC_BFF game object
    public GameObject NPCNQXObject;         // get the NPC_NQX game object
    public GameObject NPCSGLObject;         // get the NPC_SGL game object
    public GameObject NPCTZDObject;         // get the NPC_TZD game object
    public GameObject NPCJKYObject;         // get the NPC_JKY game object
    public GameObject NPCAuntyObject;       // get the NPC_Aunty game object
    public GameObject NPCAunty1Object;      // get the NPC_Aunty (1) game object
    public GameObject NPCLawyerObject;      // get the NPCLawyer game object
    GameObject[] NPCs;                      //npc array
    public Camera cam;
    public GameObject NPCInteractables;
    public Button TalkToNpcPrefab; 

    public GameObject TDPanel;              // panel that allow player to start tower defense game

    public GameObject leftBtn;              // left arrow button for movement of character
    public GameObject rightBtn;             // right arrow button for movement of character
    public GameObject upBtn;                // up arrow button for movement of character
    public GameObject downBtn;              // down arrow button for movement of character

    static bool enterMinigame = false;

    public static System.DateTime DateTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player"); // find player game object
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z); // set player position to be the position of the player

        TDPanel.SetActive(false); // disable tower defense trigger panel
        menu.SetActive(false); // disable menu to show at start

        quest = gameObject.GetComponent<Quest>();

        if (Time.timeScale == 0) // if game is paused
        {
            Time.timeScale = 1; // unpause the game
        }

        if (sceneCounter == 2) // after match 3
        {
            player.transform.position = currentPosition; // set player position to be the last position the player was at before going into the match 3 scene
            sceneCounter = 0; // reset the counter to 0

            dt = ReceptionistObject.GetComponent<DialogueTrigger>();
            // win match 3 game, change the dialogue of the receptionist
            dt.dialogue.sentences[0] = "Do you want to be a doctor?";
            dt.dialogue.sentences[1] = "Walk around the hub.";
            dt.dialogue.sentences[2] = "Talk to a Surgeon to find out more.";
        }

        StartCoroutine(SetGameDetail());
        
        NPCs = GameObject.FindGameObjectsWithTag("NPC"); //get all objects with NPC tag
    }

    // Update is called once per frame
    void Update()
    {
        if(enterMinigame == true)
        {
            StartCoroutine(PostGameLevelActivity());
        }
        
        NPCInteractables.transform.position = cam.WorldToScreenPoint(new Vector3(player.transform.position.x + 3.5f, player.transform.position.y + 1)); //set container position near player object

        if (dm.spawned == true) // if dialogue box is showned
        {
            // disable movement buttons from showing on the screen 
            leftBtn.SetActive(false); 
            rightBtn.SetActive(false);
            downBtn.SetActive(false);
            upBtn.SetActive(false);

            NPCInteractables.SetActive(false); //disable npc interactables from showing
        }

        if (dm.spawned == false) // if dialogue box is not showned
        {
            // enable movement buttons to be shown on the screen 
            leftBtn.SetActive(true);
            rightBtn.SetActive(true);
            downBtn.SetActive(true);
            upBtn.SetActive(true);

            NPCInteractables.SetActive(true); //enable npc interactables from showing
        }

        if (Player.m3unlockedlevels > 1) //games menu button to appear after winning 1 level of match 3 (first game)
        {
            gamesBtn.SetActive(true);
        }

        NearNPC();
    }

    //void checkObj() // function to check if player has clicked on the npc
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
    //        if (hit.collider == true)
    //        {
    //            obj = hit.collider.gameObject;
    //        }
    //    }
    //}

    void NearNPC() //display interactable buttons for npc when near one
    {
        foreach (GameObject npc in NPCs) //cycle through npc objects
        {
            string objectName = "TalkTo" + npc.name; //create unique name for each npc
            if (Vector2.Distance(npc.transform.position, player.transform.position) < 4) //when distance between player and npc is less than 4
            {
                if (npc == ReceptionistObject && quest.receptionistQuestIcon.activeSelf == false) //when npc cannot be interacted with
                {
                    if (NPCInteractables.transform.Find(objectName)) //if interactable exists
                    {
                        Destroy(NPCInteractables.transform.Find(objectName).gameObject); //destroy npc interactable object
                    }
                    return;
                }
                if (npc == PharmacistObject && quest.pharmacistQuestIcon.activeSelf == false) //when npc cannot be interacted with
                {
                    if (NPCInteractables.transform.Find(objectName)) //if interactable exists
                    {
                        Destroy(NPCInteractables.transform.Find(objectName).gameObject); //destroy npc interactable object
                    }
                    return;
                }
                if (npc == DoctorObject && quest.doctorQuestIcon.activeSelf == false) //when npc cannot be interacted with
                {
                    if (NPCInteractables.transform.Find(objectName)) //if interactable exists
                    {
                        Destroy(NPCInteractables.transform.Find(objectName).gameObject); //destroy npc interactable object
                    }
                    return;
                }
                if (npc == SurgeonObject && quest.surgeonQuestIcon.activeSelf == false) //when npc cannot be interacted with
                {
                    if (NPCInteractables.transform.Find(objectName)) //if interactable exists
                    {
                        Destroy(NPCInteractables.transform.Find(objectName).gameObject); //destroy npc interactable object
                    }
                    return;
                }
                if (npc == NPCLawyerObject && quest.lawyerQuestIcon.activeSelf == false) //when npc cannot be interacted with
                {
                    if (NPCInteractables.transform.Find(objectName)) //if interactable exists
                    {
                        Destroy(NPCInteractables.transform.Find(objectName).gameObject); //destroy npc interactable object
                    }
                    return;
                }
                if (NPCInteractables.transform.Find(objectName) == null) //if npc interactable object does not exist
                {
                    Button interactable = Instantiate(TalkToNpcPrefab, NPCInteractables.transform.position, NPCInteractables.transform.rotation, NPCInteractables.transform); //instantiate npc interactable object
                    interactable.name = objectName; //set npc interactable object to its unique name
                    interactable.GetComponentInChildren<Text>().text = npc.GetComponent<DialogueTrigger>().dialogue.name; //chnage text of npc interactable object to wanted npc name

                    if (npc == ReceptionistObject) // if npc is Receptionist
                    {
                        interactable.onClick.AddListener(Receptionist); // add the Receptionist function
                    }
                    if (npc == PharmacistObject) // if npc is Pharmacist
                    {
                        interactable.onClick.AddListener(Pharmacist); // add the Pharmacist function
                    }
                    if (npc == DoctorObject) // if npc is Doctor
                    {
                        interactable.onClick.AddListener(Doctor); // add the Doctor function
                    }
                    if (npc == SurgeonObject) // if npc is Surgeon
                    {
                        interactable.onClick.AddListener(Surgeon); // add the Surgeon function
                    }
                    if (npc == NPCDadObject) // if npc is NPC_Dad
                    {
                        interactable.onClick.AddListener(NPCDAD); // add the NPCDAD function
                    }
                    if (npc == NPCMHObject) // if npc is NPC_MH
                    {
                        interactable.onClick.AddListener(NPCMALCOLM); // add the NPCMALCOLM function
                    }
                    if (npc == NPCBFFObject) // if npc is NPC_BFF
                    {
                        interactable.onClick.AddListener(NPCBFF); // add the NPCBFF function
                    }
                    if (npc == NPCNQXObject) // if npc is NPC_NQX
                    {
                        interactable.onClick.AddListener(NPCNQX); // add the NPCNQX function
                    }
                    if (npc == NPCSGLObject) // if the npc is NPC_SGL
                    {
                        interactable.onClick.AddListener(NPCSGL); // add the NPCSGL function
                    }
                    if (npc == NPCTZDObject) // if the npc is NPC_TZD
                    {
                        interactable.onClick.AddListener(NPCTZD);  // add the NPCTZD function
                    }
                    if (npc == NPCJKYObject)  // if the npc is NPC_JKY
                    {
                        interactable.onClick.AddListener(NPCJKY); // add the NPCJKY function
                    }
                    if (npc == NPCAuntyObject) // if the npc is NPC_Aunty
                    {
                        interactable.onClick.AddListener(NPCAUNTY); // add the NPCAUNTY function
                    }
                    if (npc == NPCAunty1Object) // if the npc is NPC_Aunty (1)
                    {
                        interactable.onClick.AddListener(NPCAUNTY1); // add the NPCAUNTY1 function
                    }
                    if (npc == NPCLawyerObject) // if the npc is NPC_Lawyer
                    {
                        interactable.onClick.AddListener(NPCLAWYER); // add the NPCLAWYER function
                    }
                }

            }
            else //when distance between player and npc is more than 4
            {
                if(NPCInteractables.transform.Find(objectName) != null) //if npc interactable object exists
                {
                    Destroy(NPCInteractables.transform.Find(objectName).gameObject); //destroy npc interactable object
                }
            }
        }
    }

    void Receptionist()
    {
        if (receptionistStage == 0) // receptionist stage 0
        {
            // dialogue to go doctor's office
            ReceptionistObject.GetComponent<DialogueTrigger>().TriggerDialogue(); // trigger the dialogue for the receptionist npc
            receptionistStage = 1; // after talking to the receptionist, the receptionist stage goes to 1
        }
    
        if (receptionistStage == 1) 
        {
            // do nothing
        }

        if (pharmacistStage == 2 && receptionistStage == 2) // if player has completed the match 3 mini game
        {
            dt.TriggerDialogue(); // trigger the dialogue for that npc after the player talks to the npc after completing the match 3 mini game
            receptionistStage = 3; // receptionist stage 3
        }
    }

    void Pharmacist()
    {
        if (receptionistStage == 1 && doctorStage == 1 && pharmacistStage == 0) // if player has talked to the receptionist and doctor at least once
        {
            if (Player.lives > 0) //when player has lives
            {
                PharmacistObject.GetComponent<DialogueTrigger>().TriggerDialogue(); // trigger the dialogue for the pharmacist to start the match 3 mini game
                pharmacistStage = 1; // pharmacist stage 1 ( player done talking to the pharmacist )
            }
            else
            {
                Debug.Log("You do not have enough lifes to play the game!");
            }
        }
        if (pharmacistStage == 1 && dm.spawned == false) // if pharmacist stage 1 and dialogue is over
        {
            currentPosition = player.transform.position; // get current position of the player
            sceneCounter = 2; // set scenecounter to 2 to change to next scene
            //test.Load(); // trigger match 3 game

            Player.dateStartM3 = System.DateTime.Now;
            enterMinigame = true;

            Player.Save();
            StartCoroutine(Login.UpdateLives());
            StartCoroutine(Login.UpdateCoins());

            SceneManager.LoadScene(14); //match 3 scene
        }
        if (pharmacistStage == 2 && receptionistStage == 2) // if player wins match 3 game
        {
            // Do nothing
        }
    }

    void Doctor()
    {
        if (doctorStage == 0 && receptionistStage == 1) // if player has talked to the receptionist before finding the doctor
        {
            DoctorObject.GetComponent<DialogueTrigger>().TriggerDialogue(); // trigger doctor dialogue to ask player to go to pharmacist
            doctorStage = 1; // doctor stage 1
        }
    }

    void Surgeon()
    {
        if (surgeonStage == 1 && dm.spawned == false)  // if surgeon dialogue showned
        {
            TDPanel.SetActive(true); // show the option to start tower defense mini game
            //obj = null; 
            currentPosition = player.transform.position; // get current position of the player
            sceneCounter = 2; // set scenecounter to 2 to change scene
        }

        if (receptionistStage == 3 && surgeonStage == 0 && dm.spawned == false) // if player has completed match 3 game and has talked to the receptionist stage 2
        {
            // trigger tower defense if player clicks yes button
            SurgeonObject.GetComponent<DialogueTrigger>().TriggerDialogue(); // trigger surgeon dialogue before showing panel to start tower defense game
            surgeonStage = 1; // surgeon stage 1
        }
    }

    public void NPCDAD() // function to show npc dad dialogue
    {
        //if (npcdadStage == 0) 
        //{
        //    NPCDadObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        //    npcdadStage = 1;
        //}

        NPCDadObject.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void NPCMALCOLM() // function to show npc malcolm dialogue
    {
        //if (npcmalStage == 0)
        //{
        //    NPCMHObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        //    npcmalStage = 1;
        //}

        NPCMHObject.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void NPCBFF() // function to show npc bff dialogue
    {
        //if (npcbffStage == 0)
        //{
        //    NPCBFFObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        //    npcbffStage = 1;
        //}

        NPCBFFObject.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void NPCNQX() // function to show npc nqx dialogue
    {
        //if (npcnqxStage == 0)
        //{
        //    NPCNQXObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        //    npcnqxStage = 1;
        //}

        NPCNQXObject.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void NPCSGL() // function to show npc sgl dialogue
    {
        //if (npcseanStage == 0)
        //{
        //    NPCSGLObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        //    npcseanStage = 1;
        //}

        NPCSGLObject.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void NPCTZD() // function to show npc tzd dialogue
    {
        //if (npctimStage == 0)
        //{
        //    NPCTZDObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        //    npctimStage = 1;
        //}

        NPCTZDObject.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void NPCJKY() // function to show npc jky dialogue
    {
        //if (npcjunoStage == 0)
        //{
        //    NPCJKYObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        //    npcjunoStage = 1;
        //}

        NPCJKYObject.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void NPCAUNTY() // function to show npc aunty dialogue
    {
        //if (npcauntyStage == 0)
        //{
        //    NPCAuntyObject.GetComponent<DialogueTrigger>().TriggerDialogue();
        //    npcauntyStage = 1;
        //}

        NPCAuntyObject.GetComponent<DialogueTrigger>().TriggerDialogue();
    }
    public void NPCAUNTY1() // function to show npc aunty dialogue
    {
        //if (npcaunty1Stage == 0)
        //{
        //    NPCAunty1Object.GetComponent<DialogueTrigger>().TriggerDialogue();
        //    npcaunty1Stage = 1;
        //}

        NPCAunty1Object.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void NPCLAWYER() // function to show npc lawyer dialogue
    {
        if (npclawyerStage == 1) // if player completed both match 3 and tower defense minigame
        {
            if (Player.lives > 0)
            {
                NPCLawyerObject.GetComponent<DialogueTrigger>().TriggerDialogue(); // trigger lawyer dialogue
                npclawyerStage = 2; // lawyer stage 2 
            }
            else
            {
                Debug.Log("You do not have enough lifes to play the game!");
            }
        }

        if (npclawyerStage == 2 && dm.spawned == false && Player.lives > 0) // if lawyer finished dialogue
        {
            //lifeDeducted = true;

            if (Player.dateStartRunner == DateTime)
            {
                Player.dateStartRunner = System.DateTime.Now;
            }
            currentPosition = player.transform.position; // get current position of player
            sceneCounter = 2; // set scenecounter to 2 to change scene

            Player.runlocked = true; //set runner game to be unlocked in data

            //Player.lives -= 1;
            //StartCoroutine(Login.UpdateLives());
            enterMinigame = true;

            Player.Save();
            StartCoroutine(Login.UpdateLives());
            StartCoroutine(Login.UpdateCoins());

            SceneManager.LoadScene(11); // trigger endless runner game
        }
    }  

    public void StartTD() // function to start tower defense game
    {
        if (Player.lives > 0)
        {
            //lifeDeducted = true;
            surgeonStage = 2;
            currentPosition = player.transform.position;
            sceneCounter = 2;

            if (Player.dateStartTD == DateTime)
            {
                Player.dateStartTD = System.DateTime.Now;
            }

            //Player.lives -= 1;
            //StartCoroutine(Login.UpdateLives());
            enterMinigame = true;

            Player.Save();
            StartCoroutine(Login.UpdateLives());
            StartCoroutine(Login.UpdateCoins());

            SceneManager.LoadScene(9); // tower defense
        }
        else
        {
            Debug.Log("You do not have enough lifes to play the game!");
        }
    }

    public void RetriggerSurgeon() // function to retrigger surgeon if player clicks no
    {
        TDPanel.SetActive(false);
        surgeonStage = 0;
    }

    public void PauseGame() // functio to pause the game if player clicks on the pause button
    {
        Time.timeScale = 0;
        pauseBtn.SetActive(false);
        menu.SetActive(true);
    }

    public void UnpauseGame() // function to unpause the game if player clicks on the resume button
    {
        Time.timeScale = 1;
        pauseBtn.SetActive(true);
        menu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit(); // quit the game instantly
    }

    //IEnumerator PostLifeActivity()
    //{
    //    if (lifeDeducted == true)
    //    {
    //        WWWForm formPostLifeActivity = new WWWForm();
    //        WWW wwwPostLifeActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Lifes&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Player Lifes", formPostLifeActivity);
    //        yield return wwwPostLifeActivity;
    //        Debug.Log(wwwPostLifeActivity.text);
    //        Debug.Log(wwwPostLifeActivity.error);
    //        Debug.Log(wwwPostLifeActivity.url);
    //        lifeDeducted = false;

    //        WWWForm formPostGameLevelActivity = new WWWForm();
    //        WWW wwwPostGameLevelActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Game Level&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Game Level", formPostGameLevelActivity);
    //        yield return wwwPostGameLevelActivity;
    //        Debug.Log(wwwPostGameLevelActivity.text);
    //        Debug.Log(wwwPostGameLevelActivity.error);
    //        Debug.Log(wwwPostGameLevelActivity.url);
    //    }
    //}

    IEnumerator PostGameLevelActivity()
    {
        enterMinigame = false;

        WWWForm formPostGameLevelActivity = new WWWForm();
        WWW wwwPostGameLevelActivity = new WWW("http://www.stewards.com.sg/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Game Level&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Game Level", formPostGameLevelActivity);
        yield return wwwPostGameLevelActivity;

        //WWW wwwPostGameLevelActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Game Level&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Game Level", formPostGameLevelActivity);
        //Debug.Log(wwwPostGameLevelActivity.text);
        //Debug.Log(wwwPostGameLevelActivity.error);
        //Debug.Log(wwwPostGameLevelActivity.url);
    }

    [Serializable]
    public class GameDetailRoot
    {
        public GameDetail[] gameDetails;
    }

    [Serializable]
    public class GameDetail
    {
        public string gameDetailID;
        public string gameDetailName;
        public string gameDetailValue;
        public string gameDetailDescription;
    }

   public IEnumerator SetGameDetail()
    {
        //WWW wwwGetGameDetail = new WWW("http://103.239.222.212/ALIVE2Service/api/game/AllGameDetail");
        WWW wwwGetGameDetail = new WWW("http://www.stewards.com.sg/ALIVE2Service/api/game/AllGameDetail");
        yield return wwwGetGameDetail;
        Debug.Log(wwwGetGameDetail.text);
        Debug.Log(wwwGetGameDetail.error);
        Debug.Log(wwwGetGameDetail.url);

        File.WriteAllText(Application.persistentDataPath + "/AllGameDetail.json", wwwGetGameDetail.text);
        Debug.Log("AllGameDetail written");

        string jsonString = File.ReadAllText(Application.persistentDataPath + "/AllGameDetail.json");

        GameDetailRoot gameDetailRoot = new GameDetailRoot();

        gameDetailRoot = JsonUtility.FromJson<GameDetailRoot>("{\"gameDetails\":" + jsonString + "}");

        string fileName = "";
        foreach(GameDetail gameDetail in gameDetailRoot.gameDetails)
        {
            Debug.Log(gameDetail.gameDetailName);
            fileName = gameDetail.gameDetailName;
            GameDetail detail = new GameDetail();
            detail.gameDetailID = gameDetail.gameDetailID;
            detail.gameDetailName = gameDetail.gameDetailName;
            detail.gameDetailValue = gameDetail.gameDetailValue;
            detail.gameDetailDescription = gameDetail.gameDetailDescription;
            string gameDetailJson = JsonUtility.ToJson(detail);
            File.WriteAllText(Application.persistentDataPath + "/" + fileName + ".json", gameDetailJson);
        }
    }
}
