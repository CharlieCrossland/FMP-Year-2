using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    void Update()
    {
        text.SetText("Score: " + SavedVariables.Instance.currentScore);
    }
}
