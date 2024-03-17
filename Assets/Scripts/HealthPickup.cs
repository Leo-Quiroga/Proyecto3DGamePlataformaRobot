using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healtAmount;
    public bool isFullHealth;

    public GameObject heartParticles;

    public int soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Player")
        {
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(soundToPlay);

            Instantiate(heartParticles, PlayerControler.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerControler.instance.transform.rotation);

            if (isFullHealth)
            {
                HealthManager.instance.ResetHealt();
            }
            else
            {
                HealthManager.instance.AddHealth(healtAmount);
            }
        }
    }
}
