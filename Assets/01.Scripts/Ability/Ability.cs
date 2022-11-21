using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public List<Skill> skills = new List<Skill>();
    public Color mainColor;
}