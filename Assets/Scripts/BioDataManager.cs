using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;

public class BioDataManager : MonoBehaviour
{
    private static readonly HttpClient client = new HttpClient();

    public async Task<string> FetchBioData()
    {
        var response = await client.GetAsync("https://api.terra.bio/v1/userdata");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return json; // Parse biodata as necessary
    }
}