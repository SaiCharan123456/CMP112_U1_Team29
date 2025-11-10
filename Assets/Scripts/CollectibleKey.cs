using UnityEngine;

//Inherits from a base class to make more efficient the use of multiple collectibles
public class CollectibleKey : CollectiblesRoot
{

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            source.PlayOneShot(pickupSound, 1.0f); //Run the mechanics of object collection
            GameManager.instance.score += 200;
            GameManager.instance.keys += 1;

            Debug.Log("Collected a key"); //Log the changes
            Debug.Log("Score: " + GameManager.instance.score);
            Debug.Log("Keys: " + GameManager.instance.keys);

            Destroy(gameObject); //Remove the object
        }
    }

}
