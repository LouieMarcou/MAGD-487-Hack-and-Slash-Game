using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    [SerializeField] private float healthAmount = 10f;

    public float GetHealthAmount()
    {
        return healthAmount;
    }
}
