using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenMenu : MonoBehaviour {
    public Button walkButton;


    public void openMenu() {
        walkButton.gameObject.SetActive(!walkButton.gameObject.active);
    }
}
