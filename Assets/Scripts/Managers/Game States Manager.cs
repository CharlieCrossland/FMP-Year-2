using UnityEngine;

public class GameStatesManager : MonoBehaviour
{
    public static GameStatesManager Instance;

    public enum GameStates
    {
        Ready,
        Playing,
        GracePeriod,
        GameOver,
    };

    public GameStates currentState;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
    }
}
