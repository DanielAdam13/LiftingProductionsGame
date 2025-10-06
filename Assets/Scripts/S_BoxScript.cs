using UnityEngine;
using static UnityEngine.UI.Image;

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
        this.gameObject.transform.SetParent(null);
        RaycastHit hit;

        float maxDistance = 10.0f;

        if (Physics.Raycast(this.gameObject.transform.position, Vector3.down, out hit, maxDistance))
        {
            // Move object to the hit point
            transform.position = hit.point;
        }
    }
}
