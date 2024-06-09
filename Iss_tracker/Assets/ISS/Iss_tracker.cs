using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Iss_tracker : MonoBehaviour
{
    // Start is called before the first frame update

    public float raio = 1.2f;

    private Vector3 vetorVelocidade;
    private const string apiUrl = "https://api.wheretheiss.at/v1/satellites/25544";
    void Start()
    {
        StartCoroutine(IssMovimento(apiUrl));
    }

    IEnumerator IssMovimento(string ulr)
    {
        
        ISSResponse response;

        Vector3 pos = Vector3.zero;
        Vector3 pos_anterior = Vector3.zero;
        while (true)
        {
            UnityWebRequest request = UnityWebRequest.Get(apiUrl);
            yield return request.SendWebRequest();

            response = JsonUtility.FromJson<ISSResponse>(request.downloadHandler.text);
            pos_anterior = pos;


            float x = Mathf.Cos(response.latitude*Mathf.Deg2Rad)* Mathf.Cos(response.longitude * Mathf.Deg2Rad) * raio;
            float z = Mathf.Cos(response.latitude * Mathf.Deg2Rad) * Mathf.Sin(response.longitude * Mathf.Deg2Rad) * raio;
            float y = Mathf.Sin(response.latitude * Mathf.Deg2Rad) * raio;
            Debug.Log($"latitude:{response.latitude} longitude:{response.longitude} z:{z}");
            
            pos = new Vector3(x, y, z);
            transform.position = pos;



            Vector3 directionToOrigin = Vector3.zero - transform.position;
            // Calculate the rotation needed to look at the origin
            Quaternion lookRotation = Quaternion.LookRotation(directionToOrigin);
            // Apply the rotation to the GameObject, adjusting to make the x-axis point to the origin
            // This can be done by rotating 90 degrees around the y-axis after LookRotation
            transform.rotation = lookRotation * Quaternion.Euler(0, -90, 0);

            vetorVelocidade = (pos - pos_anterior).normalized*response.velocity;
            yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((vetorVelocidade/3600)*(raio/6400)*Time.deltaTime , Space.World);
    }



}
public class ISSResponse
{
    public string name;
    public int id;
    public float latitude;
    public float longitude;
    public float altitude;
    public float velocity;
    public string visibility;
    public string footprint;
    public long timestamp;
    public float daynum;
    public float solar_lat;
    public float solar_lon;
    public string units;
}
