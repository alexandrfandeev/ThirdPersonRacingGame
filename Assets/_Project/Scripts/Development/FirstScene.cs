using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene(1);
    }
}
