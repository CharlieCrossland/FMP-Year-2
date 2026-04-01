using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 10f;

    public void Update()
    {
        CheckForMouseInput();
    }

    private void CheckForMouseInput()
    {
        if (PlayerInputHandler.Instance.AttackTriggered)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        // creating a temp bullet object so i can access it in this script
        GameObject tempBullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // not doing this in a new bullet script so i can have clearer control over bullet speed and velocity
        tempBullet.GetComponent<Rigidbody2D>().linearVelocity = shootPoint.right * bulletSpeed;
        Destroy(tempBullet, 4f);
    }
}
