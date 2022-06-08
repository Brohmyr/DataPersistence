using UnityEngine;
using UnityEngine.UI;

public class UpdateNameAndScore : MonoBehaviour
{
    [SerializeField] private Text _updateNameAndBestScore;

    private void Start()
    {
        GameManager.Instance.LoadBestScore();
        _updateNameAndBestScore.text = $"Best Score : {GameManager.Instance._bestPlayerName} : {GameManager.Instance._bestScore}";
    }
}
