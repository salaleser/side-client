using System;
using System.Collections.Generic;
using Models;

[System.Serializable]
public class GroundItem
{
    public int x;
    public int y;
    public AddressItem address;
    public GroundType type_id;

    public override string ToString() => @$"Ground:
    X: {x} / Y: {y}
    Type ID: {type_id}";
}
