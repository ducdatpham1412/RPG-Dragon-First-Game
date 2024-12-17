using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUpMenu : MonoBehaviour {
    public CanvasGroup popUp;

    void Awake() {
        popUp = GetComponent<CanvasGroup>();
    }

    public void EnableTheMenu() {
        Debug.Log("Check show menu");
        popUp.alpha = 1;
        popUp.interactable = true;
        popUp.blocksRaycasts = true;
    }

    public void DisableTheMenu() {
        popUp.alpha = 0;
        popUp.interactable = false;
        popUp.blocksRaycasts = false;
    }
}
