using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPowerPickup : MonoBehaviour
{
    Pickup m_Pickup;
    public float timerLength = 10f;
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
        if (player && !player.starIsOn)
        {
            player.starIsOn = true;
            player.starTimer = timerLength;
            player.audioSource.PlayOneShot(player.starMusic);
            m_Pickup.PlayPickupFeedback();
            Destroy(gameObject);
        }
    }
}
