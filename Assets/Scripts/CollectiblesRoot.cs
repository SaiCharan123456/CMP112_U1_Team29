using System.Security;
using UnityEngine;

public class CollectiblesRoot : PlayerController
{
    [SerializeField] float m_MyX;
    [SerializeField] GameObject player;
    public int tempScore; //should be removed later!! This is just to make collectibles do something other than vanish for now!!!
    public AudioSource source;
    public AudioClip pickupSound;

    // Update is called once per frame
    public virtual void Update() //currently has no overwrites, implemented as virtual anyway in case a future collectible should need it
    {
        transform.Rotate(new Vector3(0f, 0f, 60f) * Time.deltaTime); //rotate around the Z axis, to appear like the Y axis (all collectibles should be rotated 90 degrees)
    }

    public virtual void OnTriggerEnter(Collider player) 
    {
        source.PlayOneShot(pickupSound, 1.0f);
        Debug.Log("Picked up an unidentified collectible!"); //error message of a sort, identifies if for whatever reason a collectible is picked up but doesn't run the specific collectible script
    }
}
