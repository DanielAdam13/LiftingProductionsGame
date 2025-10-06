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

    public void DropBoxes()
    {
        for (int i = stackedBoxes.Count - 1; i >= 0; i--)
        {
            stackedBoxes[i].GetComponent<S_BoxScript>().detachBox();
            stackedBoxes.RemoveAt(i);
        }
    }
}
