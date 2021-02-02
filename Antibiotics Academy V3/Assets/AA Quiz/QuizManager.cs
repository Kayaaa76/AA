using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    static bool coinsChange = false;

    List<Questions> qns = new List<Questions>(); //a list to store the questions

    public string nextScene;

    public int sceneID;

    public TextAsset questionsCSV;

    public Text questionNo;
    public Text question;

    public Text LoginToken;

    private string ans;

    private int index = 0;
    private int score;

    static bool ready = false;

    System.DateTime QuizStart;
    System.DateTime QuizEnd;
    // Start is called before the first frame update
    void Start()
    {
        string[] data = questionsCSV.text.Split(new char[] { '\n' });     //a string array that stores the split items from the csv

        for (int i = 1; i < data.Length - 1; i++)                         //for loop that stores the data into its respective variables
        {
            string[] row = data[i].Split(new char[] { ';' });             //splits the data on the ;
            Questions question = new Questions();                         //create a new Question instance
            int.TryParse(row[0], out question.questionNo);                //parse through the integer
            question.questions = row[1];                                  //set the questions from the string in row 1  
            question.answer = row[2].Trim();                              //store the answer from the string in row 2

            qns.Add(question);                                            //add the question instance to the questions list
        }

        questionNo.text = "Question: " + qns[index].questionNo;           //set the questionNo text and question text to the first question, since index starts at 0
        question.text = qns[index].questions;

        LoginToken.text = Login.LoginToken;

        coinsChange = false;
        ready = false;

        QuizStart = System.DateTime.Now;
    }

    void Update()
    {
        if (coinsChange == true)
        {
            StartCoroutine(PostCoinActivity());
            coinsChange = false;
        }

        if (ready == true)
        {
            StartCoroutine(ChangeScene());
            
        }
        else if (ready== false)
        {
            Debug.Log("not ready to change scenes");
        }
    }

    public void TrueBtn()        //function to set the ans to true
    {
        ans = "TRUE";
        CheckAnswer();
    }

    public void FalseBtn()       //function to set the ans to false
    {
        ans = "FALSE";
        CheckAnswer();
    }

    void CheckAnswer()           //function to check answer
    {
        if (index < qns.Count && index != qns.Count - 1)    //if the current qns is less than the total question
        {
            if (ans == qns[index].answer)                   //if answer is the same as the answer in the question instance
            {
                score++;                                    //increase score
            }
            index++;                                        //increase index
            NextQuestion();
        }

        else
        {
            QuizEnd = System.DateTime.Now;
            Debug.Log("No more Questions.");
            Debug.Log(score);
            Debug.Log("You got 100 coins for completing the Quiz!");
            Player.coins += 100;
            coinsChange = true;
            //if (sceneID == 1)
            //{
            //    SceneManager.LoadScene(nextScene);
            //    Player.donePreQuiz = true;
            //}

            //else
            //{
            //    Player.donePostQuiz = true;
            //    Application.Quit();
            //}
        }
    }

    void NextQuestion()         //function to display next question
    {
        if (index < qns.Count)  //if index is less than questions count
        {
            questionNo.text = "Question: " + qns[index].questionNo;  //set the questionNo text and question text based on the index
            question.text = qns[index].questions;
        }
    }

    IEnumerator PostCoinActivity()
    {
        WWWForm formPostCoinActivity = new WWWForm();
        WWW wwwPostCoinActivity = new WWW("http://103.239.222.212/ALIVE2Service/api/game/PostActivity?ActivityTypeName=" + "Player Coins&" + "username=" + Login.tnameField.text + "&ActivityDataValue=" + "Player Coins", formPostCoinActivity);
        yield return wwwPostCoinActivity;
        Debug.Log(wwwPostCoinActivity.text);
        Debug.Log(wwwPostCoinActivity.error);
        Debug.Log(wwwPostCoinActivity.url);
        ready = true;
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSecondsRealtime(1);
        if (sceneID == 1)
        {
            Debug.Log("Time taken for Pre-Game Quiz = " + (QuizEnd - QuizStart).TotalSeconds + " seconds");
            Player.preGameQuizTime = (QuizEnd - QuizStart).ToString();
            Debug.Log(Player.preGameQuizTime);
            Player.preGameQuizScore = score;
            Player.donePreQuiz = true;
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            if (Player.donePreQuiz == true)
            {
                Player.postGameQuizScore = score;
                Player.donePostQuiz = true;
                Application.Quit();
            }
        }
    }
}
