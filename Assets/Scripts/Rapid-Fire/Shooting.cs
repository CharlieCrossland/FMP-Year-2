using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 10f;

    bool stopShooting;

    float fireRateTimer;
    public float fireRate = 0.5f;

    public void Update()
    {
        CheckForShoot();
        RotateGunAroundPlayer();
    }

    private void CheckForShoot()
    {
        if (PlayerInputHandler.Instance.AttackTriggered && stopShooting == false)
        {
            Shoot();

            stopShooting = true;
        }

        // prevents shooting while timer goes down
        // once timer reaches 0, the player can shoot
        if (stopShooting)
        {
            if (fireRateTimer <= 0)
            {
                fireRateTimer = fireRate;
                stopShooting = false;
            }
            else
            {
                fireRateTimer -= Time.deltaTime;
            }
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

    private void RotateGunAroundPlayer()
    {
        // create a new vector3 for mouse position and set it to the mouse position found in unitys input system

        // rotate the player to face the mouse position on z axis
        // translate mouse x and y to rotate player on z

        Vector3 mousePoint = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePoint);


        Vector3 targ = mousePos;
        targ.z = 0f;

        // gets the centre of object, as it is on parent object we have a pivot point for the game objects children
        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        //point = playerCentre.transform.position;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;

        //transform.RotateAround(point, Vector3.forward, angle);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
