using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue; // reference Dialogue script
    
    public void TriggerDialogue() // function to trigger the dialogue
    {
        if(gameObject.name == "NPC_Dad")
        {
            //Debug.Log("Ping");

            //dialogue.sentences[0] = "Test sentence 1";
            //dialogue.sentences[1] = "Also Test sentence but 2";

            //dialogue.sentences = new string[4];      

            string parkNPC1String = File.ReadAllText(Application.persistentDataPath + "/Park NPC 1.json");
            JSONObject parkNPC1Json = (JSONObject)JSON.Parse(parkNPC1String);

            dialogue.sentences[3] = parkNPC1Json["gameDetailDescription"];
        }
        else if (gameObject.name == "NPC_Aunty (1)")
        {
            string parkNPC2String = File.ReadAllText(Application.persistentDataPath + "/Park NPC 2.json");
            JSONObject parkNPC2Json = (JSONObject)JSON.Parse(parkNPC2String);

            dialogue.sentences[2] = parkNPC2Json["gameDetailDescription"];
        }
        else if (gameObject.name == "NPC_Aunty")
        {
            string supermarketNPC1String = File.ReadAllText(Application.persistentDataPath + "/Supermarket NPC 1.json");
            JSONObject  supermarketNPC1Json = (JSONObject)JSON.Parse(supermarketNPC1String);

            dialogue.sentences[2] = supermarketNPC1Json["gameDetailDescription"];
        }
        else if (gameObject.name == "NPC_BFF")
        {
            string supermarketNPC2String = File.ReadAllText(Application.persistentDataPath + "/Supermarket NPC 2.json");
            JSONObject supermarketNPC2Json = (JSONObject)JSON.Parse(supermarketNPC2String);

            dialogue.sentences[3] = supermarketNPC2Json["gameDetailDescription"];
        }

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue); // find DialogueManager component and trigger the StartDialogue function in the Dialogue script
    }
}
