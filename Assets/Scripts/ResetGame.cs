using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public void ResetGameState()
    {
        Time.timeScale = 0.0f;
        Database.SaveResources(0, 0, 0, DateTime.Now);
        Database.SaveBuildingLevel(Database.Building.metalMine, 1);
        Database.SaveBuildingLevel(Database.Building.crystalMine, 1);
        Database.SaveBuildingLevel(Database.Building.deuteriumMine, 1);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1.0f;
    }
}
