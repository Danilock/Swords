using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class Trap : MonoBehaviour
{
    [SerializeField] float trapDamage = 3f;
    [SerializeField] bool hasAnimation, destroyWhenDesactivate = false;
    Collider2D trapCollider;
    PlayerController player;
    SpriteRenderer sprite;//TODO: Delete this weirdy variable.

    private void Start()
    {
        trapCollider = GetComponent<Collider2D>();
        player = FindObjectOfType<PlayerController>();
        sprite = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Desactivate the trap permanently.
    /// </summary>
    public void DesactivateTrapCallback()
    {
        SetupDesactivating();
    }

    /// <summary>
    /// Desactivate a trap for certain time.
    /// </summary>
    /// <param name="time"></param>
    public void DesactivateTrapCallback(float time)
    {
        StartCoroutine(DesactivateByTime(time));
    }

    IEnumerator DesactivateByTime(float timeToActicate)
    {
        SetupDesactivating();
        yield return new WaitForSeconds(timeToActicate);
        trapCollider.enabled = true;
        sprite.enabled = true;
    }

    void SetupDesactivating()
    {
        trapCollider.enabled = false;
        sprite.enabled = false;
        if (hasAnimation)
        {
            //TODO: Set trap animation.
        }
        else if(destroyWhenDesactivate)
        {
            Destroy(gameObject);
        }
    }

    #region Physics
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.TakeDamage(trapDamage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.TakeDamage(trapDamage);
        }
    }
    #endregion
}
