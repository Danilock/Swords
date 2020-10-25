using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float force = 2f;
    [SerializeField] public float projectileDamage = 15f;
    [SerializeField] doDamageTo DoDamageTo = doDamageTo.Enemy; 
    enum doDamageTo { Enemy, Player }
    Rigidbody2D projectileRgb;
    // Start is called before the first frame update
    void Start()
    {
        projectileRgb = GetComponent<Rigidbody2D>();
        ProjectileMovement(transform.localScale.x);
    }

    public void ProjectileMovement(float direction)
    {
        projectileRgb.AddForce(transform.right * direction * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && DoDamageTo == doDamageTo.Enemy)
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(projectileDamage);
        }
        else if(collision.gameObject.CompareTag("Player") && DoDamageTo == doDamageTo.Player)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(projectileDamage);
        }

        Destroy(gameObject);
    }


}
