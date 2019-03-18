using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum type
{

}


[Serializable]
class Skill
{
    public string Name;
    public string Info;

    public int SkillID;

    public Sprite Img;

    public int DMG;
    public type Skill_type;
}


public class SkillBase : MonoBehaviour
{
    [SerializeField] Skill[] skills;
}
