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
    public partial class ActivitySetting : UserControl
    {
        public void ApplyCulture()
        {
            tbActivityName.Text = "Text";
            tbActivityType.Text = "Orientation";
            btnAppay.Content = "Apply";
            btnClose.Content = "Cancel";
            btnSave.Content = "OK";
            tbMergePictureRepeatDirection.Text = "Orientation";
            initActivityList();
            initMergePictureRepeatDirection();

            if (currentActivity != null)
            {

                initSetting(currentActivity.ActivityData);
            }
        }
        Activity currentActivity;
        public void SetSetting(Activity a)
        {
            this.Visibility = Visibility.Visible;
            this.ShowDisplayAutomation();
            //if (a == currentActivity)
            //    return;
            clearSetting();
            initSetting(a.ActivityData);
            currentActivity = a;
        }
        void clearSetting()
        {
            txtActivityName.Text = "";
            txtPositionX.Text = "";
            txtPositionY.Text = "";
            cbActivityType.SelectedIndex = -1;
        }
        void initSetting(ActivityComponent ac)
        {
            txtActivityName.Text = ac.ActivityName;
            txtPositionX.Text = ac.PositionX.ToString();
            txtPositionY.Text = ac.PositionY.ToString();
            string name = "";
            double size = 0;
            for (int i = 0; i < cbActivityType.Items.Count; i++)
            {
                name = ((ActivityTypeItem)cbActivityType.Items[i]).Name;

                if (name == ac.ActivityType)
                {
                    cbActivityType.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < cbFontSize.Items.Count; i++)
            {
                size = ((FontSizeItem)cbFontSize.Items[i]).Size;

                if (size == ac.FontSize)
                {
                    cbFontSize.SelectedIndex = i;
                    break;
                }
            }

            for (int i = 0; i < cbMergePictureRepeatDirection.Items.Count; i++)
            {
                name = ((RepeatDirectionItem)cbMergePictureRepeatDirection.Items[i]).Name;

                if (name == ac.RepeatDirection)
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

            ActivityType t = (ActivityType)Enum.Parse(typeof(ActivityType), ac.ActivityType, true);
            if (t == ActivityType.OR_MERGE
                || t == ActivityType.AND_MERGE
                || t == ActivityType.VOTE_MERGE)
            {
                tbMergePictureRepeatDirection.Visibility = Visibility.Visible;
                cbMergePictureRepeatDirection.Visibility = Visibility.Visible;
            }
            else
            {
                tbMergePictureRepeatDirection.Visibility = Visibility.Collapsed;
                cbMergePictureRepeatDirection.Visibility = Visibility.Collapsed;
            }


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
        void initActivityList()
        {
            List<ActivityTypeItem> Cus = new List<ActivityTypeItem>();
            Cus.Add(new ActivityTypeItem("INTERACTION", "Horizontal"));
            Cus.Add(new ActivityTypeItem("COMPLETION", "Vertical"));
            //Cus.Add(new ActivityTypeItem("INTERACTION", Text.ActivityType_INTERACTION));
            //Cus.Add(new ActivityTypeItem("AND_BRANCH", Text.ActivityType_AND_BRANCH));
            //Cus.Add(new ActivityTypeItem("OR_BRANCH", Text.ActivityType_OR_BRANCH));
            //Cus.Add(new ActivityTypeItem("AND_MERGE", Text.ActivityType_AND_MERGE));
            //Cus.Add(new ActivityTypeItem("OR_MERGE", Text.ActivityType_OR_MERGE));
            //Cus.Add(new ActivityTypeItem("VOTE_MERGE", Text.ActivityType_VOTE_MERGE));
            //Cus.Add(new ActivityTypeItem("AUTOMATION", Text.ActivityType_AUTOMATION));
            //Cus.Add(new ActivityTypeItem("INITIAL", Text.ActivityType_INITIAL));
            //Cus.Add(new ActivityTypeItem("COMPLETION", Text.ActivityType_COMPLETION));
            //// Cus.Add(new ActivityTypeItem("DUMMY", Text.ActivityType_DUMMY));
            //Cus.Add(new ActivityTypeItem("SUBPROCESS", Text.ActivityType_SUBPROCESS));
            cbActivityType.ItemsSource = Cus;
        }
        void initFontSizeList()
        {
            List<FontSizeItem> Cus = new List<FontSizeItem>();
            Cus.Add(new FontSizeItem("Small（6 points）", 18));
            Cus.Add(new FontSizeItem("Normal（8 points）", 24));
            Cus.Add(new FontSizeItem("Large（10 points）", 30));
            cbFontSize.SelectedIndex = 1;
            cbFontSize.ItemsSource = Cus;
        }
        public ActivitySetting()
        {

            InitializeComponent();
            initActivityList();
            initMergePictureRepeatDirection();
            initFontSizeList();


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
        bool workflowListIsCreated = false;

        public class WorkflowListItem
        {
            public string Name { get; set; }
            public string ID { get; set; }
            public WorkflowListItem()
            {
            }
            public WorkflowListItem(string name, string id)
            {
                Name = name;
                ID = id;
            }
        }


        public class ActivityTypeItem
        {
            public string Name { get; set; }
            public string Text { get; set; }
            public ActivityTypeItem(string name, string text)
            {
                Name = name;
                Text = text;
            }
        }
        /// <summary>
        /// 定义一个修改字体大小的字体大小属性
        /// </summary>
        /// <returns></returns>
        public class FontSizeItem
        {
            public string Name { get; set; }
            public int Size { get; set; }
            public FontSizeItem(string name, int size)
            {
                Name = name;
                Size = size;
            }
        }

        ActivityComponent getActivityData()
        {
            ActivityComponent ac = new ActivityComponent();
            ac.ActivityName = txtActivityName.Text;
            //加判断
            ac.PositionX = Convert.ToDouble(txtPositionX.Text);
            ac.PositionY = Convert.ToDouble(txtPositionY.Text);
            if (cbActivityType.SelectedIndex >= 0)
            {
                ActivityTypeItem cbi = cbActivityType.SelectedItem as ActivityTypeItem;
                if (cbi != null)
                {
                    ac.ActivityType = cbi.Name;
                }

            }
            if (cbMergePictureRepeatDirection.SelectedIndex >= 0)
            {
                RepeatDirectionItem cbi = cbMergePictureRepeatDirection.SelectedItem as RepeatDirectionItem;
                if (cbi != null)
                {
                    ac.RepeatDirection = cbi.Name;
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
            if (cbFontSize.SelectedIndex >= 0)
            {
                FontSizeItem cbi = cbFontSize.SelectedItem as FontSizeItem;
                if (cbi != null)
                {
                    ac.FontSize = cbi.Size;
                }

            }
            return ac;
        }
        public void ShowDisplayAutomation()
        {
            Storyboard story = (Storyboard)LayoutRoot.FindResource("sbActivitySettingDisplay");
            story.Begin();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

            close();
        }
        void close()
        {
            Storyboard story = (Storyboard)LayoutRoot.FindResource("sbActivitySettingClose");
            story.Completed += new EventHandler(sbActivitySettingClose_Completed);
            story.Begin();
        }
        void sbActivitySettingClose_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            currentActivity.SetActivityData(getActivityData());
            close();


        }
        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            currentActivity.SetActivityData(getActivityData());

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
            if (cbActivityType.SelectedItem != null)
            {
                ActivityType t = (ActivityType)Enum.Parse(typeof(ActivityType), ((ActivityTypeItem)cbActivityType.SelectedItem).Name, true);
                if (t == ActivityType.OR_MERGE
                    || t == ActivityType.AND_MERGE
                    || t == ActivityType.VOTE_MERGE)
                {
                    tbMergePictureRepeatDirection.Visibility = Visibility.Visible;
                    cbMergePictureRepeatDirection.Visibility = Visibility.Visible;
                }
                else
                {
                    tbMergePictureRepeatDirection.Visibility = Visibility.Collapsed;
                    cbMergePictureRepeatDirection.Visibility = Visibility.Collapsed;
                }
            }
        }

    }
}
