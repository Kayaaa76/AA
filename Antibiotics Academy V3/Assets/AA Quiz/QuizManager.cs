using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using SimpleJSON;

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

    public GameObject starRatingPanel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetQuestionAnswer());

        string[] data = questionsCSV.text.Split(new char[] { '\n' });     //a string array that stores the split items from the csv

        for (int i = 1; i < data.Length - 1; i++)                         //for loop that stores the data into its respective variables
        {
            string[] row = data[i].Split(new char[] { ';' });             //splits the data on the ;
            Questions question = new Questions();                         //create a new Question instance
            int.TryParse(row[0], out question.questionNo);                //parse through the integer
            question.questions = row[1];                                  //set the questions from the string in row 1  
            question.answer = row[2].Trim();                              //store the answer from the string in row 2

            if (questionsCSV.name == "PreQuestions")
            {
                string fileName = File.ReadAllText(Application.persistentDataPath + "/Pre-Game Question " + i + ".json");
                string tFileName = File.ReadAllText(Application.persistentDataPath + "/Pre-Game Question " + i + " Answer" + ".json");

                JSONObject jSONObject = new JSONObject();
                jSONObject = (JSONObject)JSON.Parse(fileName);

                JSONObject tJSONObject = new JSONObject();
                tJSONObject = (JSONObject)JSON.Parse(tFileName);

                question.questions = jSONObject["questionValue"];
                question.answer = tJSONObject["answer"];
            }
            if (questionsCSV.name == "PostQuestions")
            {
                string fileName = File.ReadAllText(Application.persistentDataPath + "/Post-Game Question " + i + ".json");
                string tFileName = File.ReadAllText(Application.persistentDataPath + "/Post-Game Question " + i + " Answer" + ".json");

                JSONObject jSONObject = new JSONObject();
                jSONObject = (JSONObject)JSON.Parse(fileName);

                JSONObject tJSONObject = new JSONObject();
                tJSONObject = (JSONObject)JSON.Parse(tFileName);

                question.questions = jSONObject["questionValue"];
                question.answer = tJSONObject["answer"];
            }

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
        if (ans == qns[index].answer)                   //if answer is the same as the answer in the question instance
        {
            score++;                                    //increase score
        }

        if (index < qns.Count && index != qns.Count - 1)    //if the current qns is less than the total question
        {
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
            StartCoroutine(Login.UpdateCoins());
            Player.Save();
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
            if(questionsCSV.name == "PostQuestions") //enable star rating menu after post game quiz
            {
                starRatingPanel.SetActive(true);
            }
        }

        Debug.Log("Score is: " + score);
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
            Player.Save();
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            if (Player.donePreQuiz == true)
            {
                Player.postGameQuizScore = score;
                Player.donePostQuiz = true;
                Player.dateEnd = System.DateTime.Now;
                Player.totalDuration = (Player.dateEnd - Player.dateStart).ToString();
                Player.Save();
                Application.Quit();
            }
        }
    }

    IEnumerator GetQuestionAnswer()
    {
        WWW wwwQuestionAnswer = new WWW("http://103.239.222.212/ALIVE2Service/api/game/AllQuestionAnswer");
        yield return wwwQuestionAnswer;
        Debug.Log(wwwQuestionAnswer.text);
        Debug.Log(wwwQuestionAnswer.error);
        Debug.Log(wwwQuestionAnswer.url);

        File.WriteAllText(Application.persistentDataPath + "/AllQuestionAnswer.json", wwwQuestionAnswer.text);

        string jsonString = File.ReadAllText(Application.persistentDataPath + "/AllQuestionAnswer.json");

        QARoot qARoot = new QARoot();
        qARoot = JsonUtility.FromJson<QARoot>("{\"qas\":" + jsonString + "}");

        string preGameQuestionName = "Pre-Game Question ";
        string postGameQuestionName = "Post-Game Question ";
        int x = 1;
        foreach (QA qA in qARoot.qas)
        {
            void SavePreGameQuestion()
            {
                Debug.Log(qA.questionName + "\n" + qA.questionValue);
                QA tQA = new QA();
                tQA.questionName = qA.questionName;
                tQA.questionValue = qA.questionValue;
                tQA.answer = qA.answer;
                string tQAJson = JsonUtility.ToJson(tQA);
                File.WriteAllText(Application.persistentDataPath + "/" + preGameQuestionName + x + ".json", tQAJson);
            }

            void SavePostGameQuestion()
            {
                Debug.Log(qA.questionName + "\n" + qA.questionValue);
                QA tQA = new QA();
                tQA.questionName = qA.questionName;
                tQA.questionValue = qA.questionValue;
                tQA.answer = qA.answer;
                string tQAJson = JsonUtility.ToJson(tQA);
                File.WriteAllText(Application.persistentDataPath + "/" + postGameQuestionName + x + ".json", tQAJson);
            }

            foreach (Answers answer in qA.answer)
            {
                if (answer.answerName.Contains("Pre-Game Q"))
                {
                    Debug.Log(answer.answerName + " -- value: " + answer.answerValue + " | iscorrect: " + answer.isCorrect);
                }
                else if(answer.answerName.Contains("Post-Game Q"))
                {
                    Debug.Log(answer.answerName + " -- value: " + answer.answerValue + " | iscorrect: " + answer.isCorrect);
                }
                void SavePreGameAnswer()
                {
                    Answers tAnswers = new Answers();
                    tAnswers.answerName = answer.answerName;
                    tAnswers.answerValue = answer.answerValue;
                    tAnswers.isCorrect = answer.isCorrect;
                    if(tAnswers.isCorrect == "true")
                    {
                        tAnswers.answer = "TRUE";
                    }
                    else if(tAnswers.isCorrect == "false")
                    {
                        tAnswers.answer = "FALSE";
                    }
                    string answerJson = JsonUtility.ToJson(tAnswers);
                    File.WriteAllText(Application.persistentDataPath + "/" + preGameQuestionName + x + " Answer" + ".json", answerJson);
                }

                void SavePostGameAnswer()
                {
                    Answers tAnswers = new Answers();
                    tAnswers.answerName = answer.answerName;
                    tAnswers.answerValue = answer.answerValue;
                    tAnswers.isCorrect = answer.isCorrect;
                    if (tAnswers.isCorrect == "true")
                    {
                        tAnswers.answer = "TRUE";
                    }
                    else if (tAnswers.isCorrect == "false")
                    {
                        tAnswers.answer = "FALSE";
                    }
                    string answerJson = JsonUtility.ToJson(tAnswers);
                    File.WriteAllText(Application.persistentDataPath + "/" + postGameQuestionName + x + " Answer" + ".json", answerJson);
                }

                if(answer.answerName == "Pre-Game Q1 A1")
                {
                    x = 1;
                    SavePreGameAnswer();
                }
                else if (answer.answerName == "Pre-Game Q2 A1")
                {
                    x = 2;
                    SavePreGameAnswer();
                }
                else if (answer.answerName == "Pre-Game Q3 A1")
                {
                    x = 3;
                    SavePreGameAnswer();
                }
                else if (answer.answerName == "Pre-Game Q4 A1")
                {
                    x = 4;
                    SavePreGameAnswer();
                }
                else if (answer.answerName == "Pre-Game Q5 A1")
                {
                    x = 5;
                    SavePreGameAnswer();
                }
                else if (answer.answerName == "Pre-Game Q6 A1")
                {
                    x = 6;
                    SavePreGameAnswer();
                }
                else if (answer.answerName == "Pre-Game Q7 A1")
                {
                    x = 7;
                    SavePreGameAnswer();
                }
                else if (answer.answerName == "Pre-Game Q8 A1")
                {
                    x = 8;
                    SavePreGameAnswer();
                }
                else if (answer.answerName == "Pre-Game Q9 A1")
                {
                    x = 9;
                    SavePreGameAnswer();
                }
                else if (answer.answerName == "Pre-Game Q10 A1")
                {
                    x = 10;
                    SavePreGameAnswer();
                }
                else if (answer.answerName == "Post-Game Q1 A1")
                {
                    x = 1;
                    SavePostGameAnswer();
                }
                else if (answer.answerName == "Post-Game Q2 A1")
                {
                    x = 2;
                    SavePostGameAnswer();
                }
                else if (answer.answerName == "Post-Game Q3 A1")
                {
                    x = 3;
                    SavePostGameAnswer();
                }
                else if (answer.answerName == "Post-Game Q4 A1")
                {
                    x = 4;
                    SavePostGameAnswer();
                }
                else if (answer.answerName == "Post-Game Q5 A1")
                {
                    x = 5;
                    SavePostGameAnswer();
                }
                else if (answer.answerName == "Post-Game Q6 A1")
                {
                    x = 6;
                    SavePostGameAnswer();
                }
                else if (answer.answerName == "Post-Game Q7 A1")
                {
                    x = 7;
                    SavePostGameAnswer();
                }
                else if (answer.answerName == "Post-Game Q8 A1")
                {
                    x = 8;
                    SavePostGameAnswer();
                }
                else if (answer.answerName == "Post-Game Q9 A1")
                {
                    x = 9;
                    SavePostGameAnswer();
                }
                else if (answer.answerName == "Post-Game Q10 A1")
                {
                    x = 10;
                    SavePostGameAnswer();
                }
            }

            if (qA.questionName == preGameQuestionName + 1)
            {
                x = 1;
                SavePreGameQuestion();
            }
            else if (qA.questionName == preGameQuestionName + 2)
            {
                x = 2;
                SavePreGameQuestion();
            }
            else if (qA.questionName == preGameQuestionName + 3)
            {
                x = 3;
                SavePreGameQuestion();
            }
            else if (qA.questionName == preGameQuestionName + 4)
            {
                x = 4;
                SavePreGameQuestion();
            }
            else if (qA.questionName == preGameQuestionName + 5)
            {
                x = 5;
                SavePreGameQuestion();
            }
            else if (qA.questionName == preGameQuestionName + 6)
            {
                x = 6;
                SavePreGameQuestion();
            }
            else if (qA.questionName == preGameQuestionName + 7)
            {
                x = 7;
                SavePreGameQuestion();
            }
            else if (qA.questionName == preGameQuestionName + 8)
            {
                x = 8;
                SavePreGameQuestion();
            }
            else if (qA.questionName == preGameQuestionName + 9)
            {
                x = 9;
                SavePreGameQuestion();
            }
            else if (qA.questionName == preGameQuestionName + 10)
            {
                x = 10;
                SavePreGameQuestion();
            }

            else if (qA.questionName == postGameQuestionName + 1)
            {
                x = 1;
                SavePostGameQuestion();
            }
            else if (qA.questionName == postGameQuestionName + 2)
            {
                x = 2;
                SavePostGameQuestion();
            }
            else if (qA.questionName == postGameQuestionName + 3)
            {
                x = 3;
                SavePostGameQuestion();
            }
            else if (qA.questionName == postGameQuestionName + 4)
            {
                x = 4;
                SavePostGameQuestion();
            }
            else if (qA.questionName == postGameQuestionName + 5)
            {
                x = 5;
                SavePostGameQuestion();
            }
            else if (qA.questionName == postGameQuestionName + 6)
            {
                x = 6;
                SavePostGameQuestion();
            }
            else if (qA.questionName == postGameQuestionName + 7)
            {
                x = 7;
                SavePostGameQuestion();
            }
            else if (qA.questionName == postGameQuestionName + 8)
            {
                x = 8;
                SavePostGameQuestion();
            }
            else if (qA.questionName == postGameQuestionName + 9)
            {
                x = 9;
                SavePostGameQuestion();
            }
            else if (qA.questionName == postGameQuestionName + 10)
            {
                x = 10;
                SavePostGameQuestion();
            }
        }
    }

    [Serializable]
    public class QARoot
    {
        public QA[] qas;
    }

    [Serializable]
    public class QA
    {
        public string gameCompetencyID;
        public string questionID;
        public string questionName;
        public string questionValue;
        public string questionFeedback;
        public string questionDescription;
        public string dateModified;
        public Answers[] answer;
    }

    [Serializable]
    public class Answers
    {
        //public AnswerA answerA;
        //public AnswerB answerB;
        public string answerID;
        public string answerName;
        public string answerValue;
        public string isCorrect;
        public string questionID;
        public string question;
        public string answer;
    }

    [Serializable]
    public class AnswerA
    {
        public string answerId;
        public string answerName;
        public string answerValue;
        public string isCorrect;
        public string questionID;
        public string question;
    }

    [Serializable]
    public class AnswerB
    {
        public string answerId;
        public string answerName;
        public string answerValue;
        public string isCorrect;
        public string questionID;
        public string question;
    }
}
