using System;
using UnityEngine;


[Serializable]
public class Armor
{
    [Header("Armor Points.")]
    public int totalPoints;

    [Header("Armor Pieces.")]
    public Head head;
    public Shoulders shoulders;
    public Chest chest;
    public Hands hands;
    public Leggings leggings;
    public Feet feet;

    public void CalculatePoints()
    {
        totalPoints = 0;
        totalPoints += (head != null) ? head.points : 0;
        totalPoints += (shoulders != null) ? shoulders.points : 0;
        totalPoints += (chest != null) ? chest.points : 0;
        totalPoints += (hands != null) ? hands.points : 0;
        totalPoints += (leggings != null) ? leggings.points : 0;
        totalPoints += (feet != null) ? feet.points : 0;
    }
}
