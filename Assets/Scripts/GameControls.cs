using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameControls : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Press <b>Tab<b> <b>B<b> - Boomerang<br>Hint!! -- Change the color to save yourself from the bullets.";
    }

}
