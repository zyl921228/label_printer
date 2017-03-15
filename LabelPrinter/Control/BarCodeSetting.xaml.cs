using LabelPrinter.Component;
using LabelPrinter.Designer;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabelPrinter.Control
{
    public partial class BarCodeSetting : UserControl
    {

        public void ApplyCulture()
        {
            //btSubFlow.Text = Text.SubFlow;
            //tbBarCodeInfo.Text = Text.ActivityName;
            //tbActivityType.Text = Text.ActivityType;
            //btnAppay.Content = Text.Button_Apply;
            //btnClose.Content = Text.Button_Cancle;
            //btnSave.Content = Text.Button_OK;
            //tbMergePictureRepeatDirection.Text = Text.RepeatDirection;
            //initActivityList();
            //initMergePictureRepeatDirection();

            if (currentBarCode != null)
            {

                //initSetting(currentActivity.ActivityData);
            }
        }
        BarCode currentBarCode;
        public void SetSetting(BarCode b)
        {
            this.Visibility = Visibility.Visible;
            this.ShowDisplayAutomation();
            //if (a == currentActivity)
            //    return;
            clearSetting();
            initSetting(b.BarCodeData);
            currentBarCode = b;
        }
        void clearSetting()
        {
            txtBarCodeInfo.Text = "";
            txtPositionX.Text = "";
            txtPositionY.Text = "";
            cbBarCodeType.SelectedIndex = -1;
            cbMergePictureRepeatDirection.SelectedIndex = -1;
        }
        void initSetting(BarCodeComponent bc)
        {
            txtBarCodeInfo.Text = bc.BarCodeInfo;
            txtPositionX.Text = bc.PositionX.ToString();
            txtPositionY.Text = bc.PositionY.ToString();
            txtWidth.Text = bc.BarCodeWidth.ToString();
            txtHeight.Text = bc.BarCodeHeight.ToString();
            string name = "";
            double size = 0;
            for (int i = 0; i < cbBarCodeType.Items.Count; i++)
            {
                name = ((BarCodeTypeItem)cbBarCodeType.Items[i]).Name;

                if (name == bc.BarCodeType)
                {
                    cbBarCodeType.SelectedIndex = i;
                    break;
                }
            }


            for (int i = 0; i < cbMergePictureRepeatDirection.Items.Count; i++)
            {
                name = ((RepeatDirectionItem)cbMergePictureRepeatDirection.Items[i]).Name;

                if (name == bc.RepeatDirection)
                {
                    cbMergePictureRepeatDirection.SelectedIndex = i;
                    break;
                }
            }
            //for (int i = 0; i < cbSubFlowList.Items.Count; i++)
            //{
            //    name = ((WorkflowListItem)cbSubFlowList.Items[i]).ID;

            //    if (name == ac.SubFlow)
            //    {
            //        cbSubFlowList.SelectedIndex = i;
            //        break;
            //    }
            //}

            //ActivityType t = (ActivityType)Enum.Parse(typeof(ActivityType), bc.ActivityType, true);
            //if (t == ActivityType.OR_MERGE
            //    || t == ActivityType.AND_MERGE
            //    || t == ActivityType.VOTE_MERGE)
            //{
            //    tbMergePictureRepeatDirection.Visibility = Visibility.Visible;
            //    cbMergePictureRepeatDirection.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    tbMergePictureRepeatDirection.Visibility = Visibility.Collapsed;
            //    cbMergePictureRepeatDirection.Visibility = Visibility.Collapsed;
            //}


            //if (t == ActivityType.SUBPROCESS)
            //{
            //    btSubFlow.Visibility = Visibility.Visible;
            //    cbSubFlowList.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    btSubFlow.Visibility = Visibility.Collapsed;
            //    cbSubFlowList.Visibility = Visibility.Collapsed;
            //}

        }

        public class BarCodeTypeItem
        {
            public string Name { get; set; }
            public int Text { get; set; }
            public BarCodeTypeItem(string name, int text)
            {
                Name = name;
                Text = text;
            }
        }

        public class RepeatDirectionItem
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public RepeatDirectionItem()
            {
            }
            public RepeatDirectionItem(string name, string text)
            {
                Name = name;
                Text = text;
            }
        }
        void initMergePictureRepeatDirection()
        {
            List<RepeatDirectionItem> Cus = new List<RepeatDirectionItem>();

            Cus.Add(new RepeatDirectionItem("Horizontal", "Horizontal"));
            Cus.Add(new RepeatDirectionItem("Vertical", "Vertical"));
            cbMergePictureRepeatDirection.ItemsSource = Cus;
            cbMergePictureRepeatDirection.SelectedIndex = 0;
        }

        BarCodeComponent getBarCodeData()
        {
            BarCodeComponent bc = new BarCodeComponent();
            bc.BarCodeInfo = txtBarCodeInfo.Text;
            //加判断
            bc.PositionX = Convert.ToDouble(txtPositionX.Text);
            bc.PositionY = Convert.ToDouble(txtPositionY.Text);
            bc.BarCodeWidth = Convert.ToInt32(txtWidth.Text);
            bc.BarCodeHeight = Convert.ToInt32(txtHeight.Text);
            if (cbBarCodeType.SelectedIndex >= 0)
            {
                BarCodeTypeItem cbi = cbBarCodeType.SelectedItem as BarCodeTypeItem;
                if (cbi != null)
                {
                    bc.BarCodeType = cbi.Name;
                }

            }
            if (cbMergePictureRepeatDirection.SelectedIndex >= 0)
            {
                RepeatDirectionItem cbi = cbMergePictureRepeatDirection.SelectedItem as RepeatDirectionItem;
                if (cbi != null)
                {
                    bc.RepeatDirection = cbi.Name;
                }

            }
            //if (cbSubFlowList.SelectedIndex >= 0)
            //{
            //    WorkflowListItem cbi = cbSubFlowList.SelectedItem as WorkflowListItem;
            //    if (cbi != null)
            //    {
            //        ac.SubFlow = cbi.ID;
            //    }

            //}
            return bc;
        }
        public void ShowDisplayAutomation()
        {
            Storyboard story = (Storyboard)LayoutRoot.FindResource("sbBarCodeSettingDisplay");
            story.Begin();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

            close();
        }
        void close()
        {
            Storyboard story = (Storyboard)LayoutRoot.FindResource("sbBarCodeSettingClose");
            story.Completed += new EventHandler(sbBarCodeSettingClose_Completed);
            story.Begin();
        }
        void sbBarCodeSettingClose_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            currentBarCode.SetBarCodeData(getBarCodeData());
            close();


        }
        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            currentBarCode.SetBarCodeData(getBarCodeData());

        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            FrameworkElement element = sender as FrameworkElement;
            mousePosition = e.GetPosition(null);
            trackingMouseMove = true;
            if (null != element)
            {
                element.CaptureMouse();
                element.Cursor = Cursors.Hand;
            }

        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {



            FrameworkElement element = sender as FrameworkElement;
            trackingMouseMove = false;
            element.ReleaseMouseCapture();

            mousePosition.X = mousePosition.Y = 0;
            element.Cursor = null;


        }
        bool trackingMouseMove = false;
        Point mousePosition;


        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            element.Cursor = Cursors.Hand;
            if (trackingMouseMove)
            {
                double deltaV = e.GetPosition(null).Y - mousePosition.Y;
                double deltaH = e.GetPosition(null).X - mousePosition.X;
                double newTop = deltaV + (double)this.GetValue(Canvas.TopProperty);
                double newLeft = deltaH + (double)this.GetValue(Canvas.LeftProperty);

                double containerWidth = (double)this.Parent.GetValue(Canvas.WidthProperty);
                double containerHeight = (double)this.Parent.GetValue(Canvas.HeightProperty);
                if (newLeft + this.Width > containerWidth
                   || newTop + this.Height > containerHeight
                    || newLeft < 0
                    || newTop < 0
                    )
                {
                    //超过流程容器的范围
                }
                else
                {



                    this.SetValue(Canvas.TopProperty, newTop);
                    this.SetValue(Canvas.LeftProperty, newLeft);

                    mousePosition = e.GetPosition(null);
                }
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBarCodeType.SelectedItem != null)
            {

            }
        }
        void initBarCodeList()
        {
            List<BarCodeTypeItem> Cus = new List<BarCodeTypeItem>();
            Cus.Add(new BarCodeTypeItem("Code39", 39));
            //Cus.Add(new BarCodeTypeItem("Code128", 128));


            cbBarCodeType.ItemsSource = Cus;
            cbBarCodeType.SelectedIndex = 0;
        }
        public BarCodeSetting()
        {
            InitializeComponent();
            initBarCodeList();
            initMergePictureRepeatDirection();
        }
    }
}
