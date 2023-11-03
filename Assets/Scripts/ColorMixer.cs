using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorMixer : MonoBehaviour
{
    public Button buttonRed;
    public Button buttonGreen;
    public Button mixArea;
    public Button buttonBlue;
    public Button reset;
    private Color originalColor;

    // public SpriteRenderer mixingArea;
    public SpriteRenderer player;
    public Color resultColor;
    //private Color selectedColor1 = GetComponent<Button> ().colors; // Initial color
    public static List<Color> colorList = new List<Color>();
    private Color enemyColor;
    public bool reset_flag;

    //TODO: deactivate color panel for 5 seconds if blue/green is on so that player can't use two powers at once.
    public bool isRedOn; //no need to deactivate as it is not timed.
    public bool isBlueOn;
    public bool isGreenOn;
    // Start is called before the first frame update
    private void Start()
    {
        buttonRed.onClick.AddListener(() => OnButtonClick(buttonRed));
        buttonGreen.onClick.AddListener(() => OnButtonClick(buttonGreen));
        mixArea.onClick.AddListener(() => OnButtonClick(mixArea));
        buttonBlue.onClick.AddListener(() => OnButtonClick(buttonBlue));
        reset.onClick.AddListener(OnResetClick);
        reset_flag = true;
        mixArea.interactable=false;
        buttonRed.interactable=Player.redCollected==0?false:true;
        buttonGreen.interactable=Player.greenCollected==0?false:true;
        buttonBlue.interactable=Player.blueCollected==0?false:true;
        buttonRed.GetComponentInChildren<TMP_Text>().text = Player.redCollected.ToString();
        buttonGreen.GetComponentInChildren<TMP_Text>().text = Player.greenCollected.ToString();
        buttonBlue.GetComponentInChildren<TMP_Text>().text = Player.blueCollected.ToString();
    }

    private void Update(){

            buttonRed.interactable=Player.redCollected==0?false:true;
            buttonGreen.interactable=Player.greenCollected==0?false:true;
            buttonBlue.interactable=Player.blueCollected==0?false:true;
            buttonRed.GetComponentInChildren<TMP_Text>().text = Player.redCollected.ToString();
            buttonGreen.GetComponentInChildren<TMP_Text>().text = Player.greenCollected.ToString();
            buttonBlue.GetComponentInChildren<TMP_Text>().text = Player.blueCollected.ToString();
    }
    private void OnButtonClick(Button clicked){

        if(!PairDoor1.byTheDoor && !PairDoor2.byTheDoor)
        {
            //just show one color in mixer
            mixArea.GetComponent<Image>().color = clicked.GetComponent<Image>().color;
        } else{ 
            //enable matching
            // clicked.interactable = false; //disable the button so player cannot click twice, wont wwork here
            Color mixAreaColor = mixArea.GetComponent<Image>().color ;
            Color clickedColor = clicked.GetComponent<Image>().color;
            // to avoid multiple additions of same color.
            // if(mixAreaColor.r!=clickedColor.r){
            //     mixAreaColor.r=clickedColor.r;
            // } if(mixAreaColor.g!=clickedColor.g){
            //     mixAreaColor.g=clickedColor.g;
            // } if(mixAreaColor.b!=clickedColor.b){
            //     mixAreaColor.b=clickedColor.b;
            // }
            Debug.Log("clicked button is "+ clicked);
            Color buttonColor = clicked.GetComponent<Image>().color;
            colorList.Add(buttonColor);
            UpdateOutputColor();
        }
        
        
    }
    //this function is for mixing
    // private void OnButtonClick(Button clicked)
    // {   
    //     clicked.interactable = false; //disable the button so player cannot click twice
    //     Debug.Log("clicked button is "+ clicked);
    //     Color buttonColor = clicked.GetComponent<Image>().color;
    //     if(clicked.name=="mixArea"){
    //         GameObject[] colorButtons = GameObject.FindGameObjectsWithTag("ColorButton");
    //         foreach(GameObject colorButton in colorButtons){
    //             Button button = colorButton.GetComponent<Button>();
    //             if(button.interactable==false){
    //                 Color curr = button.GetComponent<Image>().color;
    //                 if(curr==Color.red){
    //                     Player.redCollected--;
    //                 } if(curr==Color.blue){
    //                     Player.blueCollected--;
    //                 } if(curr==Color.green){
    //                     Player.greenCollected--;
    //                 }
    //             }
    //         }
    //         // player.color = mixArea.GetComponent<Image>().color;
    //     }
    //     colorList.Add(buttonColor);
    //     UpdateOutputColor();
    //     //clicked.GetComponent<Image>().color *= new Color((float)0.78,(float)0.78,(float)0.78,(float)0.5);             
    // }

    //resetForMixing
    public void OnResetClick()
    {
        // maybe at start disable/HIDE it, only use it for mixing.
        // if(Player.redCollected>0){
        //     buttonRed.interactable = true;
        // } if(Player.blueCollected>0){
        //     buttonBlue.interactable = true;
        // } if(Player.greenCollected>0){
        //     buttonGreen.interactable = true;
        // } 
        mixArea.GetComponent<Image>().color = new Color(1,1,1,1);
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
           // Player.current_mechs.Add("Camouflage");
            reset_flag = false;
        }
        Debug.Log("Camouflage count" + Player.camouflage);
        mixArea.GetComponent<Image>().color = resultColor;
        player.color = resultColor;

    }

    public static Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0,0,0,0);
        foreach(Color c in aColors)
        {
            result += c;
        }
        // result /= aColors.Length;
        result.a = 1;
        // to avoid multiple additions of same color.
        if(result.r>1){
            result.r=1;
        } if(result.g>1){
            result.g=1;
        } if(result.b>1){
            result.b=1;
        }
        Debug.Log("the resultant color is "+ result);
        return result;
    }

}
