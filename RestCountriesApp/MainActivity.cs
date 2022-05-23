using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using System.Net.Http;
using Newtonsoft.Json;
using RestCountriesApp.Models;
using System.Collections.Generic;
using System.Linq;
using Android.Widget;
using System.Threading.Tasks;
using Android.Content;

namespace RestCountriesApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private string[] countriesList;
        private List<CountryModel> data;
        private ListView _list;
        private ArrayAdapter<string> _adapter;

        private HttpClient httpClient;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://restcountries.com/")
            };
            await GetData();
            FillData();
            var searchBar = FindViewById<Android.Widget.SearchView>(Resource.Id.searchView1);
            searchBar.QueryTextChange += (s, e) => _adapter.Filter.InvokeFilter(e.NewText);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private async Task GetData()
        {
            var response = await httpClient.GetAsync("v3.1/all");
            var result = await response.Content.ReadAsStringAsync();
            data = JsonConvert.DeserializeObject<List<CountryModel>>(result);
            countriesList = data.Select(c => c.Name.Official).OrderBy(n => n).ToArray();
        }

        private void FillData()
        {
            _list = FindViewById<ListView>(Resource.Id.countriesList);
            _adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, countriesList);
            _list.Adapter = _adapter;
            _list.ItemClick += OnListItemClicked;
        }

        private void OnListItemClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            var countryName = countriesList[e.Position];
            Intent intent = new Intent(this, typeof(DetailsActivity));
            var model = data.FirstOrDefault(i => i.Name.Official == countryName);
            intent.PutExtra("name", model.Name.Official);
            intent.PutExtra("region", model.Region);
            intent.PutExtra("capital", model.Capital?.FirstOrDefault());
            intent.PutExtra("languages", model.Languages?.LanguagesList.ToArray());
            StartActivity(intent);
            //var navigationIntent = new Intent()
        }
    }
}
