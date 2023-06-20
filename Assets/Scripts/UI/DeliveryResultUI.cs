using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconsImage;
    [SerializeField] private TMP_Text messageText;

    [Space(10)]
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite succesSprite;
    [SerializeField] private Sprite failedSprite;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += OnRecipeFailed;

        gameObject.SetActive(false);
    }

    private void OnRecipeFailed()
    {
        gameObject.SetActive(true);
        backgroundImage.color = failedColor;
        iconsImage.sprite = failedSprite;
        messageText.text = "DELIVER\nFAILED";
    }

    private void OnRecipeSuccess()
    {
        gameObject.SetActive(true);
        backgroundImage.color = successColor;
        iconsImage.sprite = succesSprite;
        messageText.text = "DELIVER\nSUCCESS";
    }
}
