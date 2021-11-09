using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings")]
public class PlayerSettings : ScriptableObject
{
    public bool isPlaying;
    public bool isDeath;
    public float ForwardSpeed;
    public float sensitivity;
    public float sledgeScale;
    public int punchScore;
    public int score;
    public int finishScore;
    public int level;
}
