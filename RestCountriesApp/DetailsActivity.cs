using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestCountriesApp
{
    [Activity(Label = "DetailsActivity")]
    public class DetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.detailsLayout);
            var name = Intent.GetStringExtra("name");
            var region = Intent.GetStringExtra("region");
            var capital = Intent.GetStringExtra("capital");
            var languages = Intent.GetStringArrayExtra("languages");

            var nameTextView = FindViewById<TextView>(Resource.Id.name);
            nameTextView.Text = name;
            var regionTextView = FindViewById<TextView>(Resource.Id.region);
            regionTextView.Text = region;
            var capitalTextView = FindViewById<TextView>(Resource.Id.capital);
            capitalTextView.Text = capital;
            var languagesListView = FindViewById<ListView>(Resource.Id.languagesList);
            languagesListView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, languages.Where(i => !String.IsNullOrEmpty(i)).ToArray());
            // Create your application here
        }
    }
}