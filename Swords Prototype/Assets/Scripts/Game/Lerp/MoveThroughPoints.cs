using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(LerpMovement))]
public class MoveThroughPoints : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] bool flipX, flixY;
    LerpMovement lerp;
    int actualPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        lerp = GetComponent<LerpMovement>();
        lerp.Move(points[actualPoint]);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, points[actualPoint].position) < .01f)
        {
            if (actualPoint < points.Length - 1)
                actualPoint++;
            else if (actualPoint == points.Length - 1)
                actualPoint = 0;

            lerp.Move(points[actualPoint]);
            Flip();
        }   
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        if(flipX)
            theScale.x *= -1;
        if(flixY)
            theScale.y *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmos()
    {
        if (points.Length == 0)
            return;
        int i = 1;
        Gizmos.color = Color.black * .5f;
        foreach(Transform point in points)
        {
            Gizmos.DrawSphere(point.position, .2f);
            #if UNITY_EDITOR	
                Handles.Label(point.position, i.ToString());
            #endif
            i++;
        }
    }
}
