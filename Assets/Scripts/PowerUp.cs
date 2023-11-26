using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    public Image powerUpTimerImage; // Radial image for the power-up timer
    public static float powerUpDuration = 10.0f; // Duration of the power-up
    GameObject player;
    public static float powerUpTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        powerUpTimer = 0;

        if (powerUpTimerImage != null)
        {
            powerUpTimerImage.fillAmount = 0;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Player.isPowerUpOn)
        {
            powerUpTimer += Time.deltaTime;
            if (powerUpTimerImage != null)
            {
                powerUpTimerImage.fillAmount = (powerUpDuration - powerUpTimer) / powerUpDuration;
            }
            if (powerUpTimer > powerUpDuration)
            {
                DeactivatePowerUp();
            }
        }

    }

    public void ActivatePowerUp()
    {
        if (!Player.isPowerUpOn)
        {
            Player.isPowerUpOn = true;
            Player.playerMoveSpeed += 4.0f;
            powerUpTimer = 0;
            Player.powerup++;
            Player.current_mechs.Add("Powerup");

            if (powerUpTimerImage != null)
            {
                powerUpTimerImage.fillAmount = 0;
            }
        }
    }

    public void DeactivatePowerUp()
    {
        Player.isPowerUpOn = false;
        Player.playerMoveSpeed -= 4.0f;
    }


}
