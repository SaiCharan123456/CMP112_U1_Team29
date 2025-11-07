using UnityEngine;

//Inherits from a root class to make more efficient the use of multiple collectibles
public class CollectibleHealth : CollectiblesRoot
{

    public override void OnTriggerEnter(Collider other)
    {
        source.PlayOneShot(pickupSound, 1.0f); //Run the mechanics of object collection
        Debug.Log("Collected some health");

        playerState.instance.health += 5; //Log the changes
        Debug.Log("Health: " + playerState.instance.health);

        Destroy(gameObject); //Remove the object
    }

}
