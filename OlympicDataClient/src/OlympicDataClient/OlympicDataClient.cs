using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OlympicDataClient
{
    public class OlympicDataClient
    {
        public OlympicDataClient(string baseUrl)
        {
            httpClient = new HttpClient();
            BaseUrl = baseUrl.TrimEnd ('/');
        }

        public string BaseUrl { get; private set; }

        HttpClient httpClient;

        public async Task<MedalsResponse> GetMedalsAsync ()
        {
            var json = await httpClient.GetStringAsync(BaseUrl + "/medals");

            return await Task.Run(() => {
               return JsonConvert.DeserializeObject<MedalsResponse>(json);
           });
        }

        public async Task<CountryMedalsResponse> GetMedalsAsync (string countryCode)
        {
            var json = await httpClient.GetStringAsync(BaseUrl + "/medals/" + countryCode);

            return await Task.Run(() => {
                return JsonConvert.DeserializeObject<CountryMedalsResponse>(json);
            });
        }
    }

    public class Medal
    {
        [JsonProperty ("flag")]
        public string FlagUrl { get; set; }

        [JsonProperty ("noc")]
        public string CountryCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("bronze")]
        public int BronzeCount { get; set; }

        [JsonProperty("silver")]
        public int SilverCount { get; set; }

        [JsonProperty("gold")]
        public int GoldCount { get; set; }

        [JsonProperty("total")]
        public int TotalCount { get; set; }
    }

    public class MedalsResponse
    {
        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        [JsonProperty("totalCountries")]
        public int TotalCountries { get; set; }

        [JsonProperty("medals")]
        public List<Medal> Medals { get; set; }

        [JsonProperty("nextUpdate")]
        public DateTime NextUpdate { get; set; }
    }

    public class Event
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("discipline_code")]
        public string DisciplineCode { get; set; }

        [JsonProperty("discipline")]
        public string Discipline { get; set; }
    }

    public class Team
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("noc")]
        public string CountryCode { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("disipline_code")]
        public string DisciplineCode { get; set; }

        [JsonProperty("discipline")]
        public string Discipline { get; set; }
    }

    public class Athelete
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("noc")]
        public string CountryCode { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }
    }

    public class CountryMedalsResponse
    {
        [JsonProperty("competitor_type")]
        public string CompetitorType { get; set; }

        [JsonProperty("event")]
        public Event Event { get; set; }

        [JsonProperty("team")]
        public Team Team { get; set; }

        [JsonProperty("medal")]
        public string Medal { get; set; }

        [JsonProperty("athelete")]
        public Athelete Athelete { get; set; }
    }
}
