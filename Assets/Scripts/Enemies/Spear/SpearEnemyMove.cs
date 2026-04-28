using UnityEngine;

public class SpearEnemyMove : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    private GameObject targetOBJ;
    [SerializeField] private Transform target;

    float distance = 8f;

    private void Start()
    {
        targetOBJ = GameObject.Find("Player Controller");
        target = targetOBJ.transform;
    }

    void Update()
    {
        KeepAwayFromPlayer(); 
    }

    private void KeepAwayFromPlayer()
    {
        transform.position = (transform.position - target.position).normalized * distance + target.position;
    }    
}
