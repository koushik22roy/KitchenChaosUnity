using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestingCodeUI : MonoBehaviour
{
    [SerializeField] private Button clientButton, hostButton;

    void Awake()
    {
        clientButton.onClick.AddListener(() =>
        {
            Debug.Log("Start Client");
            NetworkManager.Singleton.StartClient();
            Hide();
        });


        hostButton.onClick.AddListener(() =>
        {
            Debug.Log("Start Host");
            NetworkManager.Singleton.StartHost();
            Hide();
        });
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
