using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarRating : MonoBehaviour
{
    public Sprite starSelect;
    public Sprite starUnselect;

    public GameObject starRatingMenu;
    public GameObject afterRatingMenu;

    Button[] Stars;

    int rate = 1;

    // Start is called before the first frame update
    void Start()
    {
        Stars = new Button[transform.childCount];                                    //get total number of stars based on number of buttons
    }

    // Update is called once per frame
    void Update()
    {
        ratings();
    }

    void ratings()
    {
        for (int i = 0; i < Stars.Length; i++)                                       //go through each button
        {
            Stars[i] = transform.GetChild(i).GetComponent<Button>();                 //get button

            if (i < rate)                                                            //when star is selected
            {
                Stars[i].GetComponent<Image>().sprite = starSelect;                  //set button as given image
            }
            else                                                                     //when star is not selected
            {
                Stars[i].GetComponent<Image>().sprite = starUnselect;                //set button as given image
            }
        }
    }

    public void confirmRating()
    {
        afterRatingMenu.SetActive(true);
        starRatingMenu.SetActive(false);
    }

    public void oneStar()
    {
        rate = 1;
    }

    public void twoStar()
    {
        rate = 2;
    }

    public void threeStar()
    {
        rate = 3;
    }

    public void fourStar()
    {
        rate = 4;
    }

    public void fiveStar()
    {
        rate = 5;
    }
}
