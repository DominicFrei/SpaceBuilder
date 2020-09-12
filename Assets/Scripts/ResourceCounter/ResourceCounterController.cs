using System;

public interface IResourceCounterController
{
    void Start();
    void UpdateUI();
    void OnApplicationQuit();
}

public class ResourceCounterController : IResourceCounterController
{
    private IResourceCounterView _resourcesCounterView = null;

    private int _metal = 0;
    private int _crystal = 0;
    private int _deuterium = 0;

    private int metalPerSecond = 20;
    private int crystalPerSecond = 10;
    private int deuteriumPerSecond = 5;

    public ResourceCounterController(IResourceCounterView resourceCounterView)
    {
        _resourcesCounterView = resourceCounterView;
    }

    public void Start()
    {
        LoadResources();
    }

    public void UpdateUI()
    {
        _metal += metalPerSecond;
        _crystal += crystalPerSecond;
        _deuterium += deuteriumPerSecond;

        if (null == _resourcesCounterView)
        {
            Logger.Error("_resourcesCounterView is null.");
            return;
        }
        _resourcesCounterView.UpdateText(_metal, _crystal, _deuterium);
    }

    public void OnApplicationQuit()
    {
        Database.SaveResources(_metal, _crystal, _deuterium, DateTime.Now);
    }

    private void LoadResources()
    {
        (int metal, int crystal, int deuterium, DateTime lastUpdate) = Database.LoadResources();

        int secondsSinceLastUpdate = DateHelper.DifferenceToNowInSeconds(lastUpdate);

        int addedMetal = secondsSinceLastUpdate * metalPerSecond;
        int addedCrystal = secondsSinceLastUpdate * crystalPerSecond;
        int addedDeuterium = secondsSinceLastUpdate * deuteriumPerSecond;

        _metal = metal + addedMetal;
        _crystal = crystal + addedCrystal;
        _deuterium = deuterium + addedDeuterium;

        Logger.Info("Metal added: " + addedMetal);
        Logger.Info("Crystal added: " + addedCrystal);
        Logger.Info("Deuterium added: " + addedDeuterium);
    }
}
