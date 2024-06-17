///----------------------------------------------------------------------------
///   Module:       YouTube Service
///   Author:       NuboHeimer (https://vkplay.live/nuboheimer)
///   Email:        nuboheimer@yandex.ru
///   Telegram:     t.me/nuboheimer
///   Version:      0.1.1
///----------------------------------------------------------------------------
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

public class CPHInline
{
    public bool GetFollowersCount()
    {

        string youTubeApiKey = args["youTubeApiKey"].ToString();
        string channelId = args["youTubeCreatorChannelID"].ToString();
        string YouTubeApiHost = "https://www.googleapis.com/youtube/v3";
        string EndpointGetChannelInfo = "/channels?part=statistics&id={0}&key={1}";

        string url = string.Format(YouTubeApiHost + EndpointGetChannelInfo, channelId, youTubeApiKey);

        HttpClient client = new HttpClient();
        HttpResponseMessage response = client.GetAsync(url).Result;

        if (response.IsSuccessStatusCode)
        {
            string responseContent = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<YouTubeData>(responseContent);
            string youTubeFollowerCount = data.items[0].statistics.subscriberCount;
            CPH.SetArgument("youTubeFollowerCount", youTubeFollowerCount);
        }
        else
        {
            CPH.LogInfo("Failed to retrieve subscriber count.");
        }
        return true;
    }
}

public class YouTubeData
{
    public List<ItemsData> items { get; set; }
}

public class ItemsData
{
    public StatisticsData statistics { get; set; }
}

public class StatisticsData
{
    public string subscriberCount { get; set; }
}