using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] Transform attackAreaPosition;
    [SerializeField] Vector2 attackAreaSize;
    [SerializeField] Projectile arrowPrefab;
    [SerializeField] Transform arrowPoint;//Poitn where the arrow will be instantiated
    [SerializeField] float arrowDamage = 5f;
    [SerializeField] float baseDamage = 5f;//Base damage that will modify all attacks amount of damage.
    PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    /// <summary>
    /// Creates a overlapBox area and hits every enemy detected by certain damage.
    /// </summary>
    /// <param name="damage"></param>
    public void MeleeAttack(float damage)
    {
        Collider2D[] enemies = Physics2D.OverlapBoxAll(attackAreaPosition.position, attackAreaSize, 0f, LayerMask.GetMask("Enemy"));
    
        foreach(Collider2D enemy in enemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(damage + baseDamage);
        }
    }

    /// <summary>
    /// Instantiate an arrow.
    /// </summary>
    public void InstantiateArrow()
    {
        player.rgb2D.AddForce(Vector2.right * -transform.localScale.x * 3f, ForceMode2D.Impulse);

        Projectile arrow = Instantiate(arrowPrefab, arrowPoint.position, Quaternion.identity);
        arrow.projectileDamage = arrowDamage + baseDamage;
        arrow.gameObject.transform.localScale = gameObject.transform.localScale;
    }

    private void OnDrawGizmos()
    {
        if (attackAreaPosition == null)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(attackAreaPosition.position, attackAreaSize);
    }
}
