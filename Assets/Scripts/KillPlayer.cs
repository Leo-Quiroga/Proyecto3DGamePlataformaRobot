using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            HealthManager.instance.currentHealth = 0;
            HealthManager.instance.UpdateUI();
            GameManager.instance.Respawn();
        }
    }
}
