using System.Net;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace SPNETAndroid;

[Activity(Label = "TranslationHisctoryActivity")]
public class TranslationHisctoryActivity : ListActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Create your application here
        //await Task.Delay(2000);
        //using (HttpClient client = new HttpClient())
        //{

        //}
        //var phoneNumbers = Intent.Extras.GetStringArrayList("phone_numbers") ?? new string[0];
        //this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, phoneNumbers);

        SearchTwitter("maui");
    }

    internal void SearchTwitter(string text)
    {
        string searchUrl = string.Format("http://search.twitter.com/search.json?" + "q={0}&rpp=10&include_entities=false&" + "result_type=mixed", text);
        var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(searchUrl));
        httpReq.BeginGetResponse(new AsyncCallback(ResponseCallback), httpReq);
    }

    void ResponseCallback(IAsyncResult ar)
    {
        var httpReq = (HttpWebRequest)ar.AsyncState;

        using (var httpRes = (HttpWebResponse)httpReq.EndGetResponse(ar))
        {
            ParseResults(httpRes);
        }
    }

    void ParseResults(HttpWebResponse httpRes)
    {
        var s = httpRes.GetResponseStream();
        var j = JsonSerializer.Deserialize<JsonObject>(s);

        var results = (from result in (JsonArray)j["results"]
                       let jResult = result as JsonObject
                       select
                       jResult["text"].ToString()).ToArray();

        RunOnUiThread(() =>
        {
            PopulateTweetList(results);
        });
    }

    void PopulateTweetList(string[] results)
        => ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, results);
}