using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnNetwork : MonoBehaviour
{
    void Awake()
    {
        if (!GameVariables.visitedMainMenu && SceneManager.GetActiveScene().name == "MultiplayerMenu")
        {
            GameVariables.visitedMainMenu = true;
        }
        if (!GameVariables.visitedMainMenu)
        {
            SceneManager.LoadScene("MultiplayerMenu");
        }
    }
}
public static class GameVariables
{
    public static bool visitedMainMenu = false;
}