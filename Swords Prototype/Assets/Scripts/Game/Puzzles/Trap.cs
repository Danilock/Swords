using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class Trap : MonoBehaviour
{
    [SerializeField] float trapDamage = 3f;
    [SerializeField] bool hasAnimation, destroyWhenDesactivate = false, canPushPlayer = false, inParent = false;
    [SerializeField] Vector2 force;
    [SerializeField] GameObject parent;
    Collider2D trapCollider;
    PlayerController player;
    Animator trapAnimator, parentAnimator;

    private void Start()
    {
        trapCollider = GetComponent<Collider2D>();
        player = FindObjectOfType<PlayerController>();
        trapAnimator = GetComponent<Animator>();

        if(parent != null)
        {
            parentAnimator = parent.GetComponent<Animator>();
        }
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
        if (hasAnimation)
        {
            if (inParent)
            {
                parentAnimator.SetBool("hideTrap", false);
                parent.GetComponent<Collider2D>().enabled = true;
                trapCollider.enabled = true;
            }
            else
            {
                trapAnimator.SetBool("hideTrap", false);
            }
        }
    }

    void SetupDesactivating()
    {
        trapCollider.enabled = false;
        if(destroyWhenDesactivate)
        {
            StopAllCoroutines();
            DestroyTrap();
        }
        else if (hasAnimation)
        {
            if (inParent)
            {
                parentAnimator.SetBool("hideTrap", true);
                parent.GetComponent<Collider2D>().enabled = false;
                trapCollider.enabled = false;
            }
            else
            {
                trapAnimator.SetBool("hideTrap", true);
            }
        }
    }

    void DestroyTrap()
    {
        Destroy(gameObject);
    }

    #region Physics
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.TakeDamage(trapDamage);
            if (canPushPlayer)
                player.rgb2D.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.TakeDamage(trapDamage);
            if(canPushPlayer)
                player.rgb2D.AddForce(force, ForceMode2D.Impulse);
        }
    }
    #endregion
}
