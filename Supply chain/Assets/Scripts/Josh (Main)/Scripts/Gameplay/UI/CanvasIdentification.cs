using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Requirements:

// Player's camera

abstract public class CanvasIdentification : MonoBehaviour
{
    Animator anim;
    CanvasGroup canvasGroup;
    bool hasBeenSeen;
    Vector3 playerDir;

    Camera playerCam;

    [HideInInspector] public GameObject canvas;
    string identityName;
    Sprite identityTypeSprite;

    [Header("(Text) Element Detail Holder")]
    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] Image spriteIdentityImage;
    [Space]
    [Space]

    [Header("Distance Value Upon Identity Appearance")]
    [SerializeField] float MaxUIDistance;
    [SerializeField] float MinUIDistance;

    virtual public void SetParameters()
    {
        anim = (canvas = gameObject).GetComponent<Animator>();
        (canvasGroup = canvas.GetComponent<CanvasGroup>()).alpha = 0;
        playerCam = FindObjectOfType<PlayerMovement>().GetComponentInChildren<Camera>();
    }

    public void CanvasParameters(string Name, Sprite Type)
    {
        identityName = Name.ToUpper();
        identityTypeSprite = Type;
        textName.text = identityName;
        spriteIdentityImage.sprite = identityTypeSprite;
    }

    public void PlayerDetection()
    {
        if (playerCam)
        {
            float dist = Vector3.Distance(transform.position, playerCam.transform.position);

            // Player is a set distance from/to element?
            if (dist <= MaxUIDistance && dist! > MinUIDistance)
            {
                canvasGroup.alpha = 1;
                if (!anim.enabled) anim.enabled = true;

                if (!hasBeenSeen && anim.GetInteger("Spotted") != 1) anim.SetInteger("Spotted", 1);
                else if (anim.GetInteger("Spotted") != 2) anim.SetInteger("Spotted", 2);

                playerDir = playerCam.transform.forward;
                playerDir.y = 0;

                canvas.transform.rotation = Quaternion.LookRotation(playerDir);

                if (!hasBeenSeen) hasBeenSeen = true;
            }
            else
            {
                if (canvas.activeSelf && anim.GetInteger("Spotted") != 0) anim.SetInteger("Spotted", 0);
                if (canvasGroup.alpha <= 0 && anim.enabled) anim.enabled = false;
            }
        }
    }
}