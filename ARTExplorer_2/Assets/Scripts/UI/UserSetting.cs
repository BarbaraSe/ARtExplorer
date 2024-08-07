using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSetting : MonoBehaviour
{
    public Direction StrongSide {get ; set; }
}

public enum Direction 
{
    Left,
    Right
}