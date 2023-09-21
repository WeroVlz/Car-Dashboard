using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class manages and modifies every aspect of the dashboard that involves speed and kilometers coveres.
/// </summary>
public class Speedometer : MonoBehaviour
{
    public Drivable Drivable;
    public StartUp StartUp;
    private Transform needleTransform;
    private Transform rpmNeedleTransform;
    public TextMeshProUGUI parking;
    public TextMeshProUGUI driving;
    public TextMeshProUGUI mileage;
    public TextMeshProUGUI trip;
    public TextMeshProUGUI range;
    private Color activatedColor = new Color(32/255f,118/255f,149/255f);
    private Color deactivatedColor = new Color(41/255f,41/255f,41/255f);
    public Image fuelTank;
    public Sprite[] fuelTankSprites = new Sprite[9];
    private const float MAX_SPEED_LIMIT = 260.0f;
    private const float MAX_SPEED_ANGLE = -130;
    private const float MIN_SPEED_ANGLE = 130;
    private float speed = 0.0f;
    private float rpmSpeed =  0.0f;
    private float gearLimit = 0.0f;
    private bool gearFlag = false;
    private bool startAnimationFlag = true;
    private bool startAnimationLimitFlag = false;
    private float time = 0.0f;
    private float mileageCount = 41578;
    private float tripCount = 0.0f;
    private float rangeCount = 450;
    private float fuelUsage = 0.0f;
    private int fuelIndex = 0;
    

    void Start()
    {
        parking.color = activatedColor;
        needleTransform = transform.Find("Needle");
        rpmNeedleTransform = transform.Find("NeedleRPM");
        Drivable.UpdatedData += UpdatedData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatedData(object sender, Drivable.DrivableArgs e){
        if(StartUp.getScreenOnFlag()){
            //TO DO: Startup animation
            if(startAnimationFlag){

                if(speed > MAX_SPEED_LIMIT && rpmSpeed > MAX_SPEED_LIMIT) startAnimationLimitFlag = true;
                if(rpmSpeed < 28.0f && startAnimationLimitFlag){
                    rpmSpeed = 25.0f;
                    if(speed < 3.0f && startAnimationLimitFlag){
                        speed = 0.0f;
                        startAnimationFlag = false;
                    }
                }
                if(startAnimationLimitFlag == false){
                    speed += 6.0f;
                    rpmSpeed += 6.0f;
                }else{
                    speed -= 6.0f;
                    rpmSpeed -= 6.0f;
                }
            }
            
        }
        if(startAnimationFlag == false){
            time += Time.deltaTime;

            if(e.GasPedal > 0.0f){
                parking.color = deactivatedColor;
                driving.color = activatedColor;
            }

            if(speed < MAX_SPEED_LIMIT){
                speed += e.GasPedal/5f;
                if (gearFlag == false) rpmSpeed += e.GasPedal/2f;
                gearLimit += e.GasPedal/5f;
            }
            else{
                speed = MAX_SPEED_LIMIT;
            } 
                
            if(e.GasPedal < 0.05f){
                speed -= 0.05f;
                rpmSpeed -= 0.05f;
            } 
                

            if(speed > 0.0f){
                if(e.BrakePedal < -0.1f){
                    speed += e.BrakePedal/5f;
                    rpmSpeed += e.BrakePedal/4f;
                }
                if(rpmSpeed < 25.0f){
                    rpmSpeed =25.0f;
                }

            }
            else{
                speed = 0.0f;
                rpmSpeed = 25.0f;
            } 

            if(gearLimit > 30.0f){
                gearFlag = true;
                gearLimit = 0.0f;
            }

            if(gearFlag){
                rpmSpeed -= 2.0f;
                if(rpmSpeed < 25.0f){
                    gearFlag = false;
                    rpmSpeed = 25.0f;
                }
            }
                
            tripCount += speed/36000 * time;
            mileageCount += speed/36000 * time;
            rangeCount -= speed/36000 * time;
            fuelUsage += speed/36000 * time;
            mileage.text = (int)mileageCount + "km";
            trip.text = tripCount.ToString("F1") + "km";
            range.text = (int)rangeCount + "km";

            if(e.Buttons[10]){
                tripCount = 0.0f;
                trip.text = tripCount.ToString("F1") + "km";
            }

            if(fuelUsage > 50.0f){
                fuelUsage = 0.0f;
                fuelIndex ++;
                if(fuelIndex > 8){
                    Debug.Log("Out of gas.");
                    Application.Quit();
                }
                else{
                    fuelTank.sprite = fuelTankSprites[fuelIndex]; 
                }
                if(fuelIndex > 7) StartUp.symbols[9].enabled = true;
            }
        }

        needleTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
        rpmNeedleTransform.eulerAngles = new Vector3(0, 0, GetRpmRotation());
         
    }

    /// <summary>
    ///     Calculates the rotation that we need for the needle in order to move according to the speed.
    /// </summary>
    /// <returns> Speed Rotation</returns>
    private float GetSpeedRotation(){
        float needleAngle = MIN_SPEED_ANGLE - MAX_SPEED_ANGLE;

        float speedNormalized = speed/ MAX_SPEED_LIMIT;

        return MIN_SPEED_ANGLE - speedNormalized * needleAngle;
    }

    /// <summary>
    /// Calculates the rotation that we need for the rpm needle in order to move according to the engine revolutions.
    /// </summary>
    /// <returns> Rpm Rotation</returns>
    private float GetRpmRotation(){
        float needleAngle = MIN_SPEED_ANGLE - MAX_SPEED_ANGLE;

        float rpmSpeedNormalized = rpmSpeed/MAX_SPEED_LIMIT;

        return MIN_SPEED_ANGLE - rpmSpeedNormalized * needleAngle;
    }
}
