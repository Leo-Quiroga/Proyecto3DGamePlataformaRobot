using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    //public GameObject coinPickUp;

    public int valueCoin;

    public GameObject coinParticles;

    public int soundToPlay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            GameManager.instance.AddCoins(valueCoin);
            Instantiate(coinParticles, transform.position, transform.rotation);

            AudioManager.instance.PlaySFX(soundToPlay);
        }
    }
}
