using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileExample : MonoBehaviour
{
    [SerializeField] int limit = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while(limit > 0)
        {
            Debug.Log("Hello World");
            limit--;
        }
    }
}
