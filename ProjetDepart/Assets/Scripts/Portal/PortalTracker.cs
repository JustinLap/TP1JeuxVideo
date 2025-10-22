using System.Collections.Generic;
using UnityEngine;

public class PortalTracker : MonoBehaviour
{
    private readonly List<Portal> activePortals = new();

    public void RegisterPortal(Portal portal)
    {
        if (!activePortals.Contains(portal))
            activePortals.Add(portal);
    }

    public void UnregisterPortal(Portal portal)
    {
        if (activePortals.Contains(portal))
            activePortals.Remove(portal);

        if (activePortals.Count == 0)
        {
            Finder.EventChannels.PublishLevelWin();
        }
    }
}
