using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void SceneLoad(int numbScene)
    {
        SceneManager.LoadScene(numbScene);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
