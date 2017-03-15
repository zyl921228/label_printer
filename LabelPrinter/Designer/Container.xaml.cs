using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Linq;

namespace LabelPrinter.Designer
{
    public partial class Container : UserControl, IContainer
    {

        public void SetGridLines()
        {
            if (!cbShowGridLines.IsChecked.HasValue || !cbShowGridLines.IsChecked.Value)
                return;
            GridLinesContainer.Children.Clear();
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color = Color.FromArgb(255, 160, 160, 160);
            //  brush.Color = Color.FromArgb(255, 255, 255, 255);
            double thickness = 0.3;
            double top = 0;
            double left = 0;

            double width = cnsDesignerContainer.Width;
            double height = cnsDesignerContainer.Height;

            double stepLength = 40;

            double x, y;
            x = left + stepLength;
            y = top;

            while (x < width + left)
            {
                Line line = new Line();
                line.X1 = x;
                line.Y1 = y;
                line.X2 = x;
                line.Y2 = y + height;



                line.Stroke = brush;
                line.StrokeThickness = thickness;
                line.Stretch = Stretch.Fill;
                GridLinesContainer.Children.Add(line);
                x += stepLength;
            }


            x = left;
            y = top + stepLength;

            while (y < height + top)
            {
                Line line = new Line();
                line.X1 = x;
                line.Y1 = y;
                line.X2 = x + width;
                line.Y2 = y;


                line.Stroke = brush;
                line.Stretch = Stretch.Fill;
                line.StrokeThickness = thickness;
                GridLinesContainer.Children.Add(line);
                y += stepLength;
            }


        }

        public bool Contains(UIElement uie)
        {
            return cnsDesignerContainer.Children.Contains(uie);
        }
        public CheckResult CheckSave()
        {
            CheckResult cr = new CheckResult();
            cr.IsPass = true;
            CheckResult temCR = null;
            IElement iel;
            bool hasInitial = false;
            bool hasCompledion = false;
            string msg = "";
            foreach (UIElement uic in cnsDesignerContainer.Children)
            {
                iel = uic as IElement;
                if (iel != null)
                {
                    temCR = iel.CheckSave();
                    if (!temCR.IsPass)
                    {
                        cr.IsPass = false;
                        cr.Message += temCR.Message;


                    }
                    if (iel.ElementType == WorkFlowElementType.Activity)
                    {
                        if (((Activity)uic).Type == ActivityType.INITIAL)
                        {
                            hasInitial = true;

                        }
                        else if (((Activity)uic).Type == ActivityType.COMPLETION)
                        {
                            hasCompledion = true;

                        }
                    }
                }
            }

            //if (!hasInitial)
            //{
            //    cr.IsPass = false;
            //    msg += Text.Message_MustHaveOnlyOneBeginActivity + "\r\n";
            //}
            //if (!hasCompledion)
            //{
            //    cr.IsPass = false;
            //    msg += Text.Message_MustHaveAtLeastOneEndActivity + "\r\n";
            //}
            if (string.IsNullOrEmpty(txtWorkFlowName.Text))
            {
                cr.IsPass = false;
                msg += "Label Name required\r\n\n";
            }
            msg += "Please follow the instruction above.";
            cr.Message = msg;
            return cr;
        }

        public string WorkFlowUrlID
        {
            get
            {
                //if (System.Windows.Browser.HtmlPage.Document.QueryString.ContainsKey("id"))
                //    return System.Windows.Browser.HtmlPage.Document.QueryString["id"].ToString();
                return "";
            }
        }

        public Container()
        {
            InitializeComponent();
            initSpeedList();
            initLightnessList();

            //System.Windows.Browser.HtmlPage.RegisterScriptableObject("WorkFlowDesignerContainer", this);  
            //sliZoom.Value = 1;
            MessageBody.Visibility = Visibility.Collapsed;
            XmlContainer.Visibility = Visibility.Collapsed;
            siActivitySetting.Visibility = Visibility.Collapsed;
            siBarCodeSetting.Visibility = Visibility.Collapsed;

            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            tbWidth.Text = ((int)(cnsDesignerContainer.Width / 8)).ToString();
            tbHeight.Text = ((int)(cnsDesignerContainer.Height / 8)).ToString();

            // if (EditType == PageEditType.Add)
            //createNewWorkFlow();
            // cbShowGridLines.IsChecked = true;
            SetGridLines();

            //ApplyCulture();

            //if (Configure.CurrentCulture.Name.ToLower() == "zh-cn")
            //{
            //    btnApplyChineseCulture.IsEnabled = false;
            //    btnApplyEnglishCulture.IsEnabled = true;

            //}
            //if (Configure.CurrentCulture.Name.ToLower() == "en-us")
            //{
            //    btnApplyChineseCulture.IsEnabled = true;
            //    btnApplyEnglishCulture.IsEnabled = false;
            //}
            _doubleClickTimer = new System.Windows.Threading.DispatcherTimer();
            _doubleClickTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            _doubleClickTimer.Tick += new EventHandler(DoubleClick_Timer);
        }
        void initSpeedList()
        {
            List<int> Cus = new List<int>();
            Cus.Add(2);
            Cus.Add(3);
            Cus.Add(4);
            Cus.Add(5);
            cbPrintSpeed.ItemsSource = Cus;
            cbPrintSpeed.SelectedIndex = 2;
        }
        void initLightnessList()
        {
            List<int> Cus = new List<int>();
            Cus.Add(0);
            Cus.Add(1);
            Cus.Add(2);
            Cus.Add(3);
            Cus.Add(4);
            Cus.Add(5);
            Cus.Add(6);
            Cus.Add(7);
            Cus.Add(8);
            Cus.Add(9);
            Cus.Add(10);
            Cus.Add(11);
            Cus.Add(12);
            Cus.Add(13);
            Cus.Add(14);
            Cus.Add(15);
            Cus.Add(16);
            Cus.Add(17);
            Cus.Add(18);
            Cus.Add(19);
            cbPrintLightness.ItemsSource = Cus;
            cbPrintLightness.SelectedIndex = 8;
        }
        void DoubleClick_Timer(object sender, EventArgs e)
        {
            _doubleClickTimer.Stop();
        }
        PageEditType editType = PageEditType.None;
        public PageEditType EditType
        {
            get
            {
                if (editType == PageEditType.None)
                {
                    editType = PageEditType.Add;
                }
                return editType;
            }
            set
            {
                editType = value;
            }
        }

        public void ShowActivitySetting(Activity a)
        {

            siActivitySetting.SetSetting(a);

            // sbActivitySettingDisplay.Begin();
        }
        public void ShowBarCodeSetting(BarCode b)
        {

            siBarCodeSetting.SetSetting(b);

            // sbActivitySettingDisplay.Begin();
        }

