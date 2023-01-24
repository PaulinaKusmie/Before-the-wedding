using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Before_the_wedding.Services
{
    public class MyTab : TabBar
    {
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == "CurrentItem")
            {
                int index = this.Items.IndexOf(this.CurrentItem);
                if (index == 1)
                {
                    //CurrentItem.Items[1];

                    //Items.ij
                }

                if (index == 1)
                {
                    //handle the stuff
                }

            }
        }

    }
}
