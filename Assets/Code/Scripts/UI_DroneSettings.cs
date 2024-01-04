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

    public Button btn_save;
    public Button btn_close;

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

            btn_save = root.Q<Button>("Save") as Button;
            btn_close = root.Q<Button>("Close") as Button;

        }
        catch
        {
            return false;
        }
        return true;
    }

    private void OnSaveClicked(ClickEvent evt)
    {
        DronePhysicsHandler.droneHandler = new DroneHandler(drone_weight.value,motor_kv.value,battery_c.value, prop_pitch.value, prop_d.value);

        DronePhysicsHandler.r.mass = drone_weight.value / 1000;
    }

    private void OnCloseClicked(ClickEvent evt)
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;

        SetCorrectUiComponents();

        btn_save.RegisterCallback<ClickEvent>(OnSaveClicked);
        btn_close.RegisterCallback<ClickEvent>(OnCloseClicked);

    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
