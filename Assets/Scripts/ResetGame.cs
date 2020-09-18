using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public void ResetGameState()
    {
        Time.timeScale = 0.0f;
        PlayerPrefsHelper.SaveResources(0, 0, 0);
        PlayerPrefsHelper.SaveBuildingLevel(PlayerPrefsHelper.Building.metalMine, 1);
        PlayerPrefsHelper.SaveBuildingLevel(PlayerPrefsHelper.Building.crystalMine, 1);
        PlayerPrefsHelper.SaveBuildingLevel(PlayerPrefsHelper.Building.deuteriumMine, 1);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1.0f;
    }
}
