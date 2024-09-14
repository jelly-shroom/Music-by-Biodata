using UnityEngine;
using System.Threading.Tasks;
using System.Net.Http;

public class BioDataManager : MonoBehaviour
{
    private readonly HttpClient client = new HttpClient();
    private const string TERRA_API_ENDPOINT = "https://api.tryterra.co/v2/"; // Replace with actual Terra API endpoint

    public async Task<string> FetchBioData()
    {
        Debug.Log("BioDataManager: Fetching bio data");

        try
        {
            var response = await client.GetAsync(TERRA_API_ENDPOINT + "heartRate");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Debug.Log($"BioDataManager: Received bio data: {responseString}");
            return responseString;
        }
        catch (HttpRequestException e)
        {
            Debug.LogError($"BioDataManager: Error fetching bio data: {e.Message}");
            return null;
        }
    }
}