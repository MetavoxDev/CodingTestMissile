using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [Header("Scenes Settings")]
    [SerializeField] private string gameplayScene = "Gameplay";
    //[SerializeField] private string optionsScene = "Options";

    public void LaunchGame(int _levelIndex)
    {
        GameMaster.Instance.SetupLevel(_levelIndex);
        SceneManager.LoadScene(gameplayScene, LoadSceneMode.Single);
    }
}
