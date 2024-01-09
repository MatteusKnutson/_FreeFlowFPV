using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_InputManagerScript : MonoBehaviour
{
    public ProgressBar pgb_chanelValue;
    public Button btn_setMin;
    public Button btn_setMax;
    public DropdownField drp_channelChoices;
    public DropdownField drp_axisChoices;
    public Button btn_close;

    bool checkForChannelChange;
    List<float> originalValues;

    bool SetCorrectUIComponents()
    {
        // Pairs the correct ui elements to the code
        try
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            pgb_chanelValue = root.Q<ProgressBar>("AxisValue");
            btn_setMin = root.Q<Button>("SetMinValue") as Button;
            btn_setMax = root.Q<Button>("SetMaxValue") as Button;
            drp_channelChoices = root.Q<DropdownField>("ChannelChoices");
            drp_axisChoices = root.Q<DropdownField>("AxisChoices");
            btn_close = root.Q<Button>("Close") as Button;
        }
        catch
        {
            return false;
        }
        return true;
    }

    void ReloadChannelChoices()
    {
        // Reloads the list of axis choices
        drp_channelChoices.choices.Clear();
        for (int i = 0; i < InputManager.inputChannels.Count; i++)
        {
            drp_channelChoices.choices.Add(InputManager.inputChannels[i].controlledChannel);
        }
    }

    void SetAxisChoices()
    {
        drp_axisChoices.choices.Clear();
        drp_axisChoices.choices.Add("AUTO");
        for (int i = 1; i <= 28; i++)
        {
            drp_axisChoices.choices.Add(InputManager.defaultAxisName + " " + i);
        }
    }

    AxisHandler GetSelectedChannel()
    {
        // Returns the InputChannel That the user selected
        string chanelName = drp_channelChoices.value;
        for (int i = 0; i < InputManager.inputChannels.Count; i++)
        {
            if(chanelName == InputManager.inputChannels[i].controlledChannel)
            {
                return InputManager.inputChannels[i];
            }
        }
        return null;
    }

    private void OnDrpAxisChoicesChanged(ChangeEvent<string> evt)
    {
        // Logic for when the input changes on the dropdown
        string newValue = evt.newValue;
        string oldValue = evt.previousValue;

        if(newValue == "AUTO")
        {
            originalValues = new List<float>();
            for(int i = 0; i < 28; i++)
            {
                originalValues.Add(InputManager.GetInputValue("Axis", i + 1));
            }
            checkForChannelChange = true;
        }
        else
        {
            int axis = int.Parse(newValue.Remove(0, 5));
            GetSelectedChannel().axisIndex = axis;
        }
    }

    private void OnDrpChannelChoicesChanged(ChangeEvent<string> evt)
    {
        string newValue = evt.newValue;
        string oldValue = evt.previousValue;

        drp_axisChoices.visible = true;
        drp_axisChoices.value = "Axis " + GetSelectedChannel().axisIndex;
    }

    void ShowCorrectValue()
    {
        // Shows the correct value in the progressbar
        if(drp_axisChoices.value != null)
        {
            float value = InputManager.GetConvertedInput(InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedChannel().axisIndex), GetSelectedChannel());
            pgb_chanelValue.value = value / 2 + 0.5f;
            pgb_chanelValue.title = Math.Round(value, 4).ToString();
        }
        else
        {
            pgb_chanelValue.title = "select a channel";
        }

    }

    private void OnMaxButtonClicked(ClickEvent evt)
    {
        GetSelectedChannel().actualMaxValue = InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedChannel().axisIndex);
        InputManager.InvertMinMaxValues(GetSelectedChannel());
    }

    private void OnMinButtonClicked(ClickEvent evt)
    {
        GetSelectedChannel().actualMinValue = InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedChannel().axisIndex);
        InputManager.InvertMinMaxValues(GetSelectedChannel());
    }

    private void OnCloseClicked(ClickEvent evt)
    {
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        SetCorrectUIComponents();
        ReloadChannelChoices();
        SetAxisChoices();


        btn_setMax.RegisterCallback<ClickEvent>(OnMaxButtonClicked);
        btn_setMin.RegisterCallback<ClickEvent>(OnMinButtonClicked);
        btn_close.RegisterCallback<ClickEvent>(OnCloseClicked);

        drp_axisChoices.RegisterValueChangedCallback(OnDrpAxisChoicesChanged);
        drp_channelChoices.RegisterValueChangedCallback(OnDrpChannelChoicesChanged);
        drp_axisChoices.visible = false;

        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }


    void Update()
    {
        ShowCorrectValue();

        if(drp_axisChoices.value == "AUTO")
        {
            btn_close.visible = false;
            btn_setMin.visible = false;
            btn_setMax.visible = false;
        }
        else
        {
            btn_close.visible = true;
            btn_setMin.visible = true;
            btn_setMax.visible = true;
        }

        if(checkForChannelChange)
        {
            for(int i = 0; i < 28; i++)
            {
                if (InputManager.GetInputValue("Axis", i+1) > originalValues[i] + 0.5f || InputManager.GetInputValue("Axis", i+1) < originalValues[i] - 0.5f)
                {
                    checkForChannelChange = false;
                    originalValues.Clear(); 
                    drp_axisChoices.value = "Axis " + (i + 1);
                    break;
                }
            }
        }
    }
}
