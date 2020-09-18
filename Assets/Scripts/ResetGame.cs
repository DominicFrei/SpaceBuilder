using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class ResetGame : MonoBehaviour
{
    public static void ResetGameState()
    {
        Time.timeScale = 0.0f;

        Resources.Instance.Metal = 0;
        Resources.Instance.Crystal = 0;
        Resources.Instance.Deuterium = 0;
        Resources.Instance.LastUpdate = DateTime.Now;
        Database.SaveResources();

        BuildingEntity metalMine = new BuildingEntity("Metal Mine", BuildingType.MetalMine, 1, false, null);
        BuildingEntity crystalMine = new BuildingEntity("Crystal Mine", BuildingType.CrystalMine, 1, false, null);
        BuildingEntity deuteriumMine = new BuildingEntity("Deuterium Mine", BuildingType.DeuteriumMine, 1, false, null);
        BuildingsEntity buildingsEntity = new BuildingsEntity(metalMine, crystalMine, deuteriumMine);
        Database.SaveBuildings(buildingsEntity);

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        Time.timeScale = 1.0f;
    }
}
