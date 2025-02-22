using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

public class Player : MonoBehaviour
{
    [HideInInspector] public int huntCount = 0;
    [HideInInspector] public int burgerCount = 0;
    [HideInInspector] public int gold = 0;
    [HideInInspector] public List<GameObject> burgers;

    public Button sellButton;
    public Button okButton;
    public Button cancelButton;
    public GameObject checkImage;
    public GameObject noBurgerImage;
    public TextMeshProUGUI huntText;
    public TextMeshProUGUI burgerText;
    public TextMeshProUGUI goldText;
    public XROrigin xrOrigin; 
    public CharacterController characterController;

    private void Awake()
    {
        okButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        checkImage.SetActive(false);
        noBurgerImage.SetActive(false);
        sellButton.onClick.AddListener(SellButtonClick);
        okButton.onClick.AddListener(OkButtonClick);
        cancelButton.onClick.AddListener(CancelButtonClick);
    }

    private void SellButtonClick()
    {
        okButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
        checkImage.SetActive(true);
    }

    private void OkButtonClick()
    {
        if (burgerCount > 0)
        {
            okButton.gameObject.SetActive(false);
            checkImage.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            burgerCount--;
            gold += 500;

            if(burgers.Count > 0)
            {
                GameObject removeBurger = burgers[burgers.Count - 1];
                burgers.RemoveAt(burgers.Count - 1);
                Destroy(removeBurger);
            }
        }
        else
        {
            noBurgerImage.SetActive(true);
            StartCoroutine(NoBurgerRoutine());
            okButton.gameObject.SetActive(false);
            checkImage.SetActive(false);
            cancelButton.gameObject.SetActive(false);
        }
    }

    IEnumerator NoBurgerRoutine()
    {
        yield return new WaitForSeconds(3f);
        noBurgerImage.SetActive(false);
    }

    private void CancelButtonClick()
    {
        checkImage.SetActive(false);
        okButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        huntText.text = huntCount.ToString();
        burgerText.text = burgerCount.ToString();
        goldText.text = $"Gold\n{gold}".ToString();
    }
    private void Start()
    {
        if (xrOrigin == null)
        {
            xrOrigin = FindObjectOfType<XROrigin>();
        }

        if (characterController == null)
        {
            characterController = xrOrigin.GetComponent<CharacterController>();
        }
    }

    public void AdjustHeightAfterTeleport()
    { 
        if (xrOrigin != null && characterController != null)
        {
            float cameraHeight = xrOrigin.CameraInOriginSpaceHeight;

            characterController.height = cameraHeight;
            characterController.center = new Vector3(0, cameraHeight / 2, 0);

            Vector3 adjustedPosition = xrOrigin.transform.position;
            adjustedPosition.y += cameraHeight / 2;
            xrOrigin.transform.position = adjustedPosition;
        }
    }
}


