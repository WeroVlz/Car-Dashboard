using UnityEngine;

public class EngineTest : MonoBehaviour
{
    public Drivable Drivable;
    // Start is called before the first frame update
    void Start()
    {
        Drivable.UpdatedData += UpdatedData;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdatedData(object sender, Drivable.DrivableArgs e)
    {
        //string data = "";
        int i = 1;
        foreach (bool button in e.Buttons)
        {
            if(button == true){
                //Debug.Log(data += "Button " + i + "[" + button + "] ");
            }
            i++;
        }
        //Debug.Log("Wheel [" + e.WheelRotation + "] Gas [" + e.GasPedal + "] Brake [" + e.BrakePedal + "] " + data);
        //Debug.Log("Gas [" + e.GasPedal + "] Brake [" + e.BrakePedal + "]");

    }
}
