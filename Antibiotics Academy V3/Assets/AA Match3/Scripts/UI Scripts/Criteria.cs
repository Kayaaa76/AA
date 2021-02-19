using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    [System.Serializable]
    public class Levels                                //wave class that defines the wave properties
    {
        public TilePrefabs[] tilePrefabs;        //list of enemy prefabs
        public int limitMoves;
    }

    [System.Serializable]
    public class TilePrefabs                           //wave class that defines the wave properties
    {
        public Sprite tile;
        public int count;
    }

    public class Criteria : MonoBehaviour
    {
        public int moveCounter;
        public Text movesText;
        public GameObject tilesNeeded;
        public GameObject tilesNeededSet;

        public Levels[] levels;

        DisplayEndUI display;

        public int currentLevel;

        // Start is called before the first frame update
        void Start()
        {
            display = FindObjectOfType<DisplayEndUI>();

            DisplayCritera();
        }

        // Update is called once per frame
        void Update()
        {
            if (levels[currentLevel].limitMoves == 0) //infinite moves if no limit is set
            {
                movesText.text = "\u221E"; //set text to infinite symbol
            }
            else //when limit is set
            {
                movesText.text = moveCounter.ToString(); //set text to move counter

                if(moveCounter < 1) //trigger game lost if out of moves
                {
                    display.DisplayDeathUI();
                }
            }
        }

        void DisplayCritera()
        {
            for (int i = 0; i < levels[currentLevel].tilePrefabs.Length; i++)
            {
                Instantiate(tilesNeededSet, tilesNeeded.transform.position, tilesNeeded.transform.rotation, tilesNeeded.transform);

                tilesNeededSet.GetComponentInChildren<Image>().sprite = levels[currentLevel].tilePrefabs[i].tile;
                tilesNeededSet.GetComponentInChildren<Text>().text = "x " + levels[currentLevel].tilePrefabs[i].count;
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(tilesNeeded.GetComponent<RectTransform>());
        }
    }
}
