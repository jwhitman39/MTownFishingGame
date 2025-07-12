using Unity.VisualScripting;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float requiredReels;
    public float provisionYield;
    public bool remove = false;

    public void Update()
    {
        if (remove)
        {
            DestroyImmediate(gameObject);
        }
    }
}
