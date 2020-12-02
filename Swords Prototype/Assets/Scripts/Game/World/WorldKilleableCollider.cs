using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldKilleableCollider : MonoBehaviour
{
    PlayerController player;
    GameManager gm;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gm.PlayerLoose();
        }
    }
}
