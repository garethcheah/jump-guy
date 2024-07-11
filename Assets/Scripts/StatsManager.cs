using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;

    private int _collectibleCount;
    private DateTime _dtLastDamaged;

    // Start is called before the first frame update
    void Start()
    {
        _dtLastDamaged = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        _uiManager.UpdateDamageTimer(GetTimeSinceLastDamage());
    }

    public void UpdateCollectibleCount()
    {
        _collectibleCount += 1;
        _uiManager.UpdateCollectibleCount(_collectibleCount);
    }

    public void UpdateLastDamagedTime()
    {
        _dtLastDamaged = DateTime.Now;
    }

    private TimeSpan GetTimeSinceLastDamage()
    {
        return DateTime.Now.Subtract(_dtLastDamaged);
    }
}
