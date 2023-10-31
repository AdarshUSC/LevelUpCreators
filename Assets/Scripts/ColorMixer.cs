using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorMixer : MonoBehaviour
{
    public Button buttonRed;
    public Button buttonGreen;
    public Button mixArea;

    public Button buttonBlue;
    public Button reset;

    // public SpriteRenderer mixingArea;
    public SpriteRenderer player;
    public Color resultColor;
    //private Color selectedColor1 = GetComponent<Button> ().colors; // Initial color
    private List<Color> colorList = new List<Color>();
    private Color enemyColor;
    public bool reset_flag;
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
        buttonRed.onClick.AddListener(() => OnButtonClick(buttonRed));
        buttonGreen.onClick.AddListener(() => OnButtonClick(buttonGreen));
        mixArea.onClick.AddListener(() => OnButtonClick(mixArea));
        buttonBlue.onClick.AddListener(() => OnButtonClick(buttonBlue));
        reset.onClick.AddListener(OnResetClick);
        reset_flag = true;
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
        Debug.Log("clicked button is "+ clicked);
        Color buttonColor = clicked.GetComponent<Image>().color;
        if(clicked.name=="mixArea"){
            GameObject[] colorButtons = GameObject.FindGameObjectsWithTag("ColorButton");
            foreach(GameObject colorButton in colorButtons){
                Button button = colorButton.GetComponent<Button>();
                if(button.interactable==false){
                    Color curr = button.GetComponent<Image>().color;
                    if(curr==Color.red){
                        Player.redCollected--;
                    } else if(curr==Color.blue){
                        Player.blueCollected--;
                    } else if(curr==Color.green){
                        Player.greenCollected--;
                    }
                }
            }
            player.color = mixArea.GetComponent<Image>().color;
        }
        colorList.Add(buttonColor);
        UpdateOutputColor();
        //clicked.GetComponent<Image>().color *= new Color((float)0.78,(float)0.78,(float)0.78,(float)0.5);            
        
    }
    public void OnResetClick()
    {
        if(Player.redCollected>0){
            buttonRed.interactable = true;
        } if(Player.blueCollected>0){
            buttonBlue.interactable = true;
        } if(Player.greenCollected>0){
            buttonGreen.interactable = true;
        } 
        mixArea.GetComponent<Image>().color = new Color(0,0,0,0);
        colorList.Clear();
        UpdateOutputColor();
        reset_flag = true;
        
    }

    private void UpdateOutputColor()
    {
        Color[] selectedColors = colorList.ToArray();
        resultColor = new Color(1,1,1,1);
        if(selectedColors.Length>0){
            resultColor = CombineColors(selectedColors);
        }
        if (reset_flag)
        {
            Player.camouflage++;
            reset_flag = false;
        }
        Debug.Log("Camouflage count" + Player.camouflage);
            
        mixArea.GetComponent<Image>().color = resultColor;
        // player.color = resultColor;
    }
    private void Win(){
        if(player.color.Equals(enemyColor)){
            //this equals might not work as expected due to the color calculation? try use the same way to express color RGB value in this script and emenySpawner.
            //enemy pass by without huring player ship.
        }
    }
}
