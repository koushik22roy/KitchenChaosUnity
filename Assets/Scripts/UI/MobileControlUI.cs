using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileControlUI : MonoBehaviour
{
    [SerializeField] Button cutButton;
    [SerializeField] Button interactButton;
    private void Awake()
    {
        cutButton.onClick.AddListener(() =>
        {
            // Player.Instance.Input_ItemsCut();
        });

        interactButton.onClick.AddListener(() =>
        {
            // Player.Instance.Input_ItemInteract();
        });
    }
}
