using UnityEngine;
using System.Collections.Generic;

public class HeartUI : MonoBehaviour
{
    public static HeartUI Instance;

    [SerializeField] private GameObject heartPrefab;
    List<HealthHeart> hearts = new List<HealthHeart>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Invoke("DrawHearts", 0.25f);
    }

    public void DrawHearts()
    {
        ClearHearts();

        for (int i = 0; i < SavedVariables.Instance.maxHealth; i++)
        {
            CreateEmptyHeart();
        }

        for (int i = 0; i < PlayerHealth.Instance.healthAmount; i++)
        {
            hearts[i].SetHeartImage((HeartStatus)1);
        }

    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }
}
