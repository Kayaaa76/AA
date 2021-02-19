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
        public bool limitMoves;
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

        M3LevelSelect m3ls;

        int currentLevel;

        // Start is called before the first frame update
        void Start()
        {
            m3ls = GameObject.Find("Buttons").GetComponent<M3LevelSelect>();

            moveCounter = 0;

            DisplayCritera();
        }

        // Update is called once per frame
        void Update()
        {
            currentLevel = m3ls.playingLevel;

            movesText.text = moveCounter.ToString();
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
