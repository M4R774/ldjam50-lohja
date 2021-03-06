using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLighting : MonoBehaviour
{
    public World world;
    public GameObject FireLight;
    public GameObject player;

    public void Start()
    {
        StartCoroutine(UpdateFireLights());
    }

    IEnumerator UpdateFireLights()
    {
        while (true)
        {
            for (int i = 0; i < world.activeFires.Count; i++)
            {
                if (Vector3.Distance(player.transform.position, world.activeFires[i].GetWorldPosition()) > 15)
                {
                    continue;
                }
                Vector3 firePosition = world.activeFires[i].GetWorldPosition();
                Collider2D[] collisions = Physics2D.OverlapCircleAll(firePosition, 2.5f);
                int firesInRange = 0;
                foreach (Collider2D collision in collisions)
                {
                    if (collision.gameObject.tag == "FireLight")
                    {
                        firesInRange++;
                        break;
                    }
                }
                if (firesInRange < 1)
                {
                    GameObject newFireLight = Instantiate(FireLight, transform);
                    newFireLight.transform.position = world.activeFires[i].GetWorldPosition() + new Vector3(.5f, .5f, 0);
                    newFireLight.GetComponent<SelfDestruct>().player = player;
                    newFireLight.GetComponent<SelfDestruct>().fire = world.activeFires[i];
                    newFireLight.GetComponent<SelfDestruct>().originalPosition = newFireLight.transform.position;
                }
                yield return new WaitForSeconds(.0012f); // 0.032 = 30fps
            }
            yield return new WaitForSeconds(.0012f); // 0.032 = 30fps
        }
    }
}
