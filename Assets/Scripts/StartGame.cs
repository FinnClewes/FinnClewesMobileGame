using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadScene()
        {
        _ = SceneLoader.Instance.Load("Game", Application.exitCancellationToken);
    }
}
