using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_DroneSettings : MonoBehaviour
{
    public FloatField prop_d;
    public FloatField prop_pitch;
    public FloatField motor_kv;
    public FloatField battery_c;
    public FloatField drone_weight;
    public FloatField camera_angle;

    public Button btn_save;
    public Button btn_close;

    public Transform fpvCamera;

    bool SetCorrectUiComponents()
    {
        try
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            prop_d = root.Q<FloatField>("s_prop_d");
            prop_pitch = root.Q<FloatField>("s_prop_pitch");
            motor_kv = root.Q<FloatField>("s_motor_kv");
            battery_c = root.Q<FloatField>("s_battery_c");
            drone_weight = root.Q<FloatField>("s_drone_weight");
            camera_angle = root.Q<FloatField>("s_camera_angle");

            btn_save = root.Q<Button>("Save") as Button;
            btn_close = root.Q<Button>("Close") as Button;

        }
        catch
        {
            return false;
        }
        return true;
    }

    void ShowCorrectValues()
    {
        prop_d.value = DronePhysicsHandler.droneHandler.propDiameter;
        prop_pitch.value = DronePhysicsHandler.droneHandler.propPitch;
        motor_kv.value = DronePhysicsHandler.droneHandler.motorKv;
        battery_c.value = DronePhysicsHandler.droneHandler.batteryCells;
        drone_weight.value = DronePhysicsHandler.droneHandler.weight * 1000;
        camera_angle.value = DronePhysicsHandler.droneHandler.cameraAngle;
    }

    private void OnSaveClicked(ClickEvent evt)
    {
        DronePhysicsHandler.droneHandler = new DroneHandler(drone_weight.value / 1000,motor_kv.value,battery_c.value, prop_pitch.value, prop_d.value, camera_angle.value);

        DronePhysicsHandler.r.mass = drone_weight.value / 1000;

        fpvCamera.localEulerAngles = new Vector3(-camera_angle.value, 0, 0);
    }

    private void OnCloseClicked(ClickEvent evt)
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;

        SetCorrectUiComponents();
        ShowCorrectValues();

        btn_save.RegisterCallback<ClickEvent>(OnSaveClicked);
        btn_close.RegisterCallback<ClickEvent>(OnCloseClicked);

    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
