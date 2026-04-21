using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardParent;
    [SerializeField] private Transform leaderboardContentParent;
    [SerializeField] private Transform leaderboardItemPrefab;

    [Header("Player Name")]
    [SerializeField] private GameObject inputFieldParent;
    [SerializeField] private TMP_InputField inputField;

    [Header("Entry Limit")]
    public static List<GameObject> entryAmountList = new List<GameObject>();
    private float entryLimit = 5f;

    private string leaderboardID = "Global_Leaderboard";

    private void Awake()
    {
        inputFieldParent.SetActive(true);
        leaderboardParent.SetActive(false);
    }

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, SavedVariables.Instance.currentScore);
    }

    private void Update()
    {
        CreateName();
    }

    // this lets the player create their own name to put on the leaderboard
    private void CreateName()
    {
        string inputtedName = inputField.text.ToString();

        if (inputField.text != null)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                inputFieldParent.SetActive(false);
                leaderboardParent.SetActive(true);

                AuthenticationService.Instance.UpdatePlayerNameAsync(inputtedName);

                UpdateLeaderboard();
            }
        }
    }

    // this will constantly update itself keeping leaderboard entries up to date for as long as the player is on this scene
    private async void UpdateLeaderboard()
    {
        while (Application.isPlaying)
        {
            LeaderboardScoresPage leaderboardScoresPage = await LeaderboardsService.Instance.GetScoresAsync(leaderboardID);

            await AuthenticationService.Instance.GetPlayerNameAsync();

            foreach (Transform t in leaderboardContentParent)
            {
                Destroy(t.gameObject);
                Debug.Log("Destroy " + t.gameObject.name);
            }

            // Runs for either the amount of leadboard results OR 5 (entry limit), whichever is lower!
            for (int i = 0; i < Mathf.Min(leaderboardScoresPage.Results.Count, entryLimit); i++) 
            {
                Transform leaderboardItem = Instantiate(leaderboardItemPrefab, leaderboardContentParent);
                leaderboardItem.GetChild(0).GetComponent<TextMeshProUGUI>().text = leaderboardScoresPage.Results[i].PlayerName.ToString();
                leaderboardItem.GetChild(1).GetComponent<TextMeshProUGUI>().text = leaderboardScoresPage.Results[i].Score.ToString();

                // entryAmountList.Add(leaderboardItem.gameObject);
            }

            /*foreach (LeaderboardEntry entry in leaderboardScoresPage.Results)
            {
                if (entryAmountList.Count <= entryLimit)
                {
                    Transform leaderboardItem = Instantiate(leaderboardItemPrefab, leaderboardContentParent);
                    leaderboardItem.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerName;
                    leaderboardItem.GetChild(1).GetComponent<TextMeshProUGUI>().text = entry.Score.ToString();

                    entryAmountList.Add(leaderboardItem.gameObject);
                }
            }*/

            await Task.Delay(500);
        }
    }
}
