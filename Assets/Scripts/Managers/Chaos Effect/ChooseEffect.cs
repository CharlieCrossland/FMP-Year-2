using UnityEngine;

public class ChooseEffect : MonoBehaviour
{
    readonly float effectAmount = 7;
    float randomNumber;
    [SerializeField] private bool DebugStartGenerator;
    bool hasEffectBeenChosen;

    [Header("References")]
    [SerializeField] private GameObject playerContainer;

    void Update()
    {
        CheckGameState();
    }

    // checking if it is grace period
    // to stop it from constantly picking effects, a bool will be set true when an effect has been chosen and will only be set false when the game state changes from grace period
    private void CheckGameState()
    {
        if ((GameStatesManager.Instance.currentState == GameStatesManager.GameStates.GracePeriod && !hasEffectBeenChosen) || DebugStartGenerator)
        {
            ResetValues();

            RandomEffect();
        }
        else
        {
            hasEffectBeenChosen = false;
        }
    }

    // reset all values that could be changed by a chaos effect
    private void ResetValues()
    {
        playerContainer.transform.localScale = new(1, 1, 1);
        PlayerMovement.Instance.moveSpeed = 10;

        PlayerMovement.Instance.icyFloor = false;
        Shooting.Instance.fireRate = 0.1f;
    }

    // pick a random number which will equal a function
    // function then happens
    private void RandomEffect()
    {
        randomNumber = Random.Range(0, effectAmount);

        switch ((int)randomNumber)
        {
            case 0:
                ToxicGas();
                break;
            case 1:
                Meteorites();
                break;
            case 2:
                NoEffect();
                break;
            case 3:
                TwoRandomEffects();
                break;
            case 4:
                SlowFireRate();
                break;
            case 5:
                BiggerPlayer();
                break;
            case 6:
                TinyPlayer();
                break;
            case 7:
                IcyFloor();
                break;
        }

        hasEffectBeenChosen = true;
    }

    // numbers above the void are the assigned number that the random number generator will check for
    // 0
    private void ToxicGas()
    {
        Debug.Log("Toxic Gas. Number Generated: " + randomNumber);
        DebugStartGenerator = false;
    }

    // 1
    private void Meteorites()
    {
        Debug.Log("Meteorites. Number Generated: " + randomNumber);
        DebugStartGenerator = false;
    }

    // 2
    private void NoEffect()
    {
        Debug.Log("No Effect. Number Generated: " + randomNumber);
        DebugStartGenerator = false;
    }

    // 3
    private void TwoRandomEffects()
    {
        Debug.Log("Two Random Effects. Number Generated: " + randomNumber);
        DebugStartGenerator = false;
    }

    // 4
    private void SlowFireRate()
    {
        Debug.Log("Slow Fire Rate. Number Generated: " + randomNumber);
        DebugStartGenerator = false;

        Shooting.Instance.fireRate = 0.75f;
    }

    // 5
    private void BiggerPlayer()
    {
        Debug.Log("Big Player. Number Generated: " + randomNumber);
        DebugStartGenerator = false;

        // makes player size 2
        playerContainer.transform.localScale = new(2, 2, 2);

        PlayerMovement.Instance.moveSpeed -= 3;
    }

    // 6
    private void TinyPlayer()
    {
        Debug.Log("Tiny Player. Number Generated: " + randomNumber);
        DebugStartGenerator = false;

        // makes player size 0.75
        playerContainer.transform.localScale = new(0.75f, 0.75f, 0.75f);
    }

    // 7
    private void IcyFloor()
    {
        Debug.Log("Icy Floor. Number Generated: " + randomNumber);
        DebugStartGenerator = false;

        PlayerMovement.Instance.icyFloor = true;
    }
}
