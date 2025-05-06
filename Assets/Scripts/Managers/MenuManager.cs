using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static int difficulty;

    public GameObject Main_menu, Difficulty_menu;

    public void DifficultyChoose()
    {
        Main_menu.SetActive(false);
        Difficulty_menu.SetActive(true);
    }

    public void Menu_Choose()
    {
        Main_menu.SetActive(true);
        Difficulty_menu.SetActive(false);
    }

    public void EasyDifficulty()
    {
        difficulty = 0;
        LoadScene("Game");
    }

    public void MediumDifficulty()
    {
        difficulty = 1;
        LoadScene("Game");
    }

    public void HardDifficulty()
    {
        difficulty = 2;
        LoadScene("Game");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Start()
    {
        Menu_Choose();
    }
}
