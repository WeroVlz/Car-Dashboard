using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class manages the turn signals behaviour.
/// </summary>
public class TurnSignals : MonoBehaviour
{

    public Drivable Drivable;  
    public StartUp StartUp; 
    public Image leftBlinker;
    public Image rightBlinker;
    public float interval = 0.4f;
	public float startDelay = 0.1f;
	private bool defaultState = false;
    private bool isHazardBlinking = false;
    bool[] isBlinking = new bool[2];
    
    void Start(){
        Drivable.UpdatedData += UpdatedData;

        rightBlinker.enabled = defaultState;
        leftBlinker.enabled = defaultState;
    }
    /// <summary>
    /// Activates left blinker. If right blinker is on it turns it off. If hazard lights are on it is ignored.
    /// </summary>
    public void ActivateLeftBlinker(){
        if (isBlinking[0]){
            CancelInvoke();
            leftBlinker.enabled = defaultState;
            isBlinking[0] = false;
            return;
        }

		if (leftBlinker !=null){}
		{   
            if(isBlinking[1]){
                CancelInvoke();
                rightBlinker.enabled = defaultState;
                isBlinking[1] = false;
            }   
            if(isHazardBlinking == false){
    			isBlinking[0] = true;
			    InvokeRepeating("ToggleBlinkerState", startDelay, interval);
            }
		}
    }

    /// <summary>
    /// Activates right blinker. If left blinker is on it turns it off. If hazard lights are on it is ignored.
    /// </summary>
    public void ActivateRightBlinker(){
        
        if (isBlinking[1]){
            CancelInvoke();
            rightBlinker.enabled = defaultState;
            isBlinking[1] = false;
            return;
        }

        if (rightBlinker !=null){}
		{
            if(isBlinking[0]){
                CancelInvoke();
                leftBlinker.enabled = defaultState;
                isBlinking[0] = false;
            }   
            if(isHazardBlinking == false){
    			isBlinking[1] = true;
			    InvokeRepeating("ToggleBlinkerState", startDelay, interval);
            }
		}
    }

    /// <summary>
    /// Activates hazard lights. Ignores both blinkers if activated.
    /// </summary>
    public void ActivateHazardLights(){
        if (isHazardBlinking){
            CancelInvoke();
            leftBlinker.enabled = defaultState;
            rightBlinker.enabled = defaultState;
            isHazardBlinking = false;
            return;
        }

        if(leftBlinker != null || rightBlinker != null){
            CancelInvoke();
            leftBlinker.enabled = defaultState;
            rightBlinker.enabled = defaultState;
            isBlinking[0] = false;
            isBlinking[1] = false;
            isHazardBlinking = true;
            InvokeRepeating("ToggleHazardState", startDelay, interval);
        }
    }

    /// <summary>
    /// Changes blinker enabled state.
    /// </summary>
    void ToggleBlinkerState()
	{
        if(isBlinking[0])
		    leftBlinker.enabled = !leftBlinker.enabled;
        if(isBlinking[1])
            rightBlinker.enabled= !rightBlinker.enabled;
	}

    /// <summary>
    /// Changes both blinkers enabled state.
    /// </summary>
    void ToggleHazardState(){
        leftBlinker.enabled = !leftBlinker.enabled;
        rightBlinker.enabled = !rightBlinker.enabled;
        
    }
    
    void UpdatedData(object sender, Drivable.DrivableArgs e){
        if(StartUp.getScreenOnFlag()){
            if(e.Buttons[3]){
                ActivateHazardLights();
            }
            if(e.Buttons[4]){
                ActivateRightBlinker();
            }
            if(e.Buttons[5]){
                ActivateLeftBlinker();
            }
        }
    } 
}
