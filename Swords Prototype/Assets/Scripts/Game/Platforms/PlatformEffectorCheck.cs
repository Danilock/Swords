using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffectorCheck : MonoBehaviour
{
    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.rgb2D.velocity.y <= 0.3f)
            {
                gameObject.layer = LayerMask.NameToLayer("Platform");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("No Ground");
        }
    }
}
