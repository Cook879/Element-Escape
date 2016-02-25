using UnityEngine;
 
public class GemCreator: MonoBehaviour {
  //1
  public float minSpawnTime = 0.75f; 
  public float maxSpawnTime = 2f; 
  public GameObject[] prefabs;
 
  //2    
  void Start () {
    Invoke("SpawnGem",minSpawnTime);
  }
 
  //3
  public void SpawnGem()
  {
    Camera camera = Camera.main;
    Vector3 cameraPos = camera.transform.position;
    float xMax = camera.aspect * camera.orthographicSize;
    float xRange = camera.aspect * camera.orthographicSize * 1.75f;
    float yMax = camera.orthographicSize - 0.5f;
     

    // Get a random number for the gem colour
    int index = Random.Range(0,4);

    // 2
    Vector3 pos = 
      new Vector3(cameraPos.x + Random.Range(xMax - xRange, xMax),
                  Random.Range(-yMax, yMax),
                  prefabs[index].transform.position.z);
     
    GameObject gem = Instantiate(prefabs[index], pos, Quaternion.identity) as GameObject;
    gem.GetComponent<GemController>().setGemCreator(this);
  }
}