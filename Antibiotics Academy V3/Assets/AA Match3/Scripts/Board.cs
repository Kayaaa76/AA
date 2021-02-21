﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public enum GameState
    {
        Wait,
        Move
    }

    public class Board : MonoBehaviour
    {
        public GameState currState = GameState.Move;
        public int width;                           //width of the board
        public int height;                          //height of the board
        public int offSet;
        public GameObject tilePrefab;
        public GameObject[] piecePrefabs;
        public GameObject[,] allPieces;
        private FindMatch findMatch;
        private HealthManager healthManager;
        Criteria criteria;

        private AudioSource matchingAudio;

        public float posOffset;

        void Start()
        {
            matchingAudio = GetComponent<AudioSource>();

            Time.timeScale = 0f;

            healthManager = FindObjectOfType<HealthManager>();
            findMatch = FindObjectOfType<FindMatch>();
            criteria = GameObject.Find("Criteria").GetComponent<Criteria>();

            allPieces = new GameObject[width, height];

            posOffset = this.transform.position.x;

            SetUp();
        }

        private void SetUp()                                     //function to set up the board at the start
        {
            for (int i = 0; i < width; i++)                      //iterate through the width and height
            {
                for (int j = 0; j < height; j++)
                {
                    Vector2 tempPosition = new Vector2(i + posOffset, j + offSet);                                                     //sets tempposition to Vector2(i, j)
                    Vector2 tempPosition1 = new Vector2(i + posOffset, j);
                    GameObject backgroundTile = Instantiate(tilePrefab, tempPosition1, Quaternion.identity) as GameObject;  //instantiate tilePrefab at temp position, which is under backgroundTile game object
                    backgroundTile.transform.parent = this.transform;                                                      //sets backgroundtile gameobject parent as the board gameobject
                    backgroundTile.name = "( " + i + "," + j + " )";                                                       //sets backgroundtile name as their position on the board
                    int indexOfPiece = Random.Range(0, piecePrefabs.Length);                                               //sets index of piece to a random range between 0 and the legnth of the prefabs array
                    int maxIterations = 0;                                                                                 //max iteration = 0
                    while (MatchesAt(i, j, piecePrefabs[indexOfPiece]) && maxIterations < 100)                              //while the piece is equal to the pieces to the left, right, up and down
                    {
                        indexOfPiece = Random.Range(0, piecePrefabs.Length);                                               //resets the indexofpiece to a new one
                        maxIterations++;                                                                                   //increases max iteration
                    }
                    maxIterations = 0;                                                                                     //once matchesat returns false, while loop stops and maxiteration is set to 0
                    GameObject piece = Instantiate(piecePrefabs[indexOfPiece], tempPosition, Quaternion.identity);         //instantiate piece at temp position, which is under piece game object
                    piece.GetComponent<Piece>().row = i;
                    piece.GetComponent<Piece>().column = j;

                    piece.transform.parent = this.transform;                                                               //sets piece gameobject parent as board gameobject 
                                                                                                                           //piece.name = "( " + i + "," + j + " )"; ;                                                              //sets piece name to their position on the board
                    allPieces[i, j] = piece;                                                                              //sets allPrefabs position array to piece
                }
            }
        }

        private bool MatchesAt(int row, int column, GameObject piece)                                                      //bool function to return true or false if the other pieces to the left, right, up and down are the same as this piece
        {
            if (row > 1 && column > 1)
            {
                if (allPieces[row - 1, column].CompareTag(piece.tag) && allPieces[row - 2, column].CompareTag(piece.tag))
                {
                    return true;
                }
                if (allPieces[row, column - 1].CompareTag(piece.tag) && allPieces[row, column - 2].CompareTag(piece.tag))
                {
                    return true;
                }
            }
            else if (column <= 1 || row <= 1)
            {
                if (column > 1)
                {
                    if (allPieces[row, column - 1].CompareTag(piece.tag) && allPieces[row, column - 2].CompareTag(piece.tag))
                    {
                        return true;
                    }
                }
                if (row > 1)
                {
                    if (allPieces[row - 1, column].CompareTag(piece.tag) && allPieces[row - 2, column].CompareTag(piece.tag))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void DestroyMatchesAt(int row, int column)                        //function to destroy
        {
            if (allPieces[row, column].GetComponent<Piece>().isMatched)          //if the piece on that row, column isMatched
            {
                findMatch.currMatches.Remove(allPieces[row, column]);
                Destroy(allPieces[row, column]);                                 //destroy the piece
                healthManager.CalcAddHealth(allPieces[row, column].tag);

                //decreases respective counters when destroying piece
                if(allPieces[row, column].tag == "Sleeping")
                {
                    criteria.sleepingTileCounter--;
                }
                else if (allPieces[row, column].tag == "Fruit")
                {
                    criteria.fruitTileCounter--;
                }
                else if (allPieces[row, column].tag == "Vegetable")
                {
                    criteria.vegetableTileCounter--;
                }
                else if (allPieces[row, column].tag == "Running")
                {
                    criteria.runningTileCounter--;
                }
                else if (allPieces[row, column].tag == "Water")
                {
                    criteria.waterTileCounter--;
                }

                allPieces[row, column] = null;                                   //sets the destroyed piece's position in the array to null

            }
        }

        public void DestroyMatches()                                               //function to iterate through the pieces to check if they should be destroyed
        {
            for (int i = 0; i < width; i++)                                        //iterate through all the pieces on the board
            {
                for (int j = 0; j < height; j++)
                {
                    if (allPieces[i, j] != null)                                    //if the piece is not null
                    {
                        DestroyMatchesAt(i, j);                                    //call DestroyMatchesAt 
                        matchingAudio.Play();
                    }
                }
            }
            StartCoroutine(DecreaseColumn());                                      //call coroutine to collapse the pieces
        }

        private IEnumerator DecreaseColumn()                                       //coroutine function to collapse the pieces
        {
            int nullCount = 0;                                                     //null counter
            for (int i = 0; i < width; i++)                                        //iterate through all the pieces
            {
                for (int j = 0; j < height; j++)
                {
                    if (allPieces[i, j] == null)                                     //if the piece is null
                    {
                        nullCount++;                                                //null count increases
                    }
                    else if (nullCount > 0)                                         //if the nullcount is greater than 0
                    {
                        allPieces[i, j].GetComponent<Piece>().column -= nullCount; //sets the prefabs' column to minus the nullcount
                        allPieces[i, j] = null;                                    //sets that original prefabs position to null
                    }
                }
                nullCount = 0;                                                      //reset null counter
            }
            yield return new WaitForSeconds(.4f);
            StartCoroutine(FillBoard());
        }

        private void RefillBoard()                                                  //function to refill the board
        {
            for (int i = 0; i < width; i++)                                         //for loop through all the pieces
            {
                for (int j = 0; j < height; j++)
                {
                    if (allPieces[i, j] == null)                                    //if piece is null, meaning there is no piece
                    {
                        Vector2 tempPosition = new Vector2(i, j + offSet);          //add a tempposition on the empty piece position, with offset to make it fall
                        int prefabIndex = Random.Range(0, piecePrefabs.Length);     //choose a random number from 0 to the length of piece prefabs
                        GameObject piece = Instantiate(piecePrefabs[prefabIndex], tempPosition, Quaternion.identity);  //instantiate the piece prefab on the tempposition
                        allPieces[i, j] = piece;                                    //sets the allpieces array to that piece
                        piece.GetComponent<Piece>().row = i;                        //set the row of the piece to i 
                        piece.GetComponent<Piece>().column = j;                     //set the row of the piece to j
                    }
                }
            }
        }

        private bool MatchesOnBoard()                                               //bool function to check if any matches on board
        {
            for (int i = 0; i < width; i++)                                         //for loop through all the pieces
            {
                for (int j = 0; j < height; j++)
                {
                    if (allPieces[i, j] != null)                                    //if the piece is not null
                    {
                        if (allPieces[i, j].GetComponent<Piece>().isMatched)        //if the piece is matched
                        {
                            return true;                                            //return true
                        }
                    }
                }
            }
            return false;
        }

        private IEnumerator FillBoard()                                             //function to fill board
        {
            RefillBoard();                                                          //calls refill board to place the pieces
            yield return new WaitForSeconds(.5f);
            while (MatchesOnBoard())                                                //while there are matches on the board
            {
                yield return new WaitForSeconds(.5f);
                DestroyMatches();                                                   //destroy those matches 
            }
            yield return new WaitForSeconds(.5f);
            currState = GameState.Move;                                             //reset gamestate to move
        }
    }

}
