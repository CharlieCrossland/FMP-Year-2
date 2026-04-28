using UnityEngine;
using UnityEngine.UIElements;

public class LargeEnemyMove : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    private GameObject targetOBJ;
    [SerializeField] private Transform target;

    [Header("Editables")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxMoveTime;

    private float moveTimer;

    [Header("Move")]
    private float timer;
    [SerializeField] private float maxTimer;
    bool canMove;

    [Header("Random Speed")]
    [SerializeField] private float minMoveTimeRandom;
    [SerializeField] private float maxMoveTimeRandom;

    private void Start()
    {
        targetOBJ = GameObject.Find("Player Controller");
        target = targetOBJ.transform;

        moveSpeed = 30f;

        SetRandomMoveTimer();
    }

    private void SetRandomMoveTimer()
    {
        maxMoveTime = Random.Range(minMoveTimeRandom, maxMoveTimeRandom);
    }

    private void Update()
    {

        if (canMove == true)
        {
            MoveEnemy();
        }
        else
        {
            MoveTimer();
        }
    }

    private void MoveTimer()
    {
        if (moveTimer <= 0)
        {
            canMove = true;
            moveTimer = maxMoveTime;
        }
        else
        {
            moveTimer -= Time.deltaTime;
        }
    }

    private void MoveEnemy()
    {
        if (timer <= 0)
        {
            timer = maxTimer;
            canMove = false;
        }
        else
        {
            rb.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            timer -= Time.deltaTime;
        }
    }


}
