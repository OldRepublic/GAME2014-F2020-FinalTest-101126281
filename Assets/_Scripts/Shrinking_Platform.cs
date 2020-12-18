using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrinking_Platform : MonoBehaviour
{
    Vector3 Shrink;
    BoxCollider2D Collider;
    bool test = false;
    // Start is called before the first frame update
    void Start()
    {
        Shrink = new Vector3(-0.02f,-0.02f,0);
       
    }

    // Update is called once per frame
    void Update()
    {
        
        resetSize();
        test = true;//sets reset bool to true and unless set too false criteria met in Ontriggerstay the platform will reset.
    }

    void resetSize()
    {
        if (Time.frameCount % 20 == 0 && test == true && transform.localScale.x != 1.0f && transform.localScale.y != 1.0f)
        {
            transform.localScale += -Shrink;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)//shrinks platform when in contact with the player
    {
        if(collision.gameObject.CompareTag("Player") && transform.localScale.x != 0.0f && transform.localScale.y != 0.0f)
        {
            transform.localScale += Shrink;
            test = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            test = true;
        }
    }
   
}
