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
            Finder.EventChannels.PublishLevelEnd();
        }
    }

    public Portal GetRandomPortal()
    {
        if (activePortals.Count == 0) return null;
        return activePortals[Random.Range(0, activePortals.Count)];
    }

    public bool HasActivePortals() => activePortals.Count > 0;
}
