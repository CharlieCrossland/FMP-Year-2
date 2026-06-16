using System.Collections;
using UnityEngine;

public class Meteorites : MonoBehaviour
{
    [SerializeField] private float fallSpeed;
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private CircleCollider2D col;
    [SerializeField] private SpriteRenderer sr;
    bool particlePlayed;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Drop();
    }

    private void Drop()
    {
        if (transform.position.y >= 0)
        {
            transform.Translate(0, -fallSpeed * Time.deltaTime, 0);
        }
        else
        {
            Hide();
            EnableHitBox();
            PlayExplosionEffect();
        }
    }

    private void Hide()
    {
        sr.color = new Color(0, 0, 0, 0);
    }

    private void EnableHitBox()
    {
        col.enabled = true;
    }

    private void PlayExplosionEffect()
    {
        if (!particlePlayed)
        {
            explosionEffect.Play();
            particlePlayed = true;
        }
    }
}
