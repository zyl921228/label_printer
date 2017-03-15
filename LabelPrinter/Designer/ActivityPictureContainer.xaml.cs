using LabelPrinter.ActivityPicture;
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

namespace LabelPrinter.Designer
{
    public delegate void ColseAnimationCompletedDelegate(object sender, EventArgs e);

    public partial class ActivityPictureContainer : UserControl
    {
        public double ContainerWidth
        {
            set
            {

                gridContainer.Width = value;
            }
            get
            {
                return gridContainer.Width;
            }
        }
        public double ContainerHeight
        {
            set
            {

                gridContainer.Height = value;
            }
            get
            {
                return gridContainer.Height;
            }
        }

        public double PictureWidth
        {
            get
            {
                return ((IActivityPicture)currentPic).PictureWidth;
            }
            set
            {
                ((IActivityPicture)currentPic).PictureWidth = value;
            }
        }
        public double PictureHeight
        {
            get
            {
                return ((IActivityPicture)currentPic).PictureHeight;
            }
            set
            {
                ((IActivityPicture)currentPic).PictureHeight = value;
            }
        }

        UserControl currentPic;
        public ActivityPictureContainer()
        {
            InitializeComponent();
        }
        public new SolidColorBrush Background
        {
            set
            {
                ((IActivityPicture)currentPic).Background = value;

            }
        }
        ActivityType type;
        public ActivityType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;


                picINTERACTION.PictureVisibility = Visibility.Collapsed;

                txtActivityName.Visibility = Visibility.Visible;
                txtActivityNameV.Visibility = Visibility.Collapsed;

                if (type == ActivityType.INTERACTION)
                {
                    currentPic = picINTERACTION;
                    txtActivityName.Visibility = Visibility.Visible;
                    txtActivityNameV.Visibility = Visibility.Collapsed;


                }
                else if (type == ActivityType.COMPLETION)
                {
                    currentPic = picINTERACTION;
                    txtActivityName.Visibility = Visibility.Collapsed;
                    txtActivityNameV.Visibility = Visibility.Visible;

                }

                ((IActivityPicture)currentPic).PictureVisibility = Visibility.Visible;

            }
        }
        public double FontSize
        {
            get { return txtActivityName.FontSize; }
            set
            {
                txtActivityName.FontSize = value;
                txtActivityNameV.FontSize = value;
            }
        }
        public string AcitivtyName
        {
            get { return txtActivityName.Text; }
            set
            {
                txtActivityName.Text = value;
                txtActivityNameV.Text = value;
            }
        }
        public void SetSelectedColor()
        {
            ((IActivityPicture)currentPic).SetSelectedColor();

        }
        public void SetWarningColor()
        {
            ((IActivityPicture)currentPic).SetWarningColor();

        }
        public void ResetInitColor()
        {
            ((IActivityPicture)currentPic).ResetInitColor();
        }
        public PointCollection ThisPointCollection
        {
            get
            {
                return ((IActivityPicture)currentPic).ThisPointCollection;
            }
        }
    }
}
