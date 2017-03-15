using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LabelPrinter.ActivityPicture
{
    public interface IActivityPicture
    {
        double Opacity
        {
            set;
            get;
        }
        double PictureWidth
        {
            get;
            set;
        }
        double PictureHeight
        {
            get;
            set;
        }
        Brush Background
        {
            set;
            get;
        }
        Visibility PictureVisibility
        {
            set;
            get;
        }
        void ResetInitColor();
        void SetWarningColor();
        void SetSelectedColor();
        PointCollection ThisPointCollection { get; }

    }
}