        int nextNewActivityIndex = 0;
        public int NextNewActivityIndex
        {
            get
            {
                return ++nextNewActivityIndex;
            }
        }
        int nextNewRuleIndex = 0;
        public int NextNewRuleIndex
        {
            get
            {
                return ++nextNewRuleIndex;
            }
        }
        int nextNewLabelIndex = 0;
        public int NextNewLabelIndex
        {
            get
            {
                return ++nextNewLabelIndex;
            }
        }
        public void LoadFromXmlString(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return;
            //Activity Reader
            ActivityType activityType;
            MergePictureRepeatDirection repeatDirection = MergePictureRepeatDirection.None;
            string uniqueID = "";
            int zIndex = 0;
            string activityID = "";
            string activityName = "";
            double fontSize = 24;
            Point activityPosition = new Point();
            double temd = 0;
            Byte[] b = System.Text.UTF8Encoding.UTF8.GetBytes(xml);
            XElement xele = XElement.Load(System.Xml.XmlReader.Create(new MemoryStream(b)));
            txtWorkFlowName.Text = xele.Attribute(XName.Get("Name")).Value;
            PrintLightness = Convert.ToInt32(xele.Attribute(XName.Get("Lightness")).Value);
            PrintSpeed = Convert.ToInt32(xele.Attribute(XName.Get("Speed")).Value);
            PrintNums = Convert.ToInt32(xele.Attribute(XName.Get("Nums")).Value);
            ContainerWidth = Convert.ToDouble(xele.Attribute(XName.Get("Width")).Value);
            ContainerHeight = Convert.ToDouble(xele.Attribute(XName.Get("Height")).Value);
            UniqueID = xele.Attribute(XName.Get("UniqueID")).Value;

            var partNos = from item in xele.Descendants("Activity") select item;
            foreach (XElement node in partNos)
            {

                activityType = (ActivityType)Enum.Parse(typeof(ActivityType), node.Attribute(XName.Get("Type")).Value, true);
                //try
                //{
                //    repeatDirection = (MergePictureRepeatDirection)Enum.Parse(typeof(MergePictureRepeatDirection), node.Attribute(XName.Get("RepeatDirection")).Value, true);

                //}
                //catch (Exception e) { }
                uniqueID = node.Attribute(XName.Get("UniqueID")).Value;
                activityID = node.Attribute(XName.Get("ActivityID")).Value;
                activityName = node.Attribute(XName.Get("ActivityName")).Value;
                fontSize = Convert.ToDouble(node.Attribute(XName.Get("FontSize")).Value);

                double.TryParse(node.Attribute(XName.Get("PositionX")).Value, out temd);
                activityPosition.X = temd;
                double.TryParse(node.Attribute(XName.Get("PositionY")).Value, out temd);
                activityPosition.Y = temd;
                int.TryParse(node.Attribute(XName.Get("ZIndex")).Value, out zIndex);
                double _fontsize = fontSize;//temp
                Activity a = new Activity((IContainer)this, activityType, _fontsize);
                a.CenterPoint = activityPosition;
                a.ActivityID = activityID;
                a.ActivityName = activityName;
                a.sdPicture.FontSize = fontSize;
                a.ZIndex = zIndex;
                a.EditType = this.EditType;
                a.UniqueID = uniqueID;

                AddActivity(a);


            }

            //BarCode Reader
            BarCodeType barCodeType;
            repeatDirection = MergePictureRepeatDirection.None;
            string barCodeID = "";
            string barCodeInfo = "";
            Point barCodePosition = new Point();
            double barCodeX = 0;
            double barCodeY = 0;
            double barCodeW = 0;
            double barCodeH = 0;
            partNos = from item in xele.Descendants("BarCode") select item;
            foreach (XElement node in partNos)
            {

                barCodeType = (BarCodeType)Enum.Parse(typeof(BarCodeType), node.Attribute(XName.Get("Type")).Value, true);
                try
                {
                    repeatDirection = (MergePictureRepeatDirection)Enum.Parse(typeof(MergePictureRepeatDirection), node.Attribute(XName.Get("RepeatDirection")).Value, true);

                }
                catch (Exception e) { }
                uniqueID = node.Attribute(XName.Get("UniqueID")).Value;
                barCodeID = node.Attribute(XName.Get("BarCodeID")).Value;
                barCodeInfo = node.Attribute(XName.Get("BarCodeInfo")).Value;

                double.TryParse(node.Attribute(XName.Get("PositionX")).Value, out barCodeX);
                barCodePosition.X = barCodeX;
                double.TryParse(node.Attribute(XName.Get("PositionY")).Value, out barCodeY);
                barCodePosition.Y = barCodeY;
                double.TryParse(node.Attribute(XName.Get("BarCodeWidth")).Value, out barCodeW);
                double.TryParse(node.Attribute(XName.Get("BarCodeHeight")).Value, out barCodeH);
                int.TryParse(node.Attribute(XName.Get("ZIndex")).Value, out zIndex);
                double _fontsize = fontSize;//temp
                BarCode bc = new BarCode((IContainer)this, barCodeType, barCodeInfo, (int)barCodeW, (int)barCodeH);
                bc.RepeatDirection = repeatDirection;
                bc.CenterPoint = barCodePosition;
                bc.BarCodeID = barCodeID;
                bc.BarCodeInfo = barCodeInfo;
                bc.BarCodeWidth = (int)barCodeW;
                bc.BarCodeHeight = (int)barCodeH;
                bc.ZIndex = zIndex;
                bc.EditType = this.EditType;
                bc.UniqueID = uniqueID;
                //
                if (bc.RepeatDirection == MergePictureRepeatDirection.Vertical)
                {
                    bc.gridTransform.Angle = 90;
                    //bc.gridTransform.TranslateX = bc.BarCodeHeight + 16;
                }
                else
                {
                    bc.gridTransform.Angle = 0;
                    //bc.gridTransform.TranslateX = 0;
                }
                //
                AddBarCode(bc);


            }

        }
        string uniqueID;
        public string UniqueID
        {
            get
            {
                if (string.IsNullOrEmpty(uniqueID))
                {
                    uniqueID = Guid.NewGuid().ToString();
                }
                return uniqueID;
            }
            set
            {
                uniqueID = value;
            }

        }
        public Double ContainerWidth
        {
            get
            {
                return cnsDesignerContainer.Width;
            }
            set
            {
                cnsDesignerContainer.Width = value;
                tbWidth.Text = ((int)(value / 8)).ToString();
            }
        }
        public Double ContainerHeight
        {
            get
            {
                return cnsDesignerContainer.Height;
            }
            set
            {
                cnsDesignerContainer.Height = value;
                tbHeight.Text = ((int)(value / 8)).ToString();

            }
        }

