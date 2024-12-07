using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_SceneManager : MonoBehaviour
{
    public void LoadSceneByName(string name)
    {
        SCR_GameState.SetCurrentGameState(GameState.move);
        SCR_GameStatus.ResetAll();
        SceneManager.LoadScene(name);
    }
    public void LoadSceneByNextIndex()
    {
        SCR_GameState.SetCurrentGameState(GameState.move);
        SCR_GameStatus.ResetAll();

        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCount)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(2);
    }
    public void ResetCurrentScene()
    {
        SCR_GameState.SetCurrentGameState(GameState.move);
        SCR_GameStatus.ResetAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

