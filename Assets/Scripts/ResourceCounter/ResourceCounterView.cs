using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public interface IResourceCounterView
{
    void UpdateText(int metal, int crystal, int deuterium);
}

public class ResourceCounterView : MonoBehaviour, IResourceCounterView
{
    [SerializeField] private Text _textMetal = null;
    [SerializeField] private Text _textCrystal = null;
    [SerializeField] private Text _textDeuterium = null;

    private IResourceCounterController _resourceCounterController = null;

    private readonly WaitForSeconds _resourcesUpdateInterval = new WaitForSeconds(1.0f);

    private void Start()
    {
        _resourceCounterController = new ResourceCounterController(this);
        if (null == _resourceCounterController)
        {
            Logger.Error("_resourceCounterController is null.");
            return;
        }

        _resourceCounterController.Start();
        StartCoroutine(UpdateResourceCounter());
    }

    private void OnApplicationQuit()
    {
        if (null == _resourceCounterController)
        {
            Logger.Error("_resourceCounterController is null.");
            return;
        }

        _resourceCounterController.OnApplicationQuit();
    }

    private IEnumerator UpdateResourceCounter()
    {
        while (true)
        {
            if (null == _resourceCounterController)
            {
                Logger.Error("_resourceCounterController is null.");
                yield break;
            }

            _resourceCounterController.UpdateUI();
            yield return _resourcesUpdateInterval;
        }
    }

    public void UpdateText(int metal, int crystal, int deuterium)
    {
        _textMetal.text = "Metal: " + metal;
        _textCrystal.text = "Crystal: " + crystal;
        _textDeuterium.text = "Deuterium: " + deuterium;
    }    

}
