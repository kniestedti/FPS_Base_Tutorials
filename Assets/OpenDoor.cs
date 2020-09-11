using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Collider m_Collider;
    public AudioClip openSFX;
    public Transform door1;
    public Transform door2;
    bool opening = false;
    bool closing = false;
    bool isOpen = false;
    Vector3 door1DefaultPos = new Vector3(0, 0, 0); 
    Vector3 door2DefaultPos = new Vector3(0, 0, -3);
    float timerLength = 1f;
    float timer;
    public float speed  = 1f;

    void Start()
    {
        m_Collider = GetComponent<Collider>();
        timer = timerLength;
    }

    void Update()
    {
        if (opening && timer > 0f)
        {
            door1.Translate(Vector3.forward * Time.deltaTime * speed);
            door2.Translate(-Vector3.forward * Time.deltaTime * speed);
            timer -= Time.deltaTime;
        } else if (opening && timer <= 0f)
        {
            opening = false;
            timer = timerLength;
            closing = true;
        }

        if (closing && timer > 0f)
        {
            door1.Translate(-Vector3.forward * Time.deltaTime * speed);
            door2.Translate(Vector3.forward * Time.deltaTime * speed);
            timer -= Time.deltaTime;
        } else if (closing && timer <= 0f)
        {
            door1.localPosition = door1DefaultPos;
            door2.localPosition = door2DefaultPos;
            closing = false;
            timer = timerLength;
            isOpen = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Damageable check = other.GetComponent<Damageable>();

        if (check != null)
        {
            if (opening || closing) return;
            
            if (!isOpen)
            {
                Debug.Log("Triggered!");
                timer = timerLength;
                opening = true;
                isOpen = true;
            }
        }
    }
}
