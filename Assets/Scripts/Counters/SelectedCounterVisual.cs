using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObject;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(BaseCounter obj)
    {
        if(obj == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach(GameObject visualGameObject in visualGameObject)
        {
            visualGameObject.SetActive(true);  
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObject)
        {
            visualGameObject.SetActive(false);
        }
    }
}
