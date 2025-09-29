//using System.Numerics;
using UnityEngine;
using System.Collections.Generic;

public class S_ForkPickUp : MonoBehaviour
{
    List<GameObject> stackedBoxes = new();
    //bool hasBox = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pallet"))
        {
            int nr = stackedBoxes.Count;
            other.gameObject.GetComponent<S_BoxScript>().attachBox(this.gameObject, nr);
            stackedBoxes.Add(other.gameObject);         
        }
    }
}
