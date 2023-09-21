using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class manages the lights and the inputs in order to enabled them.
/// </summary>
public class Lights : MonoBehaviour
{
    public Drivable Drivable;
    public StartUp StartUp;
    void Start()
    {
        Drivable.UpdatedData += UpdatedData;
    }

    // Update is called once per frame
    void UpdatedData(object sender, Drivable.DrivableArgs e)
    {
        if(StartUp.getScreenOnFlag()){
            if(e.Buttons[11]){
                StartUp.symbols[13].enabled = !StartUp.symbols[13].enabled;
            }
            if(e.Buttons[9]){
                StartUp.symbols[12].enabled = !StartUp.symbols[12].enabled;
                if(StartUp.symbols[11].enabled) StartUp.symbols[11].enabled = !StartUp.symbols[11].enabled;
            }
            if(e.Buttons[8]){
                StartUp.symbols[11].enabled = !StartUp.symbols[11].enabled;
                if(StartUp.symbols[12].enabled) StartUp.symbols[12].enabled = !StartUp.symbols[12].enabled;
            }
        }

    }
}
