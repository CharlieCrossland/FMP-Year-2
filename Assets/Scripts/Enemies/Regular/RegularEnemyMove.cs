using UnityEngine;

public class RegularEnemyMove : MonoBehaviour
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

    void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        rb.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
