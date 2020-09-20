using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static readonly UnityEvent<int, int, int> EventResourcesUsed = new UnityEvent<int, int, int>();

    public static readonly UnityEvent<int, int, int, int> EventBuildingLevelsChanged = new UnityEvent<int, int, int, int>();
}
