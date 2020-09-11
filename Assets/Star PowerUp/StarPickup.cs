using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickup : MonoBehaviour
{
    Pickup m_Pickup;
    
    [Header("Parameters")]
    public float invincibilityDuration = 10f; 
    void Start()
    {
        // get pickup component on same instance
        m_Pickup = GetComponent<Pickup>();
        DebugUtility.HandleErrorIfNullGetComponent<Pickup, HealthPickup>(m_Pickup, this, gameObject);

        // Subscribe to pickup action
        m_Pickup.onPick += OnPicked;
    }

    void OnPicked(PlayerCharacterController player)
    {

        if (!player.starIsOn)
        {
            player.starTimer = invincibilityDuration;
            player.starIsOn = true;

            m_Pickup.PlayPickupFeedback();
            player.audioSource.PlayOneShot(player.starMusic);

            Destroy(gameObject);
        }
    }
}
