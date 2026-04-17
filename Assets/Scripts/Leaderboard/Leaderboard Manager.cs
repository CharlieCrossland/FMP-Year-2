using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardParent;
    [SerializeField] private Transform leaderboardContentParent;
    [SerializeField] private Transform leaderboardItemPrefab;

    [Header("Player Name")]
    [SerializeField] private TMP_InputField inputField;

    private string leaderboardID = "Global_Leaderboard";

    private void Awake()
    {
        leaderboardParent.SetActive(false);
    }

    private async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, SavedVariables.Instance.currentScore);
    }

    private async void Update()
    {
        CreateName();
    }

    private void CreateName()
    {
        string inputtedName = inputField.text.ToString();

        if (inputField.text != null)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                inputField.enabled = false;
                leaderboardParent.SetActive(true);
                UpdateLeaderboard(inputtedName);
            }
        }
    }

    private async void UpdateLeaderboard(string playerName)
    {
        while (Application.isPlaying)
        {
            LeaderboardScoresPage leaderboardScoresPage = await LeaderboardsService.Instance.GetScoresAsync(leaderboardID);

            foreach (Transform t in leaderboardContentParent)
            {
                Destroy(t.gameObject);
            }

            foreach (LeaderboardEntry entry in leaderboardScoresPage.Results)
            {
                Transform leaderboardItem = Instantiate(leaderboardItemPrefab, leaderboardContentParent);
                leaderboardItem.GetChild(0).GetComponent<TextMeshProUGUI>().text = playerName;
                leaderboardItem.GetChild(1).GetComponent<TextMeshProUGUI>().text = entry.Score.ToString();
            }

            await Task.Delay(500);
        }
    }
}
