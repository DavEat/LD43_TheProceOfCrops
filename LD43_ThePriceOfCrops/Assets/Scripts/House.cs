﻿using System.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
    #region Vars
    [SerializeField] private float _reproduceTime = 15;

    private Habitant[] _habitants = new Habitant[2];

    public Transform door;
    #endregion
    #region Functions
    public bool AddFarmer(Farmer farmer)
    {
        if (_habitants[0].farmer == null)
            _habitants[0].farmer = farmer;
        else if (_habitants[1].farmer == null)
            _habitants[1].farmer = farmer;
        else return false;

        farmer.SendToHouse(this);
        return true;
    }
    public void FarmerArrived(Farmer farmer)
    {
        if (farmer == _habitants[0].farmer)
            _habitants[0].inside = true;
        else if (farmer == _habitants[1].farmer)
            _habitants[1].inside = true;

        //PlaceFarmer + Anim
        if (_habitants[0].inside && _habitants[1].inside)
        {
            StartCoroutine(ReproduceTimer());
        }
    }
    private IEnumerator ReproduceTimer()
    {
        yield return new WaitForSeconds(_reproduceTime);
        Reproduce();
    }
    private void Reproduce()
    {
        Debug.Log("Reproduce");

        _habitants[0].farmer.LeaveHouse();
        _habitants[0].Clear();
        _habitants[1].farmer.LeaveHouse();
        _habitants[1].Clear();

        GameManager.inst.InstatiateFarmer(door.position, door.rotation);
    }
    #endregion

    private struct Habitant
    {
        public Farmer farmer;
        public bool inside;

        public void Clear()
        {
            farmer = null;
            inside = false;
        }
    }
}
