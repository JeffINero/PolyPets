using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkUpdate : MonoBehaviour {

    public Text walkText;

    public float oldDistance = 0;

    void OnEnable()
    {
        oldDistance = 0;
    }

    void Update()
    {
            oldDistance += 0.00005f;
            walkText.text = "You walked: " + oldDistance.ToString("0.00") + "km";
    }
}
