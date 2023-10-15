using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMixer : MonoBehaviour
{
    public Button button0;
    public Button button1;
    public Button button2;
    public Button button3;
    public Button reset;

    public SpriteRenderer mixingArea;
    public SpriteRenderer player;
    public Color resultColor;
    //private Color selectedColor1 = GetComponent<Button> ().colors; // Initial color
    private List<Color> colorList = new List<Color>();
    private Color enemyColor;
    public static Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0,0,0,0);
        foreach(Color c in aColors)
        {
            result += c;
        }
        result /= aColors.Length;
        return result;
    }

    // Start is called before the first frame update
    private void Start()
    {
        button0.onClick.AddListener(() => OnButtonClick(button0));
        button1.onClick.AddListener(() => OnButtonClick(button1));
        button2.onClick.AddListener(() => OnButtonClick(button2));
        button3.onClick.AddListener(() => OnButtonClick(button3));
        reset.onClick.AddListener(OnResetClick);
    }

    //private void Update()
    //{
    //    if(FindObjectOfType<SelectEnemy>()==null)
    //    {
    //        Debug.Log("enemy not initialized yet");
    //    }else
    //    {
    //        enemyColor = FindObjectOfType<SelectEnemy>().enemyColor;
    //        Debug.Log("enemy color is:" + enemyColor);
    //    }
    //}

    private void OnButtonClick(Button clicked)
    {   
        //buttonSelected = !buttonSelected;
        clicked.interactable = false; //disable the button so player cannot click twice
        Color buttonColor = clicked.GetComponent<Image>().color;
        colorList.Add(buttonColor);
        UpdateOutputColor();
        //clicked.GetComponent<Image>().color *= new Color((float)0.78,(float)0.78,(float)0.78,(float)0.5);            
        
    }
    public void OnResetClick()
    {
        button0.interactable = true;
        button1.interactable = true;
        button2.interactable = true;
        button3.interactable = true;
        mixingArea.color = new Color(0,0,0,0);
        colorList.Clear();
        UpdateOutputColor();
        
    }

    private void UpdateOutputColor()
    {
        Color[] selectedColors = colorList.ToArray();
        resultColor = new Color(1,1,1,1);
        if(selectedColors.Length>0){
            resultColor = CombineColors(selectedColors);
        }
            
        mixingArea.color = resultColor;
        player.color = resultColor;
    }
    private void Win(){
        if(player.color.Equals(enemyColor)){
            //this equals might not work as expected due to the color calculation? try use the same way to express color RGB value in this script and emenySpawner.
            //enemy pass by without huring player ship.
        }
    }
}
