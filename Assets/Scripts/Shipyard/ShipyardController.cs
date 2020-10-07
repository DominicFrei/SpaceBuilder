using UnityEngine;

interface IShipyardController
{
    void BuildShipsButtonClicked();
}

public class ShipyardController : IShipyardController
{
    #region Private Fields
    private readonly IShipyardView _shipyardView = null;
    #endregion

    #region Initialiser / Finaliser
    public ShipyardController(IShipyardView shipyardView)
    {
        _shipyardView = shipyardView;
    }
    #endregion

    #region Public Functions
    public void BuildShipsButtonClicked()
    {
        _shipyardView.ClearInputFields();
        _shipyardView.SetNoResourceTextVisible(true);
    }
    #endregion
}
