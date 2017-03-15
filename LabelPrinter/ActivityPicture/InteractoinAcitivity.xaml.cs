using LabelPrinter.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabelPrinter.ActivityPicture
{
    public partial class InteractoinAcitivity : UserControl, IActivityPicture
    {
        public InteractoinAcitivity()
        {
            InitializeComponent();
        }
        public new double Opacity
        {
            set { picINTERACTION.Opacity = value; }
            get { return picINTERACTION.Opacity; }
        }
        public double PictureWidth
        {
            set { picINTERACTION.Width = value; }
            get { return picINTERACTION.Width; }
        }
        public double PictureHeight
        {
            set { picINTERACTION.Height = value; }
            get { return picINTERACTION.Height; }
        }
        public new Brush Background
        {
            set { picINTERACTION.Fill = value; }
            get { return picINTERACTION.Fill; }
        }
        public Visibility PictureVisibility
        {
            set
            {

                this.Visibility = value;
            }
            get
            {

                return this.Visibility;
            }
        }
        public void ResetInitColor()
        {
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Colors.Transparent;
            picINTERACTION.Fill = brush;

        }
        public void SetWarningColor()
        {
            picINTERACTION.Fill = SystemConst.ColorConst.WarningColor;
        }
        public void SetSelectedColor()
        {
            picINTERACTION.Fill = SystemConst.ColorConst.SelectedColor;



        }
        public PointCollection ThisPointCollection
        {
            get { return null; }
        }
    }
}
