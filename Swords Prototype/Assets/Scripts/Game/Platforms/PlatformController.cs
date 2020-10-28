using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlatformController : MonoBehaviour
{
    Vector2 startPosition, endPosition;
    [SerializeField, Range(0, 1)] float lerpPct; 
    [SerializeField, Range(1, 100)] float lerpSpeed = 1;//Percentage of lerping(0, 1).
    [SerializeField] bool move;
    bool moving;
    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (moving)
        {
            lerpPct += 0.01f * Time.deltaTime * lerpSpeed;
            transform.position = Vector2.Lerp(startPosition, endPosition, lerpPct);
        }
    }

    public void MovePlatform(Transform destination)
    {
        endPosition = destination.position;
        moving = true;
    }
}
