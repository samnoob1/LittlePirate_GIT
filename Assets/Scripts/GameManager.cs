using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera mainCam;
    public Camera playerCam;

    public PlayerController playerControllerScript;
    public PlayerShipController playerShipControllerScript;

    public GameObject player;
    public GameObject playerShip;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = player.GetComponent<PlayerController>();
        playerShipControllerScript = playerShip.GetComponent<PlayerShipController>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            ChangePlayerState();
        }
    }

    public void ChangePlayerState() 
    {
        playerShipControllerScript.shipCanMove = !(playerShipControllerScript.shipCanMove);
        playerControllerScript.playerIsOnShip = !(playerControllerScript.playerIsOnShip);

        if (mainCam.gameObject.activeInHierarchy == true) 
        {
            mainCam.gameObject.SetActive(false);
            playerCam.gameObject.SetActive(true);
        }
        else
        {
            mainCam.gameObject.SetActive(true);
            playerCam.gameObject.SetActive(false);
        }
    }
}
