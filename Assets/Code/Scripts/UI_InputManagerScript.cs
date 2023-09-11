using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        drp_axisChoices.choices.Add("AUTO");
        for (int i = 1; i <= 28; i++)
        {
            drp_axisChoices.choices.Add(InputManager.defaultAxisName + " " + i);
        }
    }

    int GetSelectedChannel()
    {
        // Returns the int in the inputChannels list which the user has selected
        string chanelName = drp_channelChoices.value;
        for (int i = 0; i < InputManager.inputChannels.Count; i++)
        {
            if(chanelName == InputManager.inputChannels[i].controlledChannel)
            {
                return i;
            }
        }
        return -1;
    }

    void PairButtons()
    {
        // Pairs the set min and max buttons to the correct method.
        btn_setMin.clicked += () => InputManager.SetMinValue(InputManager.inputChannels[GetSelectedChannel()], (int)InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedChannel()));
        btn_setMax.clicked += () => InputManager.SetMinValue(InputManager.inputChannels[GetSelectedChannel()], (int)InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedChannel()));
        
    }

    private void OnDrpAxisValueChanged(ChangeEvent<string> evt)
    {
        string newValue = evt.newValue;
        string oldValue = evt.previousValue;

        Debug.Log("Dropdown value changed from " + oldValue + " to " + newValue);
    }

    private void Test(ChangeEvent<string> evt)
    {
        Debug.Log("HIHI");
    }

    void ShowCorrectValue()
    {
        // Shows the correct value in the progressbar
        pgb_chanelValue.value = InputManager.GetConvertedInput(InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedChannel()), InputManager.inputChannels[GetSelectedChannel()].axualMinValue, InputManager.inputChannels[GetSelectedChannel()].axualMaxValue);
        pgb_chanelValue.title = (int)InputManager.GetConvertedInput(InputManager.GetInputValue(InputManager.defaultAxisName, GetSelectedChannel()), InputManager.inputChannels[GetSelectedChannel()].axualMinValue, InputManager.inputChannels[GetSelectedChannel()].axualMaxValue) + "/1000";
    }

    void Start()
    {
        
        SetCorrectUIComponents();
        ReloadChannelChoices();
        SetAxisChoices();
        PairButtons();

        drp_axisChoices.RegisterValueChangedCallback(OnDrpAxisValueChanged);

    }

    void Update()
    {
        //ShowCorrectValue();

    }
}
