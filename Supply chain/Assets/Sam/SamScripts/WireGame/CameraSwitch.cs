using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    public Camera playerCamera,WireCamera;
    public GameObject playerPos;
    private GameObject player;
    public static CameraSwitch instance;
    public Transform resetPosition;

     void Start()
    {
        instance = this;
        WireCamera.enabled = false;
        player = GlobalScript.playerRef.gameObject;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerCamera.enabled = false;
            WireCamera.enabled = true;
            player.GetComponent<Rigidbody>().isKinematic = true;
            GlobalScript.playerRef.weaponPosition.gameObject.SetActive(false);
        }
        
    }
    void LateUpdate()
    {
        if(Input.GetKeyUp(KeyCode.G) )
        {
          GetOutOfWireBox();
        }
    }
    public void GetOutOfWireBox()
    {
        Debug.Log(WireCamera.gameObject);
        WireCamera.enabled = false;
        Debug.Log(playerCamera.gameObject);
        playerCamera.enabled = true;
         player.GetComponent<Rigidbody>().isKinematic = false;
        playerPos.transform.position = resetPosition.position;  
        GlobalScript.playerRef.weaponPosition.gameObject.SetActive(true);  
    }

}
