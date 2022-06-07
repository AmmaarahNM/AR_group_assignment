using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ARTapToPlace ARscript;
    
    public Text displayText;
    public GameObject trackSelectionUI;
    
    public GameObject adjustedButton;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ARscript.placementIndicator.activeSelf == true)
        {
            displayText.text = "Tap the green circle to generate your track";
        }

        if (ARscript.objectGenerated)
        {
            if (!ARscript.trackAdjusted)
            {
                displayText.text = "Rotate and scale your track using two fingers";
                adjustedButton.SetActive(true);
            }

            else
            {
                displayText.text = "Place your eraser on the starting position";
                adjustedButton.SetActive(false);
            }
        }



    }

    public void TrackSelection(int track)
    {
        ARscript.trackSelected = true;
        ARscript.trackChosen = track - 1;
        displayText.text = "Move your phone over a flat surface";
        trackSelectionUI.SetActive(false);

        
    }

    
}
