using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Perk", menuName = "Perk/Ability")]
public class PerkData : ScriptableObject, ISerializationCallbackReceiver
{
    public new string name;
    public int price;
    public float boostAmount;



    public void OnAfterDeserialize()
    {
        
    }

    public void OnBeforeSerialize() {}

}
