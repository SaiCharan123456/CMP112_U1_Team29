using UnityEngine;

//Inherits from a root class to make more efficient the use of multiple collectibles
public class CollectibleKey : CollectiblesRoot
{

    public override void OnTriggerEnter(Collider other)
    {
        source.PlayOneShot(pickupSound, 1.0f); //Run the mechanics of object collection
        playerState.instance.score += 200;
        playerState.instance.keysCollected += 1;

        Debug.Log("Collected a key"); //Log the changes
        Debug.Log("Score: " + playerState.instance.score);
        Debug.Log("Keys: " + playerState.instance.keysCollected);

        Destroy(gameObject); //Remove the object
    }

}
