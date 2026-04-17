using System.Collections.Generic;
using UnityEngine;

public class ChooseEffect : MonoBehaviour
{
    readonly float effectAmount = 7;
    float randomNumber;
    [SerializeField] private bool DebugStartGenerator;
    bool hasEffectBeenChosen;

    string chaosEffectName;

    int numberOfReRolls;

    public static List<int> avoidList = new List<int>();

    [Header("References")]
    [SerializeField] private GameObject playerContainer;

    private void Awake()
    {
        avoidList.Clear();
    }

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

            ChaosAnimation.Instance.StartAnimation(chaosEffectName);
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

        // check if number is in avoid list
        if (isNumberInAvoidList())
        {
            PreventMemoryLeakReRoll();
            DebugLogList();
            RandomEffect();
        }
        else // execute chaos effect
        {
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
        }

        hasEffectBeenChosen = true;
    }

    void PreventMemoryLeakReRoll()
    {
        if (numberOfReRolls >= 3)
        {
            avoidList.Clear();
            DebugLogList();
        }
    }

    #region Avoid List
    private void AddNumberToAvoidList()
    {
        // add the generated number
        avoidList.Add((int)randomNumber);
    }

#if UNITY_EDITOR
    private void DebugLogList()
    {
        if (avoidList.Capacity == 0)
        {
            Debug.Log("No values present in this list.");
        }

        foreach (int x in avoidList)
        {
            Debug.Log(x.ToString());
        }
    }
#endif

    // check if random number is in a list
    // add the random number to an avoid list
    private bool isNumberInAvoidList()
    {
        if (avoidList.Contains((int)randomNumber))
        {
            Debug.Log("Number is already in list. Re-rolling number. Number is " + (int)randomNumber);

            numberOfReRolls++;

            return true;
        }
        else
        {
            AddNumberToAvoidList();

            Debug.Log("Adding " + (int)randomNumber + " to avoid list.");

            numberOfReRolls = 0;

            return false;
        }
    }
    #endregion

    #region Effects
    // numbers above the void are the assigned number that the random number generator will check for
    // 0
    private void ToxicGas()
    {
        Debug.Log("Toxic Gas. Number Generated: " + (int)randomNumber);
        DebugStartGenerator = false;

        chaosEffectName = new("New Effect: Toxic Gas");
    }

    // 1
    private void Meteorites()
    {
        Debug.Log("Meteorites. Number Generated: " + (int)randomNumber);
        DebugStartGenerator = false;

        chaosEffectName = new("New Effect: Meteorites");

    }

    // 2
    private void NoEffect()
    {
        Debug.Log("No Effect. Number Generated: " + (int)randomNumber);
        DebugStartGenerator = false;

        chaosEffectName = new("No Effect");
    }

    // 3
    private void TwoRandomEffects()
    {
        Debug.Log("Two Random Effects. Number Generated: " + (int)randomNumber);
        DebugStartGenerator = false;

        chaosEffectName = new("New Effect: Two Random Effects");
    }

    // 4
    private void SlowFireRate()
    {
        Debug.Log("Slow Fire Rate. Number Generated: " + (int)randomNumber);
        DebugStartGenerator = false;

        chaosEffectName = new("New Effect: Slow Fire Rate");

        Shooting.Instance.fireRate = 0.75f;
    }

    // 5
    private void BiggerPlayer()
    {
        Debug.Log("Big Player. Number Generated: " + (int)randomNumber);
        DebugStartGenerator = false;

        chaosEffectName = new("New Effect: Bigger Player");

        // makes player size 2
        playerContainer.transform.localScale = new(2, 2, 2);

        PlayerMovement.Instance.moveSpeed -= 3;
    }

    // 6
    private void TinyPlayer()
    {
        Debug.Log("Tiny Player. Number Generated: " + (int)randomNumber);
        DebugStartGenerator = false;

        chaosEffectName = new("New Effect: Tiny Player");

        // makes player size 0.75
        playerContainer.transform.localScale = new(0.75f, 0.75f, 0.75f);
    }

    // 7
    private void IcyFloor()
    {
        Debug.Log("Icy Floor. Number Generated: " + (int)randomNumber);
        DebugStartGenerator = false;

        chaosEffectName = new("New Effect: Icy Floor");

        PlayerMovement.Instance.icyFloor = true;
    }
    #endregion
}
