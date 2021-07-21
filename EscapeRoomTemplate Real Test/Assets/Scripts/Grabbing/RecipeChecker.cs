using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class RecipeChecker : MonoBehaviour
{
    public List<Ingredient> recipe = new List<Ingredient>();
    public List<Ingredient> ingredients = new List<Ingredient>();
    public bool recipeMatch = false;
    public bool completed = false;

    public UnityEvent recipeComplete;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    } 
    
    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void SearchForMatch()
    {
        
    }

    private void CheckForSettings()
    {
        
    }
}
