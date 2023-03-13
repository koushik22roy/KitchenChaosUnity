using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start()
    {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChnaged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void CuttingCounter_OnProgressChnaged(CuttingCounter.OnProgressChangedEvent obj)
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
