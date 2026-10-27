using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultText;

    private void Start()
    {
        resultText.text = TurnBasedCombat.result ? "You Win" : "You Lose";
    }

    public void Retry() 
    {
        SceneManager.LoadScene("Game"); 
    }
}