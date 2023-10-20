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
        text.text = "Press Tab - Reflection, 'B' - Boomerang<br>Hint!! -- Change your color to save yourself from the bullets.";
    }

}
