using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene(0);
    }
    public void GoToRacingScene()
    {
        SceneManager.LoadScene(1);
    }
    
}
