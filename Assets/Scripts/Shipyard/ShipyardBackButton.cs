public class ShipyardBackButton : UnityEngine.MonoBehaviour
{
    public void ShipyardBackButtonClicked()
    {
        Database.SaveResources();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScreen");
    }
}
