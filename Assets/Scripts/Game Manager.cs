using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 respawnPosition;

    public GameObject deathEffect;

    public int currentCoins;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; //Implementa el Mouse. En este caso quita el Mouse de la pantalla.
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition  = PlayerControler.instance.transform.position;

        AddCoins(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
   
        }

        
    }

   public void Respawn()
       {
        StartCoroutine(RespawnWaiter());
    }

    public IEnumerator RespawnWaiter()
    {
        
        PlayerControler.instance.gameObject.SetActive(false);

        Instantiate(deathEffect, PlayerControler.instance.transform.position + new Vector3(0f, 1f, 0f), PlayerControler.instance.transform.rotation);

        yield return new WaitForSeconds(1f);

        CameraController.instance.cmBrain.enabled = false;

        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);

        HealthManager.instance.ResetHealt();

        UIManager.instance.fadeFromBlack = true;

        PlayerControler.instance.transform.position = respawnPosition;

        CameraController.instance.cmBrain.enabled = true;

        PlayerControler.instance.gameObject.SetActive(true);
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        respawnPosition = newSpawnPoint;
        Debug.Log("Spawn Point");
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.instance.coinText.text = "" + currentCoins;
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;

            Cursor.visible = false; //Implementa el Mouse. En este caso quita el Mouse de la pantalla.
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;

            Cursor.visible = true; //Implementa el Mouse. En este caso activa el Mouse de la pantalla.
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
