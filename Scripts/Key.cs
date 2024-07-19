using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IItem
{
    public void Collect()
    {
        SoundEffectManager.Play("KeyCollect");
        Destroy(gameObject);
    }
}