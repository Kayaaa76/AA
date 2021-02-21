using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public enum Tile
    {
        sleeping,
        fruit,
        vegetable,
        running,
        water
    }

    [System.Serializable]
    public class Levels                                //wave class that defines the wave properties
    {
        public TilePrefabs[] tilePrefabs;        //list of enemy prefabs
        public int limitMoves;
    }

    [System.Serializable]
    public class TilePrefabs                           //wave class that defines the wave properties
    {
        public Tile tile;
        public int count;
    }

    public class Criteria : MonoBehaviour
    {
        public int moveCounter;
        public Text movesText;
        public GameObject tilesNeeded;
        public GameObject tilesNeededSet;

        public Sprite sleepingTile;
        public Sprite fruitTile;
        public Sprite vegetableTile;
        public Sprite runningTile;
        public Sprite waterTile;

        //public int sleepingTileCounter;
        //public int fruitTileCounter;
        //public int vegetableTileCounter;
        //public int runningTileCounter;
        //public int waterTileCounter;

        public int currentLevel;
        public Levels[] levels;

        DisplayEndUI display;

        // Start is called before the first frame update
        void Start()
        {
            display = FindObjectOfType<DisplayEndUI>();
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

        public void DisplayCritera()
        {
            for (int i = 0; i < levels[currentLevel].tilePrefabs.Length; i++)
            {
                Instantiate(tilesNeededSet, tilesNeeded.transform.position, tilesNeeded.transform.rotation, tilesNeeded.transform);

                switch (levels[currentLevel].tilePrefabs[i].tile)
                {
                    case Tile.sleeping:
                        tilesNeededSet.GetComponentInChildren<Image>().sprite = sleepingTile;
                        break;

                    case Tile.fruit:
                        tilesNeededSet.GetComponentInChildren<Image>().sprite = fruitTile;
                        break;

                    case Tile.vegetable:
                        tilesNeededSet.GetComponentInChildren<Image>().sprite = vegetableTile;
                        break;

                    case Tile.running:
                        tilesNeededSet.GetComponentInChildren<Image>().sprite = runningTile;
                        break;

                    case Tile.water:
                        tilesNeededSet.GetComponentInChildren<Image>().sprite = waterTile;
                        break;
                }

                tilesNeededSet.GetComponentInChildren<Text>().text = "x " + levels[currentLevel].tilePrefabs[i].count;
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(tilesNeeded.GetComponent<RectTransform>());
        }
    }
}
