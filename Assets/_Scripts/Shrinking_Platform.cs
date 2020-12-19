using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds
{
    Shrinking_Sound,
    Growing_Sound
}

public class Shrinking_Platform : MonoBehaviour
{
    Vector3 Shrink;
    BoxCollider2D Collider;
    bool ResetSize = false;
    float StartLoc;
    float direction = 1.0f;

    public AudioSource[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        Shrink = new Vector3(-0.01f, -0.01f, 0);
        StartLoc = transform.position.y;

        sounds = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        resetSize();
        ResetSize = true;//sets reset bool to true and unless set too false criteria met in Ontriggerstay the platform will reset.
        floating();
        CheckBounds();
    }

    void resetSize()
    {
        if (Time.frameCount % 10 == 0 && ResetSize == true && transform.localScale.x != 1.0f && transform.localScale.y != 1.0f)
        {
            transform.localScale += -Shrink;
            if (Time.frameCount % 400 == 0)
            {
                sounds[(int)Sounds.Shrinking_Sound].Stop();
                sounds[(int)Sounds.Growing_Sound].Play();
            }
        }
        else if (ResetSize == false)
        {
            sounds[(int)Sounds.Growing_Sound].Stop();
            if(Time.frameCount % 300 == 0)
            sounds[(int)Sounds.Shrinking_Sound].Play();
        }
    }

    void floating ()
    {
        transform.position += new Vector3(0.0f, 0.5f * direction * Time.deltaTime, 0.0f);
    }

    void CheckBounds()
    {
        if (transform.position.y > StartLoc + 0.2f)//the number being added controls the bounds in which the platform floats between
        {
            direction = -1.0f;
        }
        else if (transform.position.y < StartLoc - 0.2f)
        {
            direction = 1.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            sounds[(int)Sounds.Growing_Sound].Stop();
            sounds[(int)Sounds.Shrinking_Sound].Play();          
        }
    }

    private void OnTriggerStay2D(Collider2D collision)//shrinks platform when in contact with the player
    {
        if(collision.gameObject.CompareTag("Player") && transform.localScale.x != 0.0f && transform.localScale.y != 0.0f)
        {           
            transform.localScale += Shrink;
            ResetSize = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ResetSize = true;
            sounds[(int)Sounds.Shrinking_Sound].Stop();
            sounds[(int)Sounds.Growing_Sound].Play();
        }
    }
   
}
