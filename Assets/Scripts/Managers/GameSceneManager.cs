using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    #region Singleton
    private static GameSceneManager instance;
    private GameSceneManager() { }
    public static GameSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameSceneManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("GameSceneManager");
                    instance = obj.AddComponent<GameSceneManager>();
                }
            }
            return instance;
        }
    }
    #endregion

    public void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        
        SceneManager.LoadScene(currentScene.name);
    }
}
