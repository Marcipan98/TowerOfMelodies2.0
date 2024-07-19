using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Declaring the sliders
    public Slider timerSlider1;
    public Slider timerSlider2;
    public Slider timerSlider3;
    public Slider timerSlider4;

    // Max timers for each slider
    private float sliderMaxTime1 = 2.0f; // 3.5 - 4
    private float sliderMaxTime2 = 3.0f; // 2.5 - 3
    private float sliderMaxTime3 = 2.5f; // 1.5 - 2
    private float sliderMaxTime4 = 1.0f; // 0.5 - 1

    private bool runSlider1;
    private bool runSlider2;
    private bool runSlider3;
    private bool runSlider4;

    private float userThreshold = 0.5f;

    private bool userPressedKey;
    private float userPressedKeyTime;
    private bool userFailed;

    private bool wonSlider1;
    private bool wonSlider2;
    private bool wonSlider3;
    private bool wonSlider4;

    void Start()
    {
        // Start with the user not pressing anything
        userPressedKey = false;
        userFailed = false;

        // All sliders are stopped in the beginning 
        runSlider1 = true;
        runSlider2 = false;
        runSlider3 = false;
        runSlider4 = false;

        // Setting the max and starting value for each slider
        timerSlider1.maxValue = sliderMaxTime1;
        timerSlider1.value = 0;

        timerSlider2.maxValue = sliderMaxTime2;
        timerSlider2.value = 0;

        timerSlider3.maxValue = sliderMaxTime3;
        timerSlider3.value = 0;

        timerSlider4.maxValue = sliderMaxTime4;
        timerSlider4.value = 0;
    }

    bool isUserBetweenThresholds(float pressedKeyTime, float sliderMaxTime) {
        bool isAboveMinValue = pressedKeyTime >= (sliderMaxTime - userThreshold); 
        bool isBelowMaxValue = pressedKeyTime <= sliderMaxTime;

        return isAboveMinValue && isBelowMaxValue;
    }

// Pass slider number and current time to stop at threshold start point 
    void stopSliderAtThreshold(int slider, float currentTime) {
        switch (slider) {
            case 1:
                if (currentTime >= sliderMaxTime1 - userThreshold) {
                    userFailed = true;
                    print("Stopped at slider 1's threshold.");
                }
                break;
            case 2:
                if (currentTime >= sliderMaxTime1 + sliderMaxTime2 - userThreshold) {
                    userFailed = true;
                    print("Stopped at slider 2's threshold.");
                }
                break;
            case 3:
                if (currentTime >= sliderMaxTime1 + sliderMaxTime2 + sliderMaxTime3 - userThreshold) {
                    userFailed = true;
                    print("Stopped at slider 3's threshold.");
                }
                break;
            case 4:
                if (currentTime >= sliderMaxTime1 + sliderMaxTime2 + sliderMaxTime3 + sliderMaxTime4 - userThreshold) {
                    userFailed = true;
                    print("Stopped at slider 4's threshold.");
                }
                break;
        }
    }

    void Update()
    {

        float currentTime = Time.time;

        if (userFailed) {
            print("You lost");
            // Change here the scene to showing that the player FAILED.
            return;
        }

        if (wonSlider1 && wonSlider2 && wonSlider3 && wonSlider4) {
            print("You won!");
            // Change here the scene to showing that the player WON.
        }

        // FOR TESTING THRESHOLDS. DELETE AFTER (numbers from 1 to 4)
        // Play up until that slider number, and then let it fail automatically
        // stopSliderAtThreshold(4, currentTime); // uncomment line to test threshold

        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space)) {
            userPressedKey = true;
            userPressedKeyTime = currentTime;
        }

        if (currentTime > sliderMaxTime1) { 
            if (!userPressedKey && runSlider1 && !wonSlider1) {
                print("User missed the threshold on slider 1.");
                userFailed = true;
                return;
            }

            // Stop 1st slider and start 2nd one
            runSlider1 = false;
            runSlider2 = true;
         }

        if (currentTime > (sliderMaxTime1 + sliderMaxTime2)) {
            if (!userPressedKey && runSlider2 && !wonSlider2) {
                print("User missed the threshold on slider 2.");
                userFailed = true;
                return;
            }

            // Stop 2nd slider and start 3rd one
            runSlider2 = false;
            runSlider3 = true;
        }

        if (currentTime > (sliderMaxTime1 + sliderMaxTime2 + sliderMaxTime3)){
            if (!userPressedKey && runSlider3 && !wonSlider3) {
                print("User missed the threshold on slider 3.");
                userFailed = true;
                return;
            }

            // Stop 3rd slider and start 4th one  
            runSlider3 = false;
            runSlider4 = true;
        }
        
        if (currentTime > (sliderMaxTime1 + sliderMaxTime2 + sliderMaxTime3 + sliderMaxTime4)) {
            if (!userPressedKey && runSlider4 && !wonSlider4) {
                print("User missed the threshold on slider 4.");
                userFailed = true;
                return;
            }

            runSlider4 = false;
        }

        // Set the current time for each of the sliders 
        if (runSlider1) {    
            timerSlider1.value = currentTime;
        }

        if (runSlider2) {
            timerSlider2.value = currentTime - sliderMaxTime1;
        }

        if (runSlider3) {
            timerSlider3.value = currentTime - sliderMaxTime1 - sliderMaxTime2;
        }

        if (runSlider4) {
            timerSlider4.value = currentTime - sliderMaxTime1 - sliderMaxTime2 - sliderMaxTime3;
        }

        // Check slider thresholds if user presses key and they didn't fail already
        if (userPressedKey && !userFailed) {
            // Check if user is between threshold depending on the current running slider
            if (runSlider1) {
                if(isUserBetweenThresholds(userPressedKeyTime, sliderMaxTime1)) {
                    wonSlider1 = true;
                    userPressedKey = false;
                    SoundEffectManager.Play("Twinkle");
                    print("Player WON on Slider 1");
                } else {
                    print("Player FAILED on Slider 1");
                    userFailed = true;
                }
            } else if (runSlider2) {
                if(isUserBetweenThresholds(userPressedKeyTime - sliderMaxTime1, sliderMaxTime2)) {
                    wonSlider2 = true;
                    userPressedKey = false;
                    SoundEffectManager.Play("Twinkle");
                    print("Player WON on Slider 2");
                } else {
                    userFailed = true;
                    print("Player FAILED on Slider 2");
                }
            } else if (runSlider3) {
                if(isUserBetweenThresholds(userPressedKeyTime - sliderMaxTime1 - sliderMaxTime2, sliderMaxTime3)) {
                    wonSlider3 = true;
                    userPressedKey = false;
                    SoundEffectManager.Play("Twinkle");
                    print("Player WON on Slider 3");
                } else {
                    print("Player FAILED on Slider 3");
                    userFailed = true;
                }
            } else if (runSlider4) {
                if(isUserBetweenThresholds(userPressedKeyTime - sliderMaxTime1 - sliderMaxTime2 - sliderMaxTime3, sliderMaxTime4)) {
                    wonSlider4 = true;
                    userPressedKey = false;
                    SoundEffectManager.Play("Twinkle");
                    print("Player WON on Slider 4");
                } else {
                    userFailed = true;
                    print("Player FAILED on Slider 4");

                }
            }
        }

    }
}

