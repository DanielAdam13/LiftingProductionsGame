using UnityEngine;

public class S_BoxScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Int nr is what nuber the box is in the tower from botom to the top
    public void attachBox( GameObject forks, int nr)
    {
        Vector3 pos = transform.localPosition;
        pos.y += 0.5f + 1 * nr;
        this.gameObject.transform.SetLocalPositionAndRotation(pos, Quaternion.identity);
        transform.parent = forks.transform;
    }

    public void detachBox()
    {

    }
}
