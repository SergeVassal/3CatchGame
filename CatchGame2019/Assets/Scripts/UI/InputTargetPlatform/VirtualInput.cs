using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class VirtualInput 
{
    protected Dictionary<string, CrossPlatformInputManager.VirtualAxis> virtualAxes =
        new Dictionary<string, CrossPlatformInputManager.VirtualAxis>();

    protected Dictionary<string, CrossPlatformInputManager.VirtualButton> virtualButtons =
        new Dictionary<string, CrossPlatformInputManager.VirtualButton>();



    public bool AxisExists(string name)
    {
        return virtualAxes.ContainsKey(name);
    }

    public bool ButtonExists(string name)
    {
        return virtualButtons.ContainsKey(name);
    }


    public CrossPlatformInputManager.VirtualAxis VirtualAxisReference(string name)
    {
        if (!virtualAxes.ContainsKey(name))
        {
            throw new Exception("There's no such axis registered");
        }
        return virtualAxes[name];
    }


    public void RegisterVirtualAxis(CrossPlatformInputManager.VirtualAxis virtualAxis)
    {
        if (virtualAxes.ContainsKey(virtualAxis.Name))
        {
            throw new Exception("There's already such axis registered");
        }
        else
        {            
            virtualAxes.Add(virtualAxis.Name, virtualAxis);
        }
    }

    public void RegisterVirtualButton(CrossPlatformInputManager.VirtualButton virtualButton)
    {
        if (virtualButtons.ContainsKey(virtualButton.Name))
        {
            throw new Exception("There's already such axis registered");
        }
        else
        {
            virtualButtons.Add(virtualButton.Name, virtualButton);
        }
    }

    public void UnregisterVirtualAxis(string name)
    {
        if (!virtualAxes.ContainsKey(name))
        {
            throw new Exception("There's no such axis registered");
        }
        virtualAxes.Remove(name);
    }

    public void UnregisterVirtualButton(string name)
    {
        if (!virtualButtons.ContainsKey(name))
        {
            throw new Exception("There's no such button registered");
        }
        virtualButtons.Remove(name);
    }


    public abstract void SetAxis(string name, float value);

    public abstract void SetAxisPositive(string name);

    public abstract void SetAxisNegative(string name);

    public abstract void SetAxisZero(string name);

    public abstract float GetAxis(string name, bool raw);


    public abstract void SetButtonDown(string name);

    public abstract void SetButtonUp(string name);

    public abstract bool GetButton(string name);

    public abstract bool GetButtonDown(string name);

    public abstract bool GetButtonUp(string name);
}
