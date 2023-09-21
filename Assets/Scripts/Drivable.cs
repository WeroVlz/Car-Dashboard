using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drivable : MonoBehaviour
{

    public event EventHandler<DrivableArgs> UpdatedData;
    public InputAction Wheel;
    public InputAction GasBrakePedal;
    public InputAction[] Button = new InputAction[12];
    private DrivableArgs _PrevDrivableArgs = new DrivableArgs();

    public void OnEnable()
    {
        Wheel.Enable();
        GasBrakePedal.Enable();
        foreach (InputAction button in Button)
        {
            button.Enable();
        }
    }

    public void OnDisable()
    {
        Wheel.Disable();
        GasBrakePedal.Disable();
        foreach (InputAction button in Button)
        {
            button.Disable();
        }
    }
    

    public class DrivableArgs : EventArgs
    {
        public float WheelRotation { get; set; }
        public float GasPedal { get; set; }
        public float BrakePedal { get; set; }
        public bool[] Buttons = new bool[12];
    }

    void Update()
    {
        DrivableArgs drivableArgs = _PrevDrivableArgs;

        if (Wheel.triggered)
        {
            drivableArgs.WheelRotation = Wheel.ReadValue<float>();
        }
        if (GasBrakePedal.triggered)
        {
            float gasBrake = GasBrakePedal.ReadValue<float>();
            drivableArgs.GasPedal = 0f;
            drivableArgs.BrakePedal = 0f;
            if (gasBrake < 0)
            {
                drivableArgs.BrakePedal = gasBrake;
            }
            if (gasBrake > 0)
            {
                drivableArgs.GasPedal = gasBrake;
            }
            //Debug.Log("Gas " + gasBrake);
        }
        int i = 0;
        foreach (InputAction button in Button)
        {
            drivableArgs.Buttons[i] = false;
            if (button.triggered)
            {
                if (button.ReadValue<float>() > 0) { drivableArgs.Buttons[i] = true; }
            }
            i++;
        }

        OnUpdateData(drivableArgs);
    }

    protected virtual void OnUpdateData(Drivable.DrivableArgs e)
    {
        EventHandler<Drivable.DrivableArgs> handler = UpdatedData;
        if (handler != null)
        {
            handler(this, e);
        }
    }
}
