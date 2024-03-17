using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int currentHealth, maxHealth;

    public float invinvibleLength = 2f;
    private float invincibleCounter;

    public Sprite[] healthBarImages;

    public int soundToPlay;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetHealt();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime; 


            ///Crea el efecto de flasheo del player cuando se activa el Hurt
            for(int i = 0; i < PlayerControler.instance.playerPieces.Length; i++)
            {
                if(Mathf.Floor(invincibleCounter * 5f) % 2 == 0)
                {
                    PlayerControler.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerControler.instance.playerPieces[i].SetActive(false);
                }

                if (invincibleCounter <= 0)
                {
                    PlayerControler.instance.playerPieces[i].SetActive(true);
                }
            }
        }
    }

    public void Hurt()
    {
        if(invincibleCounter <= 0)
        {
            currentHealth--;
            
            AudioManager.instance.PlaySFX(soundToPlay);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerControler.instance.KnockBack();

                invincibleCounter = invinvibleLength;
            }
        }

        UpdateUI();

    }

    public void ResetHealt()
    {
        currentHealth = maxHealth;
        UIManager.instance.healthImage.enabled = true;
        UpdateUI();
    }

    public void AddHealth(int amountToHeal)
    {
        currentHealth += amountToHeal;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.instance.healtText.text = currentHealth.ToString();

        switch (currentHealth)
        {
            case 5: UIManager.instance.healthImage.sprite = healthBarImages[4];
                break;
            case 4:
                UIManager.instance.healthImage.sprite = healthBarImages[3];
                break;
            case 3:
                UIManager.instance.healthImage.sprite = healthBarImages[2];
                break;
            case 2:
                UIManager.instance.healthImage.sprite = healthBarImages[1];
                break;
            case 1:
                UIManager.instance.healthImage.sprite = healthBarImages[0];
                break;
            case 0:
                UIManager.instance.healthImage.enabled = false;
                break;
        }
    }
}
