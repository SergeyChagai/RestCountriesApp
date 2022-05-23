using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestCountriesApp.Views
{
    public class SimpleInfoView : View
    {
        public SimpleInfoView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public SimpleInfoView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        private void Initialize()
        {
        }
    }
}