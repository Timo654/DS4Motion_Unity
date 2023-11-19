using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    private Gamepad controller = null;
    private Transform m_transform;

    void Start()
    {
        controller = GyroController.getController();
        m_transform = transform;
    }

    void Update()
    {
        if (controller == null)
        {
            try
            {
                controller = GyroController.getController();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        else
        {
            // Press circle button to reset rotation
            if (controller.buttonEast.isPressed)
            {
                m_transform.rotation = Quaternion.identity;
            }
            m_transform.rotation *= GyroController.getRotation(4000 * Time.deltaTime);
        }
    }
}