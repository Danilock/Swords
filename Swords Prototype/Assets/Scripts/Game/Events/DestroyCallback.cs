using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCallback : MonoBehaviour
{
    public void DestroyObject(float seconds)
    {
        Destroy(gameObject, seconds);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
