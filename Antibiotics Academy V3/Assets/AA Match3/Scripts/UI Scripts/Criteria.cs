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
        //counter for moves
        public int moveCounter;

        //text for moves
        public Text movesText;

        //parent for all criteria objects
        public GameObject tilesNeeded;

        //critera object set
        public GameObject tilesNeededSet;

        //sprites for different tiles
        public Sprite sleepingTile;
        public Sprite fruitTile;
        public Sprite vegetableTile;
        public Sprite runningTile;
        public Sprite waterTile;

        //counter for different tiles in tilesNeeded set
        public int sleepingTileCounter;
        public int fruitTileCounter;
        public int vegetableTileCounter;
        public int runningTileCounter;
        public int waterTileCounter;

        //text for different tiles in tilesNeeded set
        Text sleepingCounterText;
        Text fruitCounterText;
        Text vegetableCounterText;
        Text runningCounterText;
        Text waterCounterText;

        public bool clearTiles;

        //element for levels array
        public int currentLevel;

        //levels array
        public Levels[] levels;

        DisplayEndUI display;
        HealthManager hm;

        // Start is called before the first frame update
        void Start()
        {
            display = FindObjectOfType<DisplayEndUI>();
            hm = FindObjectOfType<HealthManager>();
        }

        // Update is called once per frame
        void Update()
        {
            updateTileCounter();

            if (levels[currentLevel].limitMoves == 0) //infinite moves if no limit is set
            {
                movesText.text = "\u221E"; //set text to infinite symbol
            }
            else //when limit is set
            {
                movesText.text = moveCounter.ToString(); //set text to move counter

                if(moveCounter < 1 && (clearTiles == false || hm.currentHealth < 100)) //trigger game lost if out of moves to clear criteria
                {
                    display.DisplayDeathUI();
                }
            }
        }

        void updateTileCounter() //set respective text to respective tile counters
        {
            if (sleepingCounterText != null)
            {
                if(sleepingTileCounter < 1) //set counter to remain at 0 once it is reached
                {
                    sleepingTileCounter = 0;
                }
                sleepingCounterText.text = "x " + sleepingTileCounter; //set text to tile counter
            }

            if (fruitCounterText != null)
            {
                if (fruitTileCounter < 1) //set counter to remain at 0 once it is reached
                {
                    fruitTileCounter = 0;
                }
                fruitCounterText.text = "x " + fruitTileCounter; //set text to tile counter
            }

            if (vegetableCounterText != null)
            {
                if (vegetableTileCounter < 1) //set counter to remain at 0 once it is reached
                {
                    vegetableTileCounter = 0;
                }
                vegetableCounterText.text = "x " + vegetableTileCounter; //set text to tile counter
            }

            if (runningCounterText != null)
            {
                if (runningTileCounter < 1) //set counter to remain at 0 once it is reached
                {
                    runningTileCounter = 0;
                }
                runningCounterText.text = "x " + runningTileCounter; //set text to tile counter
            }

            if (waterCounterText != null)
            {
                if (waterTileCounter < 1) //set counter to remain at 0 once it is reached
                {
                    waterTileCounter = 0;
                }
                waterCounterText.text = "x " + waterTileCounter; //set text to tile counter
            }

            if (sleepingTileCounter > 0 || fruitTileCounter > 0 || vegetableTileCounter > 0 || runningTileCounter > 0 || waterTileCounter > 0) //tile criteria is not cleared if any tile counter is more than 0
            {
                clearTiles = false;
            }
            else clearTiles = true; //tile criteria is cleared

            foreach(Transform criteriaSet in tilesNeeded.transform)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(criteriaSet.GetComponent<RectTransform>()); //rebuild layout to display properly
            }
        }

        public void DisplayCritera()
        {
            for (int i = 0; i < levels[currentLevel].tilePrefabs.Length; i++) //go through each tile criteria for current level
            {
                GameObject tileCriteria = Instantiate(tilesNeededSet, tilesNeeded.transform.position, tilesNeeded.transform.rotation, tilesNeeded.transform); //create the tile display set

                Image tileImage = tileCriteria.GetComponentInChildren<Image>(); //get image component of created display set
                Text counterText = tileCriteria.GetComponentInChildren<Text>(); //get text component of created display set
                int tileCounter = levels[currentLevel].tilePrefabs[i].count; //get count of tile criteria

                switch (levels[currentLevel].tilePrefabs[i].tile) //set variables according to tile chosen
                {
                    case Tile.sleeping:
                        tileImage.sprite = sleepingTile; //set display sprite as fruit tile
                        sleepingCounterText = counterText; //set display text as fruit counter text
                        sleepingTileCounter = tileCounter; //set tile criteria count to fruit tile counter
                        break;

                    case Tile.fruit:
                        tileImage.sprite = fruitTile; //set display sprite as fruit tile
                        fruitCounterText = counterText; //set display text as fruit counter text
                        fruitTileCounter = tileCounter; //set tile criteria count to fruit tile counter
                        break;

                    case Tile.vegetable:
                        tileImage.sprite = vegetableTile; //set display sprite as vegetable tile
                        vegetableCounterText = counterText; //set display text as vegetable counter text
                        vegetableTileCounter = tileCounter; //set tile criteria count to vegetable tile counter
                        break;

                    case Tile.running:
                        tileImage.sprite = runningTile; //set display sprite as running tile
                        runningCounterText = counterText; //set display text as running counter text
                        runningTileCounter = tileCounter; //set tile criteria count to running tile counter
                        break;

                    case Tile.water:
                        tileImage.sprite = waterTile; //set display sprite as water tile
                        waterCounterText = counterText; //set display text as water counter text
                        waterTileCounter = tileCounter; //set tile criteria count to water tile counter
                        break;
                }
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(tilesNeeded.GetComponent<RectTransform>()); //rebuild layout to display properly
        }
    }
}
