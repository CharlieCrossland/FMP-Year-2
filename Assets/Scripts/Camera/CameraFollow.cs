using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float followSpeed;

    Vector3 offset = new(0, 0, -10);

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, followSpeed * Time.deltaTime);
    }
}
