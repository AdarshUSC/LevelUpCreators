using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorMixer : MonoBehaviour
{
    public Button buttonRed;

    [SerializeField] private TMP_Text redText;
    [SerializeField] private TMP_Text greenText;
    [SerializeField] private TMP_Text blueText;
    public Button buttonGreen;
    public Button mixArea;
    SpriteRenderer spriteRenderer;

    public Button buttonBlue;
    public Button reset;
    public SpriteRenderer boomerang;
    private Color originalColor;

    // public SpriteRenderer mixingArea;
    public SpriteRenderer player;
    public Color resultColor;
    //private Color selectedColor1 = GetComponent<Button> ().colors; // Initial color
    private List<Color> colorList = new List<Color>();
    private Color enemyColor;
    public bool reset_flag;

    //TODO: deactivate color panel for 5 seconds if blue/green is on so that player can't use two powers at once.
    public bool isRedOn; //no need to deactivate as it is not timed.
    public bool isBlueOn;
    public bool isGreenOn;
    public static Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0,0,0,0);
        foreach(Color c in aColors)
        {
            result += c;
        }
        // result /= aColors.Length;
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
        mixArea.interactable=false;
        // buttonRed.interactable=false;
        // buttonGreen.interactable=false;
        // buttonBlue.interactable=false;
        buttonRed.GetComponentInChildren<TMP_Text>().text = Player.redCollected.ToString();
        // Debug.Log("red count is "+buttonRed.GetComponentInChildren<TMP_Text>().text);
        buttonGreen.GetComponentInChildren<TMP_Text>().text = Player.greenCollected.ToString();
        buttonBlue.GetComponentInChildren<TMP_Text>().text = Player.blueCollected.ToString();
        originalColor = boomerang.color;
        Debug.Log("Original color" + originalColor);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnButtonClick(Button clicked)
    {   
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
                        if(Player.redCollected==0){
                            buttonRed.interactable=false;
                        }
                    } if(curr==Color.blue){
                        Player.blueCollected--;
                        if(Player.blueCollected==0){
                            buttonBlue.interactable=false;
                        }
                    } if(curr==Color.green){
                        Player.greenCollected--;
                        if(Player.greenCollected==0){
                            buttonGreen.interactable=false;
                        }
                    }
                }
            }
            mixArea.interactable=false;
            // player.color = mixArea.GetComponent<Image>().color;
        } else{
            mixArea.interactable=true;
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
        mixArea.interactable=false;
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
            Player.current_mechs.Add("Camouflage");
            reset_flag = false;
        }
        Debug.Log("Camouflage count" + Player.camouflage);
        mixArea.GetComponent<Image>().color = resultColor;
        boomerang.color = resultColor;
        // player.color = resultColor;
    }
}
