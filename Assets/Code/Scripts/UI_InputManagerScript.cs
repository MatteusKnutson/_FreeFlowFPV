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


    bool SetCorrectUIComponents()
    {
        // Pairs the correct ui elements to the code
        try
        {
            var root = GetComponent<UIDocument>().rootVisualElement;

            pgb_chanelValue = root.Q<ProgressBar>("AxisValue");
            btn_setMin = root.Q<Button>("SetMin");
            btn_setMax = root.Q<Button>("SetMax");
            drp_channelChoices = root.Q<DropdownField>("ChannelChoices");
            drp_axisChoices = root.Q<DropdownField>("AxisChoices");
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

        int axis = int.Parse(newValue.Remove(0, 5));
        GetSelectedChannel().axisIndex = axis;
    }

    private void OnDrpChannelChoicesChanged(ChangeEvent<string> evt)
    {
        string newValue = evt.newValue;
        string oldValue = evt.previousValue;

        drp_axisChoices.value = "Axis " + GetSelectedChannel().axisIndex;
    }

    void ShowCorrectValue()
    {
        // Shows the correct value in the progressbar
        if(drp_axisChoices.value != null)
        {
            float value = InputManager.GetConvertedInput(InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedChannel().axisIndex), GetSelectedChannel().axualMinValue, GetSelectedChannel().axualMaxValue);
            pgb_chanelValue.value = value;
            pgb_chanelValue.title = (int)value + " / 1000";
        }
        else
        {
            pgb_chanelValue.title = "select a channel";
        }

    }

    void Start()
    {
        SetCorrectUIComponents();
        ReloadChannelChoices();
        SetAxisChoices();

        drp_axisChoices.RegisterValueChangedCallback(OnDrpAxisChoicesChanged);
        drp_channelChoices.RegisterValueChangedCallback(OnDrpChannelChoicesChanged);
    }

    void Update()
    {
        ShowCorrectValue();


        // WILL BE REPLACED WITH THE BUTTONS LATER
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetSelectedChannel().axualMinValue = InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedChannel().axisIndex);
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            GetSelectedChannel().axualMaxValue = InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedChannel().axisIndex);
        }
    }
}