        public Double ScrollViewerHorizontalOffset
        {
            get
            {
                return svContainer.HorizontalOffset;
            }
            set
            {
                svContainer.ScrollToHorizontalOffset(value);

            }
        }
        public Double ScrollViewerVerticalOffset
        {
            get
            {
                return svContainer.VerticalOffset;
            }
            set
            {
                svContainer.ScrollToVerticalOffset(value);
            }
        }
        public string ToXmlString()
        {
            System.Text.StringBuilder xml = new System.Text.StringBuilder(@"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes"" ?>
<!--
Description file of Label Printer
-->");
            xml.Append(Environment.NewLine);
            xml.Append(@"<WorkFlow");
            xml.Append(@" UniqueID=""" + UniqueID + @"""");
            xml.Append(@" ID=""" + Guid.NewGuid().ToString() + @"""");
            xml.Append(@" Name=""" + txtWorkFlowName.Text + @"""");
            xml.Append(@" Description=""" + txtWorkFlowName.Text + @"""");
            xml.Append(@" Lightness=""" + PrintLightness.ToString() + @"""");
            xml.Append(@" Speed=""" + PrintSpeed.ToString() + @"""");
            xml.Append(@" Nums=""" + PrintNums.ToString() + @"""");
            xml.Append(@" Width=""" + ContainerWidth.ToString() + @"""");
            xml.Append(@" Height=""" + ContainerHeight.ToString() + @""">");



            System.Text.StringBuilder activityXml = new System.Text.StringBuilder("    <Activitys>");
            System.Text.StringBuilder barCodeXml = new System.Text.StringBuilder("    <BarCodes>");
            System.Text.StringBuilder ruleXml = new System.Text.StringBuilder("    <Rules>");
            System.Text.StringBuilder labelXml = new System.Text.StringBuilder("    <Labels>");

            IElement ele;
            foreach (UIElement c in cnsDesignerContainer.Children)
            {
                ele = c as IElement;
                if (ele != null)
                {
                    if (ele.IsDeleted)
                        continue;
                    if (ele.ElementType == WorkFlowElementType.Activity)
                    {
                        activityXml.Append(Environment.NewLine);
                        activityXml.Append(ele.ToXmlString());
                    }
                    else if (ele.ElementType == WorkFlowElementType.BarCode)
                    {
                        barCodeXml.Append(Environment.NewLine);
                        barCodeXml.Append(ele.ToXmlString());

                    }
                    else if (ele.ElementType == WorkFlowElementType.Rule)
                    {
                        ruleXml.Append(Environment.NewLine);
                        ruleXml.Append(ele.ToXmlString());

                    }
                    else if (ele.ElementType == WorkFlowElementType.Label)
                    {
                        labelXml.Append(Environment.NewLine);
                        labelXml.Append(ele.ToXmlString());

                    }
                }

            }
            activityXml.Append(Environment.NewLine);
            activityXml.Append("    </Activitys>");
            barCodeXml.Append(Environment.NewLine);
            barCodeXml.Append("    </BarCodes>");
            ruleXml.Append(Environment.NewLine);
            ruleXml.Append("    </Rules>");
            labelXml.Append(Environment.NewLine);
            labelXml.Append("    </Labels>");
            xml.Append(Environment.NewLine);
            xml.Append(activityXml.ToString());
            xml.Append(Environment.NewLine);
            xml.Append(barCodeXml.ToString());
            xml.Append(Environment.NewLine);
            xml.Append(ruleXml.ToString());
            xml.Append(Environment.NewLine);
            xml.Append(labelXml.ToString());
            xml.Append(Environment.NewLine);
            xml.Append(@"</WorkFlow>");
            return xml.ToString();

        }
        public string ToEZPLString()
        {
            string EZPLString = "";
            EZPLString += "^Q" + ((int)ContainerHeight / 8).ToString() + ",0,0-\n";
            EZPLString += "^W" + ((int)ContainerWidth / 8).ToString() + "\n";
            EZPLString += "^H" + PrintLightness.ToString() + "\n";
            EZPLString += "^S" + PrintSpeed.ToString() + "\n";
            EZPLString += "^P" + PrintNums.ToString() + "\n";

            string activityEZPL = "";
            string barCodeEZPL = "";

            IElement ele;
            foreach (UIElement c in cnsDesignerContainer.Children)
            {
                ele = c as IElement;
                if (ele != null)
                {
                    if (ele.IsDeleted)
                        continue;
                    if (ele.ElementType == WorkFlowElementType.Activity)
                    {
                        activityEZPL += ele.ToEZPLString();
                        activityEZPL += "\n";
                    }
                    else if (ele.ElementType == WorkFlowElementType.BarCode)
                    {
                        barCodeEZPL += ele.ToEZPLString();
                        barCodeEZPL += "\n";

                    }
                }

            }

            EZPLString += activityEZPL + barCodeEZPL;
            EZPLString += "E";

            return EZPLString;

        }



        public void OnActivityChanged(Activity a)
        {
            SaveChange(HistoryType.New);

        }

        public void OnBarCodeChanged(BarCode b)
        {
            SaveChange(HistoryType.New);

        }

        void createNewWorkFlow()
        {


            if (!string.IsNullOrEmpty(WorkFlowUrlID))
            {

                //workflowClient.GetWorkFlowXMLAsync(WorkFlowUrlID);

            }
            else
            {

                string beginActivityID = Guid.NewGuid().ToString();
                string endActivityID = Guid.NewGuid().ToString();
                string rule1ID = Guid.NewGuid().ToString();
                string rule2ID = Guid.NewGuid().ToString();
                string activtyID = Guid.NewGuid().ToString();
                string workflowID = Guid.NewGuid().ToString();

                string xml = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes"" ?>
<!--
Description file of Label Printer
-->
<WorkFlow UniqueID=""" + workflowID + @""" ID="""" Name="""" Description="""" Width=""980"" Height=""580"">
    <Activitys>
       <Activity  UniqueID=""" + beginActivityID + @""" ActivityID="""" ActivityName="""" Type=""INITIAL"" SubFlow="""" PositionX=""212"" PositionY=""126"" RepeatDirection=""None"" ZIndex=""24"">
        </Activity>
       <Activity  UniqueID=""" + endActivityID + @""" ActivityID="""" ActivityName="""" Type=""COMPLETION"" SubFlow="""" PositionX=""516"" PositionY=""124"" RepeatDirection=""None"" ZIndex=""25"">
        </Activity>
       <Activity  UniqueID=""" + workflowID + @""" ActivityID="""" ActivityName=""shareidea.net"" Type=""INTERACTION"" SubFlow="""" PositionX=""368"" PositionY=""124"" RepeatDirection=""None"" ZIndex=""20"">
        </Activity>
    </Activitys>
    <Rules>
       <Rule  UniqueID=""" + rule1ID + @""" RuleID=""" + rule1ID + @""" RuleName="""" LineType=""Line"" RuleCondition="""" BeginActivityUniqueID=""" + beginActivityID + @""" EndActivityUniqueID=""" + workflowID + @""" BeginActivityID="""" EndActivityID="""" BeginPointX=""235"" BeginPointY=""121"" EndPointX=""314"" EndPointY=""120"" TurnPoint1X=""0"" TurnPoint1Y=""0"" TurnPoint2X=""0"" TurnPoint2Y=""0"" ZIndex=""18"">
        </Rule>
       <Rule  UniqueID=""" + rule2ID + @""" RuleID=""" + rule2ID + @""" RuleName="""" LineType=""Line"" RuleCondition="""" BeginActivityUniqueID=""" + workflowID + @""" EndActivityUniqueID=""" + endActivityID + @""" BeginActivityID="""" EndActivityID="""" BeginPointX=""414"" BeginPointY=""120"" EndPointX=""485"" EndPointY=""119"" TurnPoint1X=""0"" TurnPoint1Y=""0"" TurnPoint2X=""0"" TurnPoint2Y=""0"" ZIndex=""17"">
        </Rule>
    </Rules>
</WorkFlow>";
                display(xml);
            }

        }
        void display(string xml)
        {
            LoadFromXmlString(xml);
            SaveChange(HistoryType.New);
        }
        //void wfClient_GetWorkFlowXMLCompleted(object sender, Shareidea.Web.UI.Control.Workflow.Designer.ServicesClient.GetWorkFlowXMLCompletedEventArgs e)
        //{
        //    if (e.Error != null)
        //        System.Windows.Browser.HtmlPage.Window.Alert(e.Error.Message);
        //    else
        //    {
        //        string xml = e.Result;
        //        if (string.IsNullOrEmpty(xml))
        //            System.Windows.Browser.HtmlPage.Window.Alert("您请求的流程不存在!");
        //        else
        //            display(xml);
        //    }
        //}
        public void AddActivity(Activity a)
        {
            if (!cnsDesignerContainer.Children.Contains(a))
            {
                cnsDesignerContainer.Children.Add(a);
                a.Container = this;
                a.ActivityChanged += new ActivityChangeDelegate(OnActivityChanged);
            }
            if (!ActivityCollections.Contains(a))
                ActivityCollections.Add(a);


        }

        public void AddBarCode(BarCode b)
        {
            if (!cnsDesignerContainer.Children.Contains(b))
            {
                cnsDesignerContainer.Children.Add(b);
                b.Container = this;
                b.BarCodeChanged += new BarCodeChangeDelegate(OnBarCodeChanged);
            }
            if (!BarCodeCollections.Contains(b))
                BarCodeCollections.Add(b);


        }

        public void RemoveActivity(Activity a)
        {

            if (cnsDesignerContainer.Children.Contains(a))
                cnsDesignerContainer.Children.Remove(a);
            if (ActivityCollections.Contains(a))
                ActivityCollections.Remove(a);
        }
        public void RemoveBarCode(BarCode b)
        {

            if (cnsDesignerContainer.Children.Contains(b))
                cnsDesignerContainer.Children.Remove(b);
            if (BarCodeCollections.Contains(b))
                BarCodeCollections.Remove(b);
        }

        public List<LabelPrinter.Designer.Activity> activityCollections;
        public List<LabelPrinter.Designer.Activity> ActivityCollections
        {
            get
            {
                if (activityCollections == null)
                {
                    activityCollections = new List<Activity>();
                }
                return activityCollections;
            }
        }

        public List<LabelPrinter.Designer.BarCode> barCodeCollections;
        public List<LabelPrinter.Designer.BarCode> BarCodeCollections
        {
            get
            {
                if (barCodeCollections == null)
                {
                    barCodeCollections = new List<BarCode>();
                }
                return barCodeCollections;
            }
        }

        int nextMaxIndex = 0;
        public int NextMaxIndex
        {
            get
            {
                nextMaxIndex++;
                return nextMaxIndex;
            }
        }

        public double Left
        {
            get
            {
                return 155;
            }
        }
        public double Top
        {
            get
            {
                return 40;
            }

        }

        public void ShowMessage(string message)
        {
            ShowContainerCover();
            MessageTitle.Text = message;
            MessageBody.Visibility = Visibility.Visible;
        }
        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Name";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(30, 20);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }
        private void AddActivity2_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Net weight";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(30, 50);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }
        private void AddActivity3_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Gross weight";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(30, 80);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }

        private void AddActivity4_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Quality";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(90, 20);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }
        private void AddActivity5_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Size";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(90, 50);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }
        private void AddActivity6_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Price";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(90, 80);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }
        private void AddActivity7_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Bar code";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(30, 110);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }
        private void AddActivity8_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Cert. Num";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(90, 110);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }
        private void AddActivity9_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Purity";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(30, 110);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }

        private void AddActivity10_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Color";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(150, 20);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }

        private void AddActivity11_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Cut";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(150, 50);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }

        private void AddActivity12_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Labor cost";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(150, 80);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }

        private void AddActivity13_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Original code";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(30, 140);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }

        private void AddActivity14_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.ActivityName = "Fixed price";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.Position = new Point(150, 140);
            AddActivity(a);
            SaveChange(HistoryType.New);
        }



        private void AddBarCode_Click(object sender, RoutedEventArgs e)
        {
            BarCode a = new BarCode((IContainer)this, BarCodeType.Code39, "123456", 1, 60);

            a.SetValue(Canvas.ZIndexProperty, NextMaxIndex);
            a.BarCodeInfo = "123456";//Text.NewActivity + NextNewActivityIndex.ToString();
            a.BarCodeWidth = 1;
            a.BarCodeHeight = 60;

            a.Position = new Point(30, 170);

            AddBarCode(a);
            SaveChange(HistoryType.New);
        }

        public void ShowContainerCover()
        {
            canContainerCover.Visibility = Visibility.Visible;
            Storyboard story = (Storyboard)canContainerCover.FindResource("sbContainerCoverDisplay");
            story.Begin();


        }
        public void CloseContainerCover()
        {
            Storyboard story = (Storyboard)canContainerCover.FindResource("sbContainerCoverClose");
            story.Completed += new EventHandler(sbContainerCoverClose_Completed);
            story.Begin();


        }

        void sbContainerCoverClose_Completed(object sender, EventArgs e)
        {
            canContainerCover.Visibility = Visibility.Collapsed;
        }


        private void btnCloseMessageButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBody.Visibility = Visibility.Collapsed;
            CloseContainerCover();
        }

        private void btnExportXml_Click(object sender, RoutedEventArgs e)
        {
            CheckResult cr = CheckSave();
            if (cr.IsPass)
            {
                ShowContainerCover();
                XmlContainer.Visibility = Visibility.Visible;
                btnCloseXml.Visibility = Visibility.Visible;
                btnImportXml.Visibility = Visibility.Collapsed;
                txtXml.Text = ToXmlString();
                txtEZPL.Text = ToEZPLString();
            }
            else
                ShowMessage(cr.Message);

        }

        private void btnShowXmlContainer_Click(object sender, RoutedEventArgs e)
        {



            ShowContainerCover();
            btnImportXml.Visibility = Visibility.Visible;
            XmlContainer.Visibility = Visibility.Visible;



        }

        //ServicesClient.WorkFlowSoapClient _workflowClient;
        //ServicesClient.WorkFlowSoapClient workflowClient
        //{
        //    get
        //    {
        //        if (_workflowClient == null)
        //        {
        //            System.ServiceModel.BasicHttpBinding bind = new System.ServiceModel.BasicHttpBinding();
        //            System.ServiceModel.EndpointAddress endpoint = new System.ServiceModel.EndpointAddress(
        //                new Uri(System.Windows.Browser.HtmlPage.Document.DocumentUri, "services/workflow.asmx"), null);
        //            _workflowClient = new Shareidea.Web.UI.Control.Workflow.Designer.ServicesClient.WorkFlowSoapClient(bind, endpoint);
        //            _workflowClient.UpdateWorkFlowXMLCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(wfClient_UpdateWorkFlowXMLCompleted);

        //            _workflowClient.GetWorkFlowXMLCompleted += new EventHandler<Shareidea.Web.UI.Control.Workflow.Designer.ServicesClient.GetWorkFlowXMLCompletedEventArgs>(wfClient_GetWorkFlowXMLCompleted);

        //        }
        //        return _workflowClient;
        //    }
        //}

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentSelectedControlCollection != null && CurrentSelectedControlCollection.Count > 0)
            {
                //if (System.Windows.Browser.HtmlPage.Window.Confirm(Text.Comfirm_Delete))
                //{
                DeleteSeletedControl();
                SaveChange(HistoryType.New);
                // }
            }
            //CheckResult cr = CheckSave();
            //if (cr.IsPass)
            //{
            //    // workflowClient.UpdateWorkFlowXMLAsync(ToXmlString(), null);
            //}
            //else
            //{
            //   // System.Windows.Browser.HtmlPage.Window.Alert(cr.Message);

            //}

        }
        void wfClient_UpdateWorkFlowXMLCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //System.Windows.Browser.HtmlPage.Window.Alert(Text.Message_Saved);
        }
        private void CloseXml_Click(object sender, RoutedEventArgs e)
        {
            XmlContainer.Visibility = Visibility.Collapsed;
            CloseContainerCover();
        }
        Activity getActivity(string activityUniqueID)
        {
            for (int i = 0; i < activityCollections.Count; i++)
            {
                if (activityCollections[i].UniqueID == activityUniqueID)
                {
                    return activityCollections[i];
                }
            }
            return null;
        }


        Canvas _gridLinesContainer;
        Canvas GridLinesContainer
        {
            get
            {
                if (_gridLinesContainer == null)
                {

                    Canvas temCan = new Canvas();
                    temCan.Name = "canGridLinesContainer";
                    cnsDesignerContainer.Children.Add(temCan);
                    _gridLinesContainer = temCan;

                }
                return _gridLinesContainer;
            }
        }
        void cleareContainer()
        {
            cnsDesignerContainer.Children.Clear();
            _gridLinesContainer = null;
            SetGridLines();
            activityCollections = null;
        }
        private void ClearContainer(object sender, RoutedEventArgs e)
        {
            cleareContainer();

            SaveChange(HistoryType.New);
        }
        private void ImportXml_Click(object sender, RoutedEventArgs e)
        {
            cleareContainer();
            XmlContainer.Visibility = Visibility.Collapsed;
            LoadFromXmlString(txtXml.Text);
            CloseContainerCover();


        }
        private void ExportTxt_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
            if (saveDlg.ShowDialog() == true)
            {
                using (Stream fs = saveDlg.OpenFile())
                {
                    Byte[] fileContent = Encoding.UTF8.GetBytes(txtEZPL.Text);
                    fs.Write(fileContent, 0, fileContent.Length);
                    fs.Close();
                }
            }


        }

        System.Collections.Generic.Stack<string> _workFlowXmlNextStack;
        public System.Collections.Generic.Stack<string> WorkFlowXmlNextStack
        {
            get
            {
                if (_workFlowXmlNextStack == null)
                    _workFlowXmlNextStack = new Stack<string>(50);
                return _workFlowXmlNextStack;
            }
        }
        System.Collections.Generic.Stack<string> _workFlowXmlPreStack;
        public System.Collections.Generic.Stack<string> WorkFlowXmlPreStack
        {
            get
            {
                if (_workFlowXmlPreStack == null)
                    _workFlowXmlPreStack = new Stack<string>(50);
                return _workFlowXmlPreStack;
            }
        }
        string workflowXmlCurrent = @"";
        void pushNextQueueToPreQueue()
        {
            if (WorkFlowXmlPreStack.Count > 0)
                WorkFlowXmlNextStack.Push(WorkFlowXmlPreStack.Pop());
            int cout = WorkFlowXmlNextStack.Count;

            for (int i = 0; i < cout; i++)
            {
                WorkFlowXmlPreStack.Push(WorkFlowXmlNextStack.Pop());
            }
        }

        public void SaveChange(HistoryType action)
        {

            if (action == HistoryType.New)
            {
                WorkFlowXmlPreStack.Push(workflowXmlCurrent);
                workflowXmlCurrent = ToXmlString();
                WorkFlowXmlNextStack.Clear();

            }
            if (action == HistoryType.Next)
            {
                if (WorkFlowXmlNextStack.Count > 0)
                {
                    WorkFlowXmlPreStack.Push(workflowXmlCurrent);
                    workflowXmlCurrent = WorkFlowXmlNextStack.Pop();
                    cleareContainer();
                    ClearSelectFlowElement(null);

                }

                LoadFromXmlString(workflowXmlCurrent);
            }
            if (action == HistoryType.Previous)
            {
                if (WorkFlowXmlPreStack.Count > 0)
                {
                    WorkFlowXmlNextStack.Push(workflowXmlCurrent);
                    workflowXmlCurrent = WorkFlowXmlPreStack.Pop();
                    cleareContainer();

                    LoadFromXmlString(workflowXmlCurrent);
                    ClearSelectFlowElement(null);

                }
            }
            setQueueButtonEnable();
            //SetGridLines();


        }
        void setQueueButtonEnable()
        {
            if (WorkFlowXmlPreStack.Count == 0)
            {
                btnPrevious.IsEnabled = false;
            }
            else
                btnPrevious.IsEnabled = true;

            if (WorkFlowXmlNextStack.Count == 0)
            {
                btnNext.IsEnabled = false;
            }
            else
                btnNext.IsEnabled = true;
        }

        public void PreviousAction()
        {
            SaveChange(HistoryType.Previous);

        }
        public void NextAction()
        {
            SaveChange(HistoryType.Next);

        }
        private void btnPre_Click(object sender, RoutedEventArgs e)
        {
            PreviousAction();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

            NextAction();
        }




        public void AddSelectedControl(System.Windows.Controls.Control uc)
        {
            if (!CurrentSelectedControlCollection.Contains(uc))
                CurrentSelectedControlCollection.Add(uc);
        }
        public void RemoveSelectedControl(System.Windows.Controls.Control uc)
        {
            if (CurrentSelectedControlCollection.Contains(uc))
                CurrentSelectedControlCollection.Remove(uc);
        }
        List<System.Windows.Controls.Control> _currentSelectedControlCollection;

        public List<System.Windows.Controls.Control> CurrentSelectedControlCollection
        {
            get
            {
                if (_currentSelectedControlCollection == null)
                    _currentSelectedControlCollection = new List<System.Windows.Controls.Control>();
                return _currentSelectedControlCollection;

            }
        }
        //bool ctrlKeyIsPress;
        public bool CtrlKeyIsPress
        {
            get
            {
                return (Keyboard.Modifiers == ModifierKeys.Control);
                //return ctrlKeyIsPress;
            }
        }
        public void SetWorkFlowElementSelected(System.Windows.Controls.Control uc, bool isSelected)
        {
            if (isSelected)
                AddSelectedControl(uc);
            else
                RemoveSelectedControl(uc);
            if (!CtrlKeyIsPress)
                ClearSelectFlowElement(uc);

        }
        public void ClearSelectFlowElement(System.Windows.Controls.Control uc)
        {

            if (CurrentSelectedControlCollection == null || CurrentSelectedControlCollection.Count == 0)
                return;

            int count = CurrentSelectedControlCollection.Count;
            for (int i = 0; i < count; i++)
            {

                ((IElement)CurrentSelectedControlCollection[i]).IsSelectd = false;
            }
            CurrentSelectedControlCollection.Clear();
            if (uc != null)
            {
                ((IElement)uc).IsSelectd = true;
                AddSelectedControl(uc);
            }
            mouseIsInContainer = true;


        }
        public void DeleteSeletedControl()
        {
            if (CurrentSelectedControlCollection == null || CurrentSelectedControlCollection.Count == 0)
                return;
            Activity a = null;
            BarCode b = null;
            for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            {
                if (CurrentSelectedControlCollection[i] is Activity)
                {
                    a = CurrentSelectedControlCollection[i] as Activity;
                    a.Delete();
                }
                if (CurrentSelectedControlCollection[i] is BarCode)
                {
                    b = CurrentSelectedControlCollection[i] as BarCode;
                    b.Delete();
                }
            }
            ClearSelectFlowElement(null);

        }
        public void AlignTop()
        {
            if (CurrentSelectedControlCollection == null || CurrentSelectedControlCollection.Count == 0)
                return;
            Activity a = null;
            double minY = 100000.0;
            for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            {
                if (CurrentSelectedControlCollection[i] is Activity)
                {
                    a = CurrentSelectedControlCollection[i] as Activity;

                    if (a.CenterPoint.Y < minY)
                        minY = a.CenterPoint.Y;
                }

            }
            for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            {
                if (CurrentSelectedControlCollection[i] is Activity)
                {
                    a = CurrentSelectedControlCollection[i] as Activity;
                    a.CenterPoint = new Point(a.CenterPoint.X, minY);
                }
            }
        }
        public void AlignBottom()
        {
            if (CurrentSelectedControlCollection == null || CurrentSelectedControlCollection.Count == 0)
                return;
            Activity a = null;
            double maxY = 0;
            for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            {
                if (CurrentSelectedControlCollection[i] is Activity)
                {
                    a = CurrentSelectedControlCollection[i] as Activity;

                    if (a.CenterPoint.Y > maxY)
                        maxY = a.CenterPoint.Y;
                }

            }
            for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            {
                if (CurrentSelectedControlCollection[i] is Activity)
                {
                    a = CurrentSelectedControlCollection[i] as Activity;
                    a.CenterPoint = new Point(a.CenterPoint.X, maxY);
                }
            }
        }
        public void AlignLeft()
        {

            if (CurrentSelectedControlCollection == null || CurrentSelectedControlCollection.Count == 0)
                return;
            Activity a = null;
            double minX = 100000.0;
            for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            {
                if (CurrentSelectedControlCollection[i] is Activity)
                {
                    a = CurrentSelectedControlCollection[i] as Activity;

                    if (a.CenterPoint.X < minX)
                        minX = a.CenterPoint.X;
                }
            }
            for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            {
                if (CurrentSelectedControlCollection[i] is Activity)
                {
                    a = CurrentSelectedControlCollection[i] as Activity;
                    a.CenterPoint = new Point(minX, a.CenterPoint.Y);
                }
            }

        }
        public void AlignRight()
        {
            if (CurrentSelectedControlCollection == null || CurrentSelectedControlCollection.Count == 0)
                return;
            Activity a = null;
            double maxX = 0;
            for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            {
                if (CurrentSelectedControlCollection[i] is Activity)
                {
                    a = CurrentSelectedControlCollection[i] as Activity;

                    if (a.CenterPoint.X > maxX)
                        maxX = a.CenterPoint.X;
                }

            }
            for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            {
                if (CurrentSelectedControlCollection[i] is Activity)
                {
                    a = CurrentSelectedControlCollection[i] as Activity;
                    a.CenterPoint = new Point(maxX, a.CenterPoint.Y);
                }
            }
        }

        int moveStepLenght
        {
            get
            {
                if (CtrlKeyIsPress)
                    return 5;
                return 1;
            }
        }

        public void MoveUp()
        {
            MoveControlCollectionByDisplacement(0, -moveStepLenght, null);
            SaveChange(HistoryType.New);
        }
        public void MoveLeft()
        {
            MoveControlCollectionByDisplacement(-moveStepLenght, 0, null);
            SaveChange(HistoryType.New);

        }
        public void MoveDown()
        {
            MoveControlCollectionByDisplacement(0, moveStepLenght, null);
            SaveChange(HistoryType.New);

        }
        public void MoveRight()
        {
            MoveControlCollectionByDisplacement(moveStepLenght, 0, null);
            SaveChange(HistoryType.New);

        }
        public void MoveControlCollectionByDisplacement(double x, double y, UserControl uc)
        {
            if (CurrentSelectedControlCollection == null || CurrentSelectedControlCollection.Count == 0)
                return;

            Activity selectedActivity = null;
            BarCode selectedBarCode = null;
            if (uc is Activity)
                selectedActivity = uc as Activity;
            if (uc is BarCode)
                selectedBarCode = uc as BarCode;

            Activity a = null;
            BarCode b = null;
            for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            {


                if (CurrentSelectedControlCollection[i] is Activity)
                {
                    a = CurrentSelectedControlCollection[i] as Activity;
                    if (a == selectedActivity)
                        continue;
                    a.SetPositionByDisplacement(x, y);
                }

                if (CurrentSelectedControlCollection[i] is BarCode)
                {
                    b = CurrentSelectedControlCollection[i] as BarCode;
                    if (b == selectedBarCode)
                        continue;
                    b.SetPositionByDisplacement(x, y);
                }

            }

            //for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
            //{

            //    if (CurrentSelectedControlCollection[i] is Rule)
            //    {
            //        r = CurrentSelectedControlCollection[i] as Rule;
            //        if (r == selectedRule)
            //            continue;
            //        r.SetPositionByDisplacement(x, y); 
            //    }
            //}
        }

        Point mousePosition;
        bool trackingMouseMove = false;
        System.Windows.Threading.DispatcherTimer _doubleClickTimer;
        private void Container_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_doubleClickTimer.IsEnabled)
            {
                _doubleClickTimer.Stop();

                Activity a = new Activity((IContainer)this, ActivityType.INTERACTION, 24);
                a.ActivityName = "新建文字" + NextNewActivityIndex.ToString();
                Point p = e.GetPosition(this);

                a.CenterPoint = new Point(p.X - this.Left, p.Y - this.Top);
                a.IsSelectd = true;
                this.AddActivity(a);

            }
            else
            {
                _doubleClickTimer.Start();
                ClearSelectFlowElement(null);

                FrameworkElement element = sender as FrameworkElement;
                mousePosition = e.GetPosition(element);
                trackingMouseMove = true;
            }
        }

        Rectangle temproaryEllipse;
        public bool IsMouseSelecting
        {
            get
            {
                return (temproaryEllipse != null);
            }
        }

        private void Container_MouseMove(object sender, MouseEventArgs e)
        {



            if (trackingMouseMove)
            {
                FrameworkElement element = sender as FrameworkElement;
                Point beginPoint = mousePosition;
                Point endPoint = e.GetPosition(element);

                if (temproaryEllipse == null)
                {
                    temproaryEllipse = new Rectangle();



                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = Color.FromArgb(255, 234, 213, 2);
                    temproaryEllipse.Fill = brush;
                    temproaryEllipse.Opacity = 0.2;

                    brush = new SolidColorBrush();
                    brush.Color = Color.FromArgb(255, 0, 0, 0);
                    temproaryEllipse.Stroke = brush;
                    temproaryEllipse.StrokeMiterLimit = 2.0;

                    cnsDesignerContainer.Children.Add(temproaryEllipse);

                }

                if (endPoint.X >= beginPoint.X)
                {
                    if (endPoint.Y >= beginPoint.Y)
                    {
                        temproaryEllipse.SetValue(Canvas.TopProperty, beginPoint.Y);
                        temproaryEllipse.SetValue(Canvas.LeftProperty, beginPoint.X);
                    }
                    else
                    {
                        temproaryEllipse.SetValue(Canvas.TopProperty, endPoint.Y);
                        temproaryEllipse.SetValue(Canvas.LeftProperty, beginPoint.X);
                    }

                }
                else
                {
                    if (endPoint.Y >= beginPoint.Y)
                    {
                        temproaryEllipse.SetValue(Canvas.TopProperty, beginPoint.Y);
                        temproaryEllipse.SetValue(Canvas.LeftProperty, endPoint.X);
                    }
                    else
                    {
                        temproaryEllipse.SetValue(Canvas.TopProperty, endPoint.Y);
                        temproaryEllipse.SetValue(Canvas.LeftProperty, endPoint.X);
                    }

                }


                temproaryEllipse.Width = Math.Abs(endPoint.X - beginPoint.X);
                temproaryEllipse.Height = Math.Abs(endPoint.Y - beginPoint.Y);




            }
            else
            {

            }

        }
        private void Container_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trackingMouseMove = false;

            FrameworkElement element = sender as FrameworkElement;
            mousePosition = e.GetPosition(element);
            if (temproaryEllipse != null)
            {
                double width = temproaryEllipse.Width;
                double height = temproaryEllipse.Height;

                if (width > 10 && height > 10)
                {
                    Point p = new Point();
                    p.X = (double)temproaryEllipse.GetValue(Canvas.LeftProperty);
                    p.Y = (double)temproaryEllipse.GetValue(Canvas.TopProperty);

                    Activity a = null;
                    BarCode b = null;
                    foreach (UIElement uie in cnsDesignerContainer.Children)
                    {
                        if (uie is Activity)
                        {
                            a = uie as Activity;
                            if (p.X < a.CenterPoint.X && a.CenterPoint.X < p.X + width
                                && p.Y < a.CenterPoint.Y && a.CenterPoint.Y < p.Y + height)
                            {
                                AddSelectedControl(a);
                                a.IsSelectd = true;
                            }
                        }

                        if (uie is BarCode)
                        {
                            b = uie as BarCode;
                            if (p.X < b.CenterPoint.X && b.CenterPoint.X < p.X + width
                                && p.Y < b.CenterPoint.Y && b.CenterPoint.Y < p.Y + height)
                            {
                                AddSelectedControl(b);
                                b.IsSelectd = true;
                            }
                        }
                    }
                }
                cnsDesignerContainer.Children.Remove(temproaryEllipse);
                temproaryEllipse = null;
            }

        }
        public void PastMemoryToContainer()
        {
            if (CopyElementCollectionInMemory != null
                      && CopyElementCollectionInMemory.Count > 0)
            {
                Activity a = null;
                BarCode b = null;


                foreach (System.Windows.Controls.Control c in CopyElementCollectionInMemory)
                {
                    if (c is Activity)
                    {
                        a = c as Activity;
                        AddActivity(a);
                        a.CenterPoint = new Point(a.CenterPoint.X + 20, a.CenterPoint.Y + 20);
                        a.Move(a, null);


                    }

                }
                foreach (System.Windows.Controls.Control c in CopyElementCollectionInMemory)
                {
                    if (c is BarCode)
                    {
                        b = c as BarCode;
                        AddBarCode(b);
                        b.CenterPoint = new Point(b.CenterPoint.X + 20, b.CenterPoint.Y + 20);
                        b.Move(b, null);


                    }

                }



                for (int i = 0; i < CurrentSelectedControlCollection.Count; i++)
                {
                    ((IElement)CurrentSelectedControlCollection[i]).IsSelectd = false;

                }
                CurrentSelectedControlCollection.Clear();

                for (int i = 0; i < CopyElementCollectionInMemory.Count; i++)
                {

                    ((IElement)CopyElementCollectionInMemory[i]).IsSelectd = true;
                    AddSelectedControl(CopyElementCollectionInMemory[i]);
                }
                CopySelectedControlToMemory(null);

                SaveChange(HistoryType.New);


            }
        }
        public void CopySelectedControlToMemory(System.Windows.Controls.Control currentControl)
        {
            copyElementCollectionInMemory = null;

            if (currentControl != null)
            {
                if (currentControl is Activity)
                {

                    CopyElementCollectionInMemory.Add(((Activity)currentControl).Clone());
                }
                if (currentControl is BarCode)
                {

                    CopyElementCollectionInMemory.Add(((BarCode)currentControl).Clone());
                }
            }
            else
            {
                if (CurrentSelectedControlCollection != null
                    && CurrentSelectedControlCollection.Count > 0)
                {
                    Activity a = null;
                    BarCode b = null;
                    foreach (System.Windows.Controls.Control c in CurrentSelectedControlCollection)
                    {
                        if (c is Activity)
                        {
                            a = c as Activity;

                            CopyElementCollectionInMemory.Add(a.Clone());
                        }
                    }

                    foreach (System.Windows.Controls.Control c in CurrentSelectedControlCollection)
                    {
                        if (c is BarCode)
                        {
                            b = c as BarCode;

                            CopyElementCollectionInMemory.Add(b.Clone());
                        }
                    }

                    foreach (System.Windows.Controls.Control c in CurrentSelectedControlCollection)
                    {
                        if (c is Activity)
                        {
                            a = c as Activity;

                            a.OriginActivity = null;
                        }
                    }

                }
            }
        }
        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Delete:
                    if (CurrentSelectedControlCollection != null && CurrentSelectedControlCollection.Count > 0)
                    {
                        //if (System.Windows.Browser.HtmlPage.Window.Confirm(Text.Comfirm_Delete))
                        //{
                            DeleteSeletedControl();
                            SaveChange(HistoryType.New);
                       // }
                    }
                    break;
                case Key.Up:
                    MoveUp();
                    break;
                case Key.Down:
                    MoveDown();
                    break;
                case Key.Left:
                    MoveLeft();
                    break;
                case Key.Right:
                    MoveRight();
                    break;

            }
        }


        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                switch (e.Key)
                {
                    case Key.Z:

                        SaveChange(HistoryType.Previous);
                        break;
                    case Key.Y:
                        SaveChange(HistoryType.Next);
                        break;
                    case Key.C:

                        CopySelectedControlToMemory(null);
                        break;
                    case Key.V:

                        PastMemoryToContainer();
                        break;
                    case Key.A:
                        Activity a = null;
                        BarCode b = null;
                        foreach (UIElement uie in cnsDesignerContainer.Children)
                        {

                            if (uie is Activity)
                            {
                                a = uie as Activity;
                                a.IsSelectd = true;
                                AddSelectedControl(a);
                            }

                            if (uie is BarCode)
                            {
                                b = uie as BarCode;
                                b.IsSelectd = true;
                                AddSelectedControl(b);
                            }
                        }
                        break;
                    case Key.S://未能捕获 

                        Save();
                        break;

                }
            }
        }
        public void Save()
        {
            XmlContainer.Visibility = Visibility.Visible;
            btnCloseXml.Visibility = Visibility.Visible;
            btnImportXml.Visibility = Visibility.Collapsed;
            txtXml.Text = ToXmlString();
        }
        //void applyContainerCulture()
        //{
        //    //btnAddActivity.Content = Text.Button_AddActivity;
        //    // btnCreatePicture.Content = Text.Button_CreatePicture;
        //    //Content = Text.Button_AddRule;
        //    btnClearContainer.Content = Text.Button_ClearContainer;
        //    btnCloseXml.Content = Text.Button_Close;
        //    btnExportToXml.Content = Text.Button_ExportToXml;
        //    btnImportFromXml.Content = Text.Button_ImportFromXml;
        //    btnImportXml.Content = Text.Button_ImportFromXml;
        //    btnNext.Content = Text.Button_Next;
        //    btnPrevious.Content = Text.Button_Previous;
        //    tbContainerHeight.Text = Text.ContainerHeight;
        //    tbContainerWidth.Text = Text.ContainerWidth;
        //    tbWorkFlowName.Text = Text.WorkFlowName;
        //    btnCloseMessage.Content = Text.Button_Close;
        //    //tbZoom.Text = Text.Button_Zoom;
        //    btnSave.Content = Text.Button_Save;
        //    tbShowGridLines.Text = Text.Menu_ShowGridLines;
        //    // btnAddLabel.Content = Text.Button_AddLabel; 

        //}
        //public void ApplyCulture()
        //{

        //    applyContainerCulture();
        //    siActivitySetting.ApplyCulture();
        //}

        List<System.Windows.Controls.Control> copyElementCollectionInMemory;

        public List<System.Windows.Controls.Control> CopyElementCollectionInMemory
        {
            get
            {
                if (copyElementCollectionInMemory == null)
                    copyElementCollectionInMemory = new List<System.Windows.Controls.Control>();
                return copyElementCollectionInMemory;
            }
            set
            {
                copyElementCollectionInMemory = value;
            }
        }


        //private void btnApplyEnglishCulture_Click(object sender, RoutedEventArgs e)
        //{
        //    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-us");
        //    Shareidea.Web.Component.Workflow.Configure.CurrentCulture = culture;
        //    btnApplyChineseCulture.IsEnabled = true;
        //    btnApplyEnglishCulture.IsEnabled = false;
        //    ApplyCulture();
        //}
        //private void btnApplyChineseCulture_Click(object sender, RoutedEventArgs e)
        //{
        //    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("zh-cn");
        //    Shareidea.Web.Component.Workflow.Configure.CurrentCulture = culture;

        //    btnApplyChineseCulture.IsEnabled = false;
        //    btnApplyEnglishCulture.IsEnabled = true;
        //    ApplyCulture();
        //}
        bool mouseIsInContainer = false;
        public bool MouseIsInContainer { get { return mouseIsInContainer; } set { mouseIsInContainer = value; } }

        private void Container_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseIsInContainer = true;

        }

        private void Container_MouseLeave(object sender, MouseEventArgs e)
        {
            mouseIsInContainer = false;

        }
        //private void sliWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    if (cnsDesignerContainer != null)
        //    {
        //        cnsDesignerContainer.Width = sliWidth.Value;
        //        SetGridLines();
        //    }
        //}
        //private void sliHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{

        //    if (cnsDesignerContainer != null)
        //    {
        //        cnsDesignerContainer.Height = sliHeight.Value;
        //        SetGridLines();
        //    }
        //}

        //private void sliZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    if (sliZoom != null)
        //    {
        //        double zoomDeep = sliZoom.Value;
        //        btZoomValue.Text = Math.Round(zoomDeep, 2).ToString();

        //        IElement iel = null;
        //        foreach (UIElement uic in cnsDesignerContainer.Children)
        //        {
        //            iel = uic as IElement;
        //            if (iel != null)
        //            {
        //                iel.Zoom(zoomDeep);
        //            }
        //        }

        //        //if (zoomDeep >= 1)
        //        //{
        //        //    sliWidth.Value = sliWidth.Minimum * zoomDeep;
        //        //    sliHeight.Value = sliHeight.Minimum * zoomDeep;

        //        //}
        //        //else
        //        //{
        //        //    sliWidth.Value = sliWidth.Minimum;
        //        //    sliHeight.Value = sliHeight.Minimum;
        //        //}
        //    }
        //}


        private void cbShowGridLines_Click(object sender, RoutedEventArgs e)
        {
            if (cbShowGridLines.IsChecked.HasValue && cbShowGridLines.IsChecked.Value)
            {
                SetGridLines();
            }
            else
            {
                if (_gridLinesContainer != null)
                    _gridLinesContainer.Children.Clear();
                // _gridLinesContainer = null;
            }

        }

        private void tbWidth_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string pattern = @"^\d+(\.\d)?$";
                if (tbWidth.Text.Trim() != null)
                {
                    if (!Regex.IsMatch(tbWidth.Text.Trim(), pattern))
                    {
                        ShowMessage("Please type in number!");
                    }
                    else
                    {
                        cnsDesignerContainer.Width = 8 * Convert.ToDouble(tbWidth.Text);
                        SetGridLines();
                    }
                }
            }
        }

        private void tbHeight_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string pattern = @"^\d+(\.\d)?$";
                if (tbHeight.Text.Trim() != null)
                {
                    if (!Regex.IsMatch(tbHeight.Text.Trim(), pattern))
                    {
                        ShowMessage("Please type in number!");
                    }
                    else
                    {
                        cnsDesignerContainer.Height = 8 * Convert.ToDouble(tbHeight.Text);
                        SetGridLines();
                    }
                }
            }
        }

        private void tbWidth_LostFocus_1(object sender, RoutedEventArgs e)
        {
            string pattern = @"^\d+(\.\d)?$";
            if (tbWidth.Text.Trim() != null)
            {
                if (!Regex.IsMatch(tbWidth.Text.Trim(), pattern))
                {
                    ShowMessage("Please type in number!");
                }
                else
                {
                    cnsDesignerContainer.Width = 8 * Convert.ToDouble(tbWidth.Text);
                    SetGridLines();
                    SaveChange(HistoryType.New);
                }
            }
        }

        private void tbHeight_LostFocus_1(object sender, RoutedEventArgs e)
        {
            string pattern = @"^\d+(\.\d)?$";
            if (tbHeight.Text.Trim() != null)
            {
                if (!Regex.IsMatch(tbHeight.Text.Trim(), pattern))
                {
                    ShowMessage("Please type in number!");
                }
                else
                {
                    cnsDesignerContainer.Height = 8 * Convert.ToDouble(tbHeight.Text);
                    SetGridLines();
                    SaveChange(HistoryType.New);
                }
            }
        }
        private void tbNums_LostFocus_1(object sender, RoutedEventArgs e)
        {
            string pattern = @"^\d+(\.\d)?$";
            if (tbNums.Text.Trim() != null)
            {
                if (!Regex.IsMatch(tbNums.Text.Trim(), pattern))
                {
                    ShowMessage("Please type in number!");
                }
                else
                {
                    PrintNums = Convert.ToInt32(tbNums.Text);
                    SaveChange(HistoryType.New);
                }
            }
        }
        bool pLSave = true;
        bool pSSave = true;
        int printLightness = 8;
        public int PrintLightness
        {
            get
            {
                return printLightness;
            }
            set
            {
                int tmp;
                for (int i = 0; i < cbPrintLightness.Items.Count; i++)
                {
                    tmp = (int)cbPrintLightness.Items[i];

                    if (tmp == value)
                    {
                        cbPrintLightness.SelectedIndex = i;
                        break;
                    }
                }
                printLightness = value;
            }
        }
        int printSpeed = 4;
        public int PrintSpeed
        {
            get
            {
                return printSpeed;
            }
            set
            {
                int tmp;
                for (int i = 0; i < cbPrintSpeed.Items.Count; i++)
                {
                    tmp = (int)cbPrintSpeed.Items[i];

                    if (tmp == value)
                    {
                        cbPrintSpeed.SelectedIndex = i;
                        break;
                    }
                }
                printSpeed = value;
            }
        }
        int printNums = 1;
        public int PrintNums
        {
            get
            {
                return printNums;
            }
            set
            {
                tbNums.Text = value.ToString();
                printNums = value;
            }
        }

        private void cbPrintLightness_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbPrintSpeed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbPrintLightness_DropDownClosed(object sender, EventArgs e)
        {
            PrintLightness = Convert.ToInt32(cbPrintLightness.SelectedItem);
            SaveChange(HistoryType.New);
        }

        private void cbPrintSpeed_DropDownClosed(object sender, EventArgs e)
        {
            PrintSpeed = Convert.ToInt32(cbPrintSpeed.SelectedItem);
            SaveChange(HistoryType.New);
        }
    }
}
