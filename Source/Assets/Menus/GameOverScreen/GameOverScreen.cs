using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using SimpleJSON;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI final_score;
    public static int score = 0;
    public static bool updated = false;

    public TMP_InputField input;
    private string name;
    public GameObject gameOverMenu;

    public  GameObject newGameButton;

    private void Start()
    {
        // final_score.text = final_score.text + " " + score;
        newGameButton.SetActive(false);
        updated = false;
    }

    public void SubmitButton()
    {
        name = input.GetComponent<TMP_InputField>().text;
        newGameButton.SetActive(true);
        EventSystem.current.currentSelectedGameObject.SetActive(false);
        input.gameObject.SetActive(false);

        // Logica
        LeaderBoardTable.AddEntry(score,name);

        string path = Application.dataPath + "/Data/Highscores.json";
        string jsonString = File.ReadAllText(path);
        LeaderBoardTable.Highscores h = JsonUtility.FromJson<LeaderBoardTable.Highscores>(jsonString);

        LeaderBoardTable.SortHighscores(h);

        if(h.highscores_entry_list.Count > 10)
            LeaderBoardTable.RemoveEntry(10);
        
    }


    public void UpdateScore()
    {
        final_score.text = final_score.text + score;
//        Debug.Log("Final Score" + final_score.text);
        GameController.instance.gameInterface.SetActive(false);
        updated = true;
    }

    

    void Update()
    {
        score = (int)GameController.instance.score_value;
        
        if(!updated)
        {
            if(GameController.instance.gameOver)
            {
                UpdateScore();
            }
        }
        
    }


    public void NewGame()
    {
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        GameController.instance.DestroyScene();
        SceneManager.LoadScene("GameScene");
    }

    public void Menu()
    {
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        GameController.instance.DestroyScene();
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Quit()
    {
        Debug.Log("sda");
        Application.Quit();
    }
}
