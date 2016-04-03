﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sol;

[System.Serializable]
public class Stat
{
    public int id;
    public string displayName;
    public float statValue;

    public List<RoverComponent> equippedComponents = new List<RoverComponent>();

}
