using UnityEngine;

public class SpearAttack : MonoBehaviour
{
    private float attackTimer;
    [SerializeField] private float maxAttackTimer;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private float spearSpeed;

    [SerializeField] private GameObject spearPrefab;

    private GameObject targetOBJ;
    [SerializeField] private Transform target;

    private void Start()
    {
        targetOBJ = GameObject.Find("Player Controller");
        target = targetOBJ.transform;

        spearSpeed = 20;
    }

    private void Update()
    {
        AttackTimer();
        SetShootPoint();
    }

    private void AttackTimer()
    {
        if (attackTimer <= 0)
        {
            // attack
            ThrowSpear();
            attackTimer = maxAttackTimer;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void ThrowSpear()
    {
        GameObject tempSpear = Instantiate(spearPrefab, shootPoint.position, shootPoint.rotation);

        tempSpear.GetComponent<Rigidbody2D>().linearVelocity = shootPoint.right * spearSpeed;
        Destroy(tempSpear, 4f);
    }

    private void SetShootPoint()
    {
        Vector3 targ = target.position;
        targ.z = 0f;

        // gets the centre of object, as it is on parent object we have a pivot point for the game objects children
        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;

        shootPoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
