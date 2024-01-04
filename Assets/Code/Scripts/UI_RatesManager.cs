using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_RatesManager : MonoBehaviour
{
    public FloatField[] ratesRoll;
    public FloatField[] ratesPitch;
    public FloatField[] ratesYaw;
    public Button btn_save;
    public Button btn_close;

    bool SetCorrectUiComponents()
    {
        try
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            ratesRoll[0] = root.Q<FloatField>("RollRCR");
            ratesRoll[1] = root.Q<FloatField>("RollSR");
            ratesRoll[2] = root.Q<FloatField>("RollRCE");

            ratesPitch[0] = root.Q<FloatField>("PitchRCR");
            ratesPitch[1] = root.Q<FloatField>("PitchSR");
            ratesPitch[2] = root.Q<FloatField>("PitchRCE");

            ratesYaw[0] = root.Q<FloatField>("YawRCR");
            ratesYaw[1] = root.Q<FloatField>("YawSR");
            ratesYaw[2] = root.Q<FloatField>("YawRCE");

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

        InputManager.rates.Clear();

        InputManager.rates = new List<RatesHandler>()
        {
            new("roll", ratesRoll[0].value, ratesRoll[1].value, ratesRoll[2].value),
            new("pith", ratesPitch[0].value, ratesPitch[1].value, ratesPitch[2].value),
            new("yaw", ratesYaw[0].value, ratesYaw[1].value, ratesYaw[2].value)
        };

    }

    private void OnCloseClicked(ClickEvent evt)
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        ratesRoll = new FloatField[3];
        ratesPitch = new FloatField[3];
        ratesYaw = new FloatField[3];

        SetCorrectUiComponents();

        btn_save.RegisterCallback<ClickEvent>(OnSaveClicked);
        btn_close.RegisterCallback<ClickEvent>(OnCloseClicked);

    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
