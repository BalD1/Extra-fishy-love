using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private GameObject explosion;

    [HideInInspector] public int damagesToInflict;

    void Update()
    {
        this.transform.Translate(Vector2.right * speed * Time.deltaTime); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamages(damagesToInflict);
            Instantiate(explosion, this.transform.position, Quaternion.identity);
        }
            this.gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
