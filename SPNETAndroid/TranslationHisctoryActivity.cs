namespace SPNETAndroid;

[Activity(Label = "TranslationHisctoryActivity")]
public class TranslationHisctoryActivity : ListActivity
{
    protected override async void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        // Create your application here
        await Task.Delay(2000);
        using (HttpClient client = new HttpClient())
        {

        }
        var phoneNumbers = Intent.Extras.GetStringArrayList("phone_numbers") ?? new string[0];
        this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, phoneNumbers);
    }
}