namespace SPNETAndroid
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        static readonly List<string> phoneNumbers = new List<string>();
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.phoneNumberText);
            TextView translatedPhoneWord = FindViewById<TextView>(Resource.Id.translatedPhoneword);
            Button translatedButton = FindViewById<Button>(Resource.Id.translateButton);
            Button translationHistoryButton = FindViewById<Button>(Resource.Id.translationHistoryButton);

            translatedButton.Click += (sender, e) =>
            {
                string translatedNumber = PhoneTranslator.ToNumber(phoneNumberText.Text);
                if (string.IsNullOrWhiteSpace(translatedNumber))
                    translatedPhoneWord.Text = string.Empty;
                else
                {
                    translatedPhoneWord.Text = translatedNumber;
                    phoneNumbers.Add(translatedNumber);
                    translationHistoryButton.Enabled = true;
                }
            };

            translationHistoryButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(TranslationHisctoryActivity));
                intent.PutStringArrayListExtra("phone_numbers", phoneNumbers);
                StartActivity(intent);
            };
        }
    }
}