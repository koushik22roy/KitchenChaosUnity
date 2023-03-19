using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgressGameObject;

    private IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if(hasProgress == null)
        {
            Debug.LogError("GameObject " + hasProgressGameObject + " does not have a component that implement interface IHasProgress");
        }

        hasProgress.OnProgressChanged += HasProgress_OnProgressChnaged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgress_OnProgressChnaged(IHasProgress.OnProgressChangedEvent obj)
    {
        barImage.fillAmount = obj.progressNormalized;

        if(obj.progressNormalized==0f || obj.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
