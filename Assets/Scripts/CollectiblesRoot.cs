using UnityEngine;

public class CollectiblesRoot : MonoBehaviour
{
    [SerializeField] float m_MyX;
    [SerializeField] GameObject player;
    public int tempScore; //should be removed later!! This is just to make collectibles do something other than vanish for now!!!

    // Update is called once per frame
    public virtual void Update() //currently has no overwrites, implemented as virtual anyway in case a future collectible should need it
    {
        transform.Rotate(new Vector3(0f, 0f, 60f) * Time.deltaTime); //rotate around the Z axis, to appear like the Y axis (all collectibles should be rotated 90 degrees)
    }

    public virtual void OnTriggerEnter(Collider player) 
    {
        Debug.Log("Picked up an unidentified collectible!"); //error message of a sort, identifies if for whatever reason a collectible is picked up but doesn't run the specific collectible script
    }
}
