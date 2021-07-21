using System;
using UnityEngine;

public class Ingredient : MonoBehaviour, IComparable<Ingredient>
{
    public enum Ingredients
    {
        
    };

    public Ingredients ingredient;
    
    public int CompareTo(Ingredient other)
    {
        //replace this with your code.
        return 0;
    }
}
