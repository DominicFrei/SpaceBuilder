using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public void ResetGameState()
    {
        Time.timeScale = 0.0f;
        Database.SaveResources(0, 0, 0, DateTime.Now);
        BuildingEntity metalMine = new BuildingEntity("metalMine", 1, false, null);
        BuildingEntity crystalMine = new BuildingEntity("crystalMine", 1, false, null);
        BuildingEntity deuteriumMine = new BuildingEntity("deuteriumMine", 1, false, null);
        BuildingsEntity buildingsEntity = new BuildingsEntity(metalMine, crystalMine, deuteriumMine);
        Database.SaveBuildings(buildingsEntity);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1.0f;
    }
}
