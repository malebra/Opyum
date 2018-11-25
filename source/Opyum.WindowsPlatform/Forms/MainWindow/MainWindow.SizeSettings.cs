using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Opyum.WindowsPlatform
{
    public partial class MainWindow 
    {
        private Size _oldSize { get; set; }

        void SizeSettings(object sender, EventArgs e)
        {
            this.SizeChanged -= SizeSettings;

            if (this.Size.Width != _oldSize.Width)
            {
                WidthAdjustment();
            }
            else
            {
                HeightAdjustment();
            }
            _oldSize = this.Size;
            this.SizeChanged += SizeSettings;
        }

		void HeightAdjustment()
        {
            double w = this.Size.Height * WindowRatio.Ratio;
            double h = w * (1 / WindowRatio.Ratio);

            //if ((int)w/(int)h == WindowRatio.Ratio)
            //{
                this.Width = (int)w;
                this.Height = (int)h;
            //}
            

            //if ((increase && (w <= _oldSize.Width)))
            //{
            //    this.Size = _oldSize;
            //}
            //else if ((!increase && (w >= _oldSize.Width)))
            //{
            //    this.Size = _oldSize;
            //}
            //if (!(increase && (w <= _oldSize.Width)))
            //{
            //    //this.Size = _oldSize;
            //    base.Width = (int)w;
            //}
            //else if (!(!increase && (w >= _oldSize.Width)))
            //{
            //    //this.Size = _oldSize;
            //    base.Width = (int)w;
            //}


            
        }

		void WidthAdjustment()
        {
            base.Height = (int)((double)this.Size.Width * (double)(1 / WindowRatio.Ratio));
            base.Width = (int)((double)this.Size.Height * WindowRatio.Ratio);
            _oldSize = this.Size;
        }
    }
}
