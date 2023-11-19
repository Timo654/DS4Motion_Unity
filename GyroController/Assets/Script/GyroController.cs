using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.DualShock;

public class GyroController
{
    // Gyroscope
    public static ButtonControl gyroX = null;
    public static ButtonControl gyroY = null;
    public static ButtonControl gyroZ = null;

    // Acceleration
    public static ButtonControl acclX = null;
    public static ButtonControl acclY = null;
    public static ButtonControl acclZ = null;

    public static Gamepad controller = null;

    public static Gamepad getController(string layoutFile = null)
    {
        string layout = "";
        switch (Gamepad.current)
        {
            case DualSenseGamepadHID:
                layout = File.ReadAllText(layoutFile == null ? "Assets/ControllerLayouts/layoutDS5.json" : layoutFile);
                Debug.Log("DualSense detected.");
                break;
            case DualShock4GamepadHID:
                layout = File.ReadAllText(layoutFile == null ? "Assets/ControllerLayouts/layoutDS4.json" : layoutFile);
                Debug.Log("DualShock 4 detected.");
                break;
        }

        // Overwrite the default layout
        InputSystem.RegisterLayoutOverride(layout, "GyroGamepad");
        var controller = Gamepad.current;
        GyroController.controller = controller;
        bindControls(GyroController.controller);
        return GyroController.controller;
    }

    private static void bindControls(Gamepad controller)
    {
        gyroX = controller.GetChildControl<ButtonControl>("gyro X1");
        gyroY = controller.GetChildControl<ButtonControl>("gyro Y1");
        gyroZ = controller.GetChildControl<ButtonControl>("gyro Z1");
        acclX = controller.GetChildControl<ButtonControl>("accl X1");
        acclY = controller.GetChildControl<ButtonControl>("accl Y1");
        acclZ = controller.GetChildControl<ButtonControl>("accl Z1");
    }

    public static Quaternion getRotation(float scale = 1)
    {
        float x = processRawData(gyroX.ReadValue()) * scale;
        float y = processRawData(gyroY.ReadValue()) * scale;
        float z = -processRawData(gyroZ.ReadValue()) * scale;
        return Quaternion.Euler(x, y, z);
    }

    private static float processRawData(float data)
    {
        return data > 0.5 ? 1 - data : -data;
    }
}