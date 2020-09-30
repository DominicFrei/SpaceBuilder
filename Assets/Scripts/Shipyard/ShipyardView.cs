using System;
using UnityEngine;
using UnityEngine.UI;

public interface IShipyardView
{
    void UpdateFighterText(string text);
    void UpdateBattleshipText(string text);
    void UpdateCarhoshipText(string text);

    int GetFighterInputFieldText();
    int GetBattleshipInputFieldText();
    int GetCargoshipInputFieldText();

    void ClearInputFields();

    void SetNoResourceTextVisible(bool isVisible);
}

public class ShipyardView : MonoBehaviour, IShipyardView
{
    #region Private Fields
    // Ship Texts
    [SerializeField] private Text _textFighter = null;
    [SerializeField] private Text _textBattleship = null;
    [SerializeField] private Text _textCargoship = null;

    // Ship Inputs
    [SerializeField] private InputField _inputFighter = null;
    [SerializeField] private InputField _inputBattleship = null;
    [SerializeField] private InputField _inputCargoship = null;
    [SerializeField] private Text _noResourcesText = null;

    // Queue
    [SerializeField] private Text _textQueue = null;
    #endregion

    #region Public Functions
    public void UpdateFighterText(string text)
    {
        if (null == _textFighter)
        {
            Logger.Error("_textFighter is null.");
            return;
        }

        _textFighter.text = text;
    }

    public void UpdateBattleshipText(string text)
    {
        if (null == _textBattleship)
        {
            Logger.Error("_textBattleship is null.");
            return;
        }

        _textBattleship.text = text;
    }

    public void UpdateCarhoshipText(string text)
    {
        if (null == _textCargoship)
        {
            Logger.Error("_textCargoship is null.");
            return;
        }

        _textCargoship.text = text;
    }

    public int GetFighterInputFieldText()
    {
        if (null == _inputFighter)
        {
            Logger.Error("_inputFighter is null.");
            return 0;
        }

        int result = 0;
        try
        {
            result = Int32.Parse(_inputFighter.text);
        }
        catch (FormatException)
        {
            Logger.Error("Could not parse fighter input: " + _inputFighter.text);
        }

        return result;
    }

    public int GetBattleshipInputFieldText()
    {
        if (null == _inputBattleship)
        {
            Logger.Error("_inputBattleship is null.");
            return 0;
        }

        int result = 0;
        try
        {
            result = Int32.Parse(_inputBattleship.text);
        }
        catch (FormatException)
        {
            Logger.Error("Could not parse battleship input: " + _inputBattleship.text);
        }

        return result;
    }

    public int GetCargoshipInputFieldText()
    {
        if (null == _inputCargoship)
        {
            Logger.Error("_inputCargoship is null.");
            return 0;
        }

        int result = 0;
        try
        {
            result = Int32.Parse(_inputCargoship.text);
        }
        catch (FormatException)
        {
            Logger.Error("Could not parse cargoship input: " + _inputCargoship.text);
        }

        return result;
    }

    public void ClearInputFields()
    {
        if (null == _inputFighter || null == _inputBattleship || null == _inputCargoship)
        {
            Logger.Error("At least one of the text fields is null.");
            return;
        }

        _inputFighter.text = "";
        _inputBattleship.text = "";
        _inputCargoship.text = "";
    }

    public void SetNoResourceTextVisible(bool isVisible)
    {
        _noResourcesText.gameObject.SetActive(isVisible);
    }
    #endregion

    #region User Interactions
    public void BuildShipsButtonClicked()
    {
    }

    public void ShipyardBackButtonClicked()
    {
        Database.SaveResources();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScreen");
    }
    #endregion
}
