using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeChange : MonoBehaviour
{
    GameObject[] bed1;
    public Sprite pastelBed1;
    public Sprite classicBed;
    public Sprite boldBed;

    GameObject[] bed2;
    public Sprite pastelBed2;

    GameObject[] chair1;
    public Sprite pastelChair1;
    public Sprite boldChair1;

    GameObject[] chair2;
    public Sprite pastelChair2;
    public Sprite boldChair2;

    GameObject[] cupboardFront;
    public Sprite pastelCupboardFront;
    public Sprite boldCupboardFront;

    GameObject[] cupboardSide;
    public Sprite pastelCupboardSide;
    public Sprite boldCupboardSide;

    GameObject[] deskDoctor;
    public Sprite pastelDeskDoctor;
    public Sprite classicDeskDoctor;
    public Sprite boldDeskDoctor;

    GameObject[] deskMini;
    public Sprite pastelDeskMini;
    public Sprite classicDeskMini;
    public Sprite boldDeskMini;

    GameObject[] deskPharmacist;
    public Sprite pastelDeskPharmacist;
    public Sprite classicDeskPharmacist;
    public Sprite boldDeskPharmacist;

    GameObject[] deskReceptionist;
    public Sprite pastelDeskReceptionist;
    public Sprite classicDeskReceptionist;
    public Sprite boldDeskReceptionist;

    GameObject[] door;
    public Sprite pastelDoor;
    public Sprite classicDoor;
    public Sprite boldDoor;

    GameObject[] floormat;
    public Sprite pastelFloorMat;
    public Sprite boldFloorMat;

    GameObject[] plant1;
    public Sprite pastelPlant1;
    public Sprite boldPlant1;

    GameObject[] plant2;
    public Sprite pastelPlant2;
    public Sprite boldPlant2;

    GameObject[] stairs;
    public Sprite pastelStairs;
    public Sprite classicStairs;
    public Sprite boldStairs;

    GameObject[] ticketMachine;
    public Sprite pastelTicketMachine;
    public Sprite boldTicketMachine;

    // Start is called before the first frame update
    void Start()
    {
        bed1 = GameObject.FindGameObjectsWithTag("envBed1");
        bed2 = GameObject.FindGameObjectsWithTag("envBed2");
        chair1 = GameObject.FindGameObjectsWithTag("envChair1");
        chair2 = GameObject.FindGameObjectsWithTag("envChair2");
        cupboardFront = GameObject.FindGameObjectsWithTag("envCupboardFront");
        cupboardSide = GameObject.FindGameObjectsWithTag("envCupboardSide");
        deskDoctor = GameObject.FindGameObjectsWithTag("envDeskDoctor");
        deskMini = GameObject.FindGameObjectsWithTag("envDeskMini");
        deskPharmacist = GameObject.FindGameObjectsWithTag("envDeskPharmacist");
        deskReceptionist = GameObject.FindGameObjectsWithTag("envDeskReceptionist");
        door = GameObject.FindGameObjectsWithTag("envDoor");
        floormat = GameObject.FindGameObjectsWithTag("envFloormat");
        plant1 = GameObject.FindGameObjectsWithTag("envPlant1");
        plant2 = GameObject.FindGameObjectsWithTag("envPlant2");
        stairs = GameObject.FindGameObjectsWithTag("envStairs");
        ticketMachine = GameObject.FindGameObjectsWithTag("envTicketMachine");

        changeEnvironment();
    }

    public void changeEnvironment()
    {
        if(PlayerPrefs.GetString("Theme") == "Pastel")
        {
            for (int i = 0; i < bed1.Length; i++)
            {
                bed1[i].GetComponent<SpriteRenderer>().sprite = pastelBed1;
            }
            for (int i = 0; i < bed2.Length; i++)
            {
                bed2[i].GetComponent<SpriteRenderer>().sprite = pastelBed2;
            }
            for (int i = 0; i < chair1.Length; i++)
            {
                chair1[i].GetComponent<SpriteRenderer>().sprite = pastelChair1;
            }
            for (int i = 0; i < chair2.Length; i++)
            {
                chair2[i].GetComponent<SpriteRenderer>().sprite = pastelChair2;
            }
            for (int i = 0; i < cupboardFront.Length; i++)
            {
                cupboardFront[i].GetComponent<SpriteRenderer>().sprite = pastelCupboardFront;
            }
            for (int i = 0; i < cupboardSide.Length; i++)
            {
                cupboardSide[i].GetComponent<SpriteRenderer>().sprite = pastelCupboardSide;
            }
            for (int i = 0; i < deskDoctor.Length; i++)
            {
                deskDoctor[i].GetComponent<SpriteRenderer>().sprite = pastelDeskDoctor;
            }
            for (int i = 0; i < deskMini.Length; i++)
            {
                deskMini[i].GetComponent<SpriteRenderer>().sprite = pastelDeskMini;
            }
            for (int i = 0; i < deskPharmacist.Length; i++)
            {
                deskPharmacist[i].GetComponent<SpriteRenderer>().sprite = pastelDeskPharmacist;
            }
            for (int i = 0; i < deskReceptionist.Length; i++)
            {
                deskReceptionist[i].GetComponent<SpriteRenderer>().sprite = pastelDeskReceptionist;
            }
            for (int i = 0; i < door.Length; i++)
            {
                door[i].GetComponent<SpriteRenderer>().sprite = pastelDoor;
            }
            for (int i = 0; i < floormat.Length; i++)
            {
                floormat[i].GetComponent<SpriteRenderer>().sprite = pastelFloorMat;
            }
            for (int i = 0; i < plant1.Length; i++)
            {
                plant1[i].GetComponent<SpriteRenderer>().sprite = pastelPlant1;
            }
            for (int i = 0; i < plant2.Length; i++)
            {
                plant2[i].GetComponent<SpriteRenderer>().sprite = pastelPlant1;
            }
            for (int i = 0; i < stairs.Length; i++)
            {
                stairs[i].GetComponent<SpriteRenderer>().sprite = pastelStairs;
            }
            for (int i = 0; i < ticketMachine.Length; i++)
            {
                ticketMachine[i].GetComponent<SpriteRenderer>().sprite = pastelTicketMachine;
            }
        }

        else if (PlayerPrefs.GetString("Theme") == "Classic")
        {
            for (int i = 0; i < bed1.Length; i++)
            {
                bed1[i].GetComponent<SpriteRenderer>().sprite = classicBed;
            }
            for (int i = 0; i < bed2.Length; i++)
            {
                bed2[i].GetComponent<SpriteRenderer>().sprite = classicBed;
            }
            for (int i = 0; i < chair1.Length; i++)
            {
                chair1[i].GetComponent<SpriteRenderer>().sprite = pastelChair1;
            }
            for (int i = 0; i < chair2.Length; i++)
            {
                chair2[i].GetComponent<SpriteRenderer>().sprite = pastelChair1;
            }
            for (int i = 0; i < cupboardFront.Length; i++)
            {
                cupboardFront[i].GetComponent<SpriteRenderer>().sprite = pastelCupboardFront;
            }
            for (int i = 0; i < cupboardSide.Length; i++)
            {
                cupboardSide[i].GetComponent<SpriteRenderer>().sprite = pastelCupboardSide;
            }
            for (int i = 0; i < deskDoctor.Length; i++)
            {
                deskDoctor[i].GetComponent<SpriteRenderer>().sprite = classicDeskDoctor;
            }
            for (int i = 0; i < deskMini.Length; i++)
            {
                deskMini[i].GetComponent<SpriteRenderer>().sprite = classicDeskMini;
            }
            for (int i = 0; i < deskPharmacist.Length; i++)
            {
                deskPharmacist[i].GetComponent<SpriteRenderer>().sprite = classicDeskPharmacist;
            }
            for (int i = 0; i < deskReceptionist.Length; i++)
            {
                deskReceptionist[i].GetComponent<SpriteRenderer>().sprite = classicDeskReceptionist;
            }
            for (int i = 0; i < door.Length; i++)
            {
                door[i].GetComponent<SpriteRenderer>().sprite = classicDoor;
            }
            for (int i = 0; i < floormat.Length; i++)
            {
                floormat[i].GetComponent<SpriteRenderer>().sprite = pastelFloorMat;
            }
            for (int i = 0; i < plant1.Length; i++)
            {
                plant1[i].GetComponent<SpriteRenderer>().sprite = pastelPlant1;
            }
            for (int i = 0; i < plant2.Length; i++)
            {
                plant2[i].GetComponent<SpriteRenderer>().sprite = pastelPlant2;
            }
            for (int i = 0; i < stairs.Length; i++)
            {
                stairs[i].GetComponent<SpriteRenderer>().sprite = classicStairs;
            }
            for (int i = 0; i < ticketMachine.Length; i++)
            {
                ticketMachine[i].GetComponent<SpriteRenderer>().sprite = pastelTicketMachine;
            }
        }

        else if (PlayerPrefs.GetString("Theme") == "Bold")
        {
            for (int i = 0; i < bed1.Length; i++)
            {
                bed1[i].GetComponent<SpriteRenderer>().sprite = boldBed;
            }
            for (int i = 0; i < bed2.Length; i++)
            {
                bed2[i].GetComponent<SpriteRenderer>().sprite = boldBed;
            }
            for (int i = 0; i < chair1.Length; i++)
            {
                chair1[i].GetComponent<SpriteRenderer>().sprite = boldChair1;
            }
            for (int i = 0; i < chair2.Length; i++)
            {
                chair2[i].GetComponent<SpriteRenderer>().sprite = boldChair2;
            }
            for (int i = 0; i < cupboardFront.Length; i++)
            {
                cupboardFront[i].GetComponent<SpriteRenderer>().sprite = boldCupboardFront;
            }
            for (int i = 0; i < cupboardSide.Length; i++)
            {
                cupboardSide[i].GetComponent<SpriteRenderer>().sprite = boldCupboardSide;
            }
            for (int i = 0; i < deskDoctor.Length; i++)
            {
                deskDoctor[i].GetComponent<SpriteRenderer>().sprite = boldDeskDoctor;
            }
            for (int i = 0; i < deskMini.Length; i++)
            {
                deskMini[i].GetComponent<SpriteRenderer>().sprite = boldDeskMini;
            }
            for (int i = 0; i < deskPharmacist.Length; i++)
            {
                deskPharmacist[i].GetComponent<SpriteRenderer>().sprite = boldDeskPharmacist;
            }
            for (int i = 0; i < deskReceptionist.Length; i++)
            {
                deskReceptionist[i].GetComponent<SpriteRenderer>().sprite = boldDeskReceptionist;
            }
            for (int i = 0; i < door.Length; i++)
            {
                door[i].GetComponent<SpriteRenderer>().sprite = boldDoor;
            }
            for (int i = 0; i < floormat.Length; i++)
            {
                floormat[i].GetComponent<SpriteRenderer>().sprite = boldFloorMat;
            }
            for (int i = 0; i < plant1.Length; i++)
            {
                plant1[i].GetComponent<SpriteRenderer>().sprite = boldPlant1;
            }
            for (int i = 0; i < plant2.Length; i++)
            {
                plant2[i].GetComponent<SpriteRenderer>().sprite = boldPlant2;
            }
            for (int i = 0; i < stairs.Length; i++)
            {
                stairs[i].GetComponent<SpriteRenderer>().sprite = boldStairs;
            }
            for (int i = 0; i < ticketMachine.Length; i++)
            {
                ticketMachine[i].GetComponent<SpriteRenderer>().sprite = boldTicketMachine;
            }
        }
    }
}
