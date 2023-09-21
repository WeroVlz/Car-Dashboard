using UnityEngine;
using UnityEngine.UI;
using System.Threading;

/// <summary>
/// This class manages the start up procedure of the dashboard.
/// </summary>
public class StartUp : MonoBehaviour
{
    public Drivable Drivable;
    public Image screenOff;
    public Image[] symbols = new Image[16];
    private bool screenOnFlag = false;
	private float startDelay = 1.5f;
    void Start()
    {
        Drivable.UpdatedData += UpdatedData;
    }

    // Update is called once per frame

    void UpdatedData(object sender, Drivable.DrivableArgs e){
        
        if(e.Buttons[0]){
            screenOnFlag = true;
            screenOff.enabled = false;
            Invoke("TurnOffSymbols", startDelay);
        }
    }

    /// <summary>
    /// Getter of the screen flag.
    /// </summary>
    public bool getScreenOnFlag(){
        return screenOnFlag;
    }
    /// <summary>
    /// Turn all the car symbols after start up.
    /// </summary>
    private void TurnOffSymbols(){
        foreach(Image symbol in symbols){
            symbol.enabled = false;
        }
    }
}
