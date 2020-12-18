using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrinking_Platform : MonoBehaviour
{
    Vector3 Shrink;
    // Start is called before the first frame update
    void Start()
    {
        Shrink = new Vector3(-0.1f,-0.1f,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)//shrinks platform when in contact with the player
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            transform.localScale += Shrink;
        }
    }
}
