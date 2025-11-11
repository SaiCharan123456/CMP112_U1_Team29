using UnityEngine;

//Inherits from a base class to make more efficient the use of multiple collectibles
public class CollectibleHealth : CollectiblesRoot
{

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject sound = Instantiate(audioPrefab); //Run the mechanics of object collection
            Debug.Log("Collected some health");

            GameManager.instance.health += 1; //Log the changes
            Debug.Log("Health: " + GameManager.instance.health);

            Destroy(gameObject); //Remove the object
        }
    }

}
