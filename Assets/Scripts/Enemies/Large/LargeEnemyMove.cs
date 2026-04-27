using UnityEngine;

public class LargeEnemyMove : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    private GameObject targetOBJ;
    [SerializeField] private Transform target;

    [Header("Editables")]
    [SerializeField] private float moveSpeed;

    [Header("Random Speed")]
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private void Start()
    {
        targetOBJ = GameObject.Find("Player Controller");
        target = targetOBJ.transform;

        SetRandomMoveSpeed();
    }

    private void SetRandomMoveSpeed()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        SetMove();
    }

    private void SetMove()
    {
       // if ()
    }
}
