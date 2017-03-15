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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabelPrinter.Designer
{
    public delegate void BarCodeMoveDelegate(BarCode b, MouseEventArgs e);
    public delegate void BarCodeDeleteDelegate(BarCode b);

    public delegate void BarCodeChangeDelegate(BarCode b);
    public partial class BarCode : UserControl, IElement
    {


        string barCodeInfo;

        public string BarCodeInfo
        {
            get
            {
                return barCodeInfo;
            }
            set
            {
                barCodeInfo = value;
            }
        }

        double positionX;

        public double PositionX
        {
            get
            {
                return positionX;
            }
            set
            {
                positionX = value;
            }
        }

        double positionY;

        public double PositionY
        {
            get
            {
                return positionY;
            }
            set
            {
                positionY = value;
            }
        }


        MergePictureRepeatDirection _repeatDirection = MergePictureRepeatDirection.Horizontal;
        public MergePictureRepeatDirection RepeatDirection
        {
            get
            {

                //_repeatDirection = ((MergeActivity)currentPic).RepeatDirection;

                return _repeatDirection;
            }
            set
            {
                _repeatDirection = value;
                if (_repeatDirection == MergePictureRepeatDirection.Vertical)
                {
                    gridTransform.Angle = 90;
                    //gridTransform.TranslateX = this.BarCodeHeight + 16;
                }
                else
                {
                    gridTransform.Angle = 0;
                    //gridTransform.TranslateX = 0;
                }

                //((MergeActivity)currentPic).RepeatDirection = _repeatDirection;
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
        string barCodeID;

        public string BarCodeID
        {
            get
            {
                return barCodeID;
            }
            set
            {
                barCodeID = value;
            }
        }


        //
        double origPictureWidth = 0;
        double origPictureHeight = 0;
        Point origPosition;
        bool positionIsChange = true;

        int barCodeWith = 1;//default
        public int BarCodeWidth
        {
            get
            {
                return barCodeWith;
            }
            set
            {
                barCodeWith = value;
            }
        }

        int barCodeHeight = 60;//default
        public int BarCodeHeight
        {
            get
            {
                return barCodeHeight;
            }
            set
            {
                barCodeHeight = value;
            }
        }

        bool isPassCheck
        {
            set
            {
                if (value)
                {

                    //sdPicture.ResetInitColor();
                }
                else
                {
                    //sdPicture.SetWarningColor();
                }
            }
        }
        BarCodeType type = BarCodeType.Code39;
        public BarCodeType Type
        {
            get
            {
                return type;
            }
            set
            {
                bool isChanged = false;
                if (type != value)
                {

                    isChanged = true;
                }
                type = value;
                if (type == BarCodeType.Code39)
                {
                    //eiCenterEllipse.Visibility = Visibility.Collapsed;

                }
                else
                {
                    //eiCenterEllipse.Visibility = Visibility.Visible;
                }
                //sdPicture.Type = type;
                if (isChanged)
                    Move(this, null);

            }
        }
        public event BarCodeMoveDelegate BarCodeMove;
        public event BarCodeDeleteDelegate DeleteBarCode;
        public void Move(BarCode a, MouseEventArgs e)
        {
            if (BarCodeMove != null)
                BarCodeMove(a, e);
        }




        public BarCode(IContainer container, BarCodeType bt, string binfo, int bw, int bh)
        {
            InitializeComponent();
            _container = container;
            editType = PageEditType.Add;
            this.Type = bt;
            //System.Windows.Browser.HtmlPage.Document.AttachEvent("oncontextmenu", OnContextMenu);
            //this.Name = UniqueID;
            BarCodeCore.EnCodeDraw drawer = new BarCodeCore.EnCodeDraw();
            barCodeGrid.Children.Add(drawer.DrawImg39(binfo, bw, bh));

            _doubleClickTimer = new System.Windows.Threading.DispatcherTimer();
            _doubleClickTimer.Interval = new TimeSpan(0, 0, 0, 0, 400);
            _doubleClickTimer.Tick += new EventHandler(DoubleClick_Timer);
            Storyboard story = (Storyboard)containerUI.FindResource("sbDisplay");
            story.Begin();

        }


        PageEditType editType = PageEditType.None;
        public PageEditType EditType
        {
            get
            {
                return editType;
            }
            set
            {
                editType = value;
            }
        }

        public event BarCodeChangeDelegate BarCodeChanged;

        void barCodeChange()
        {
            if (BarCodeChanged != null)
                BarCodeChanged(this);
        }

        BarCodeComponent barCodeData;
        public BarCodeComponent BarCodeData
        {
            get
            {
                if (barCodeData == null)
                {
                    if (EditType == PageEditType.Add)
                    {
                        barCodeData = new BarCodeComponent();
                        barCodeData.BarCodeID = this.BarCodeID;
                        barCodeData.UniqueID = this.UniqueID;
                        barCodeData.BarCodeInfo = this.BarCodeInfo;
                        barCodeData.BarCodeType = Type.ToString();
                        barCodeData.BarCodeWidth = this.BarCodeWidth;
                        barCodeData.BarCodeHeight = this.BarCodeHeight;
                        barCodeData.PositionX = CenterPoint.X;
                        barCodeData.PositionY = CenterPoint.Y;
                        barCodeData.RepeatDirection = RepeatDirection.ToString();


                    }
                    else if (EditType == PageEditType.Modify)
                    {
                        barCodeData = getBarCodeComponentFromServer(this.BarCodeID);

                    }
                }
                barCodeData.PositionX = CenterPoint.X;
                barCodeData.PositionY = CenterPoint.Y;
                //activityData.FontSize = sdPicture.FontSize;
                return barCodeData;
            }
            set
            {
                barCodeData = value;
            }
        }

        public void SetBarCodeData(BarCodeComponent barCodeData)
        {
            bool isChanged = false;

            if (BarCodeData.BarCodeInfo != barCodeData.BarCodeInfo
                || BarCodeData.BarCodeType != barCodeData.BarCodeType
                || BarCodeData.BarCodeWidth != barCodeData.BarCodeWidth || BarCodeData.BarCodeHeight != barCodeData.BarCodeHeight || BarCodeData.PositionX != barCodeData.PositionX || BarCodeData.PositionY != barCodeData.PositionY || BarCodeData.RepeatDirection != barCodeData.RepeatDirection)
            {
                isChanged = true;

            }

            BarCodeData = barCodeData;
            setUIValueByBarCodeData(barCodeData);
            if (isChanged)
            {
                if (BarCodeChanged != null)
                    BarCodeChanged(this);
            }
            IsSelectd = IsSelectd;

        }

        void setUIValueByBarCodeData(BarCodeComponent barCodeData)
        {
            BarCodeInfo = barCodeData.BarCodeInfo;
            BarCodeWidth = barCodeData.BarCodeWidth;
            BarCodeHeight = barCodeData.BarCodeHeight;
            BarCodeCore.EnCodeDraw drawer = new BarCodeCore.EnCodeDraw();
            barCodeGrid.Children.Clear();
            barCodeGrid.Children.Add(drawer.DrawImg39(BarCodeInfo, BarCodeWidth, BarCodeHeight));
            CenterPoint = new Point(barCodeData.PositionX, barCodeData.PositionY);
            BarCodeType type = (BarCodeType)Enum.Parse(typeof(BarCodeType), barCodeData.BarCodeType, true);
            MergePictureRepeatDirection repeatDirection = (MergePictureRepeatDirection)Enum.Parse(typeof(MergePictureRepeatDirection), barCodeData.RepeatDirection, true);
            Type = type;
            RepeatDirection = repeatDirection;


        }

        BarCodeComponent getBarCodeComponentFromServer(string barCodeID)
        {
            BarCodeComponent bc = new BarCodeComponent();
            bc = new BarCodeComponent();
            bc.BarCodeID = this.BarCodeID;
            bc.UniqueID = this.UniqueID;
            bc.BarCodeInfo = this.BarCodeInfo;
            bc.BarCodeType = Type.ToString();
            bc.BarCodeWidth = this.BarCodeWidth;
            bc.BarCodeHeight = this.BarCodeHeight;
            bc.PositionX = CenterPoint.X;
            bc.PositionY = CenterPoint.Y;
            bc.RepeatDirection = RepeatDirection.ToString();
            return bc;
        }

        public Point CenterPoint
        {
            get
            {


                return new Point((double)this.GetValue(Canvas.LeftProperty), (double)this.GetValue(Canvas.TopProperty));

            }
            set
            {


                this.SetValue(Canvas.LeftProperty, value.X);
                this.SetValue(Canvas.TopProperty, value.Y);
                Move(this, null);


            }
        }

        public Point Position
        {
            get
            {
                Point position;

                position = new Point();
                position.Y = (double)this.GetValue(Canvas.TopProperty);
                position.X = (double)this.GetValue(Canvas.LeftProperty);


                return position;
            }
            set
            {

                this.SetValue(Canvas.TopProperty, value.Y);
                this.SetValue(Canvas.LeftProperty, value.X);
                Move(this, null);
            }
        }

        bool isSelectd = false;
        public bool IsSelectd
        {
            get
            {
                return isSelectd;
            }
            set
            {
                isSelectd = value;
                if (isSelectd)
                {
                    picBARCODE.Fill = SystemConst.ColorConst.SelectedColor;

                    if (!_container.CurrentSelectedControlCollection.Contains(this))
                        _container.AddSelectedControl(this);



                }
                else
                {
                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = Colors.Transparent;
                    picBARCODE.Fill = brush;
                }
            }

        }

        public CheckResult CheckSave()
        {
            CheckResult cr = new CheckResult();
            cr.IsPass = true;



            //if (Type == ActivityType.INITIAL)
            //{
            //    if (EndRuleCollections != null
            //        && EndRuleCollections.Count > 0)
            //    {
            //        cr.IsPass = false;
            //        cr.Message += string.Format(Text.Message_CanNotHavePreactivity, ActivityName);
            //    }
            //    if (BeginRuleCollections == null
            //        || BeginRuleCollections.Count == 0)
            //    {
            //        cr.IsPass = false;//必须至少有一个后继活动
            //        cr.Message += string.Format(Text.Message_MustHaveAtLeastOneFollowUpActivity, ActivityName);
            //    }
            //}
            //else if (Type == ActivityType.COMPLETION)
            //{
            //    if (BeginRuleCollections != null
            //        && BeginRuleCollections.Count > 0)
            //    {
            //        cr.IsPass = false;//不能有后继活动
            //        cr.Message += string.Format(Text.Message_NotHaveFollowUpActivity, ActivityName);
            //    }
            //    if (EndRuleCollections == null
            //        || EndRuleCollections.Count == 0)
            //    {
            //        cr.IsPass = false;//必须至少有一个前驱活动
            //        cr.Message += string.Format(Text.Message_MustHaveAtLeastOnePreactivity, ActivityName);
            //    }
            //}
            //else
            //{
            //    if ((BeginRuleCollections == null
            //    || BeginRuleCollections.Count == 0)
            //        && (EndRuleCollections == null
            //    || EndRuleCollections.Count == 0))
            //    {
            //        cr.IsPass = false;//必须设置前驱和后继活动
            //        cr.Message += string.Format(Text.Message_RequireTheInstallationOfPreAndFollowupActivity, ActivityName);
            //    }
            //    else
            //    {

            //        if (BeginRuleCollections == null
            //        || BeginRuleCollections.Count == 0)
            //        {
            //            cr.IsPass = false;//必须至少有一个后继活动
            //            cr.Message += string.Format(Text.Message_MustHaveAtLeastOneFollowUpActivity, ActivityName);
            //        }
            //        if (EndRuleCollections == null
            //        || EndRuleCollections.Count == 0)
            //        {
            //            cr.IsPass = false;//必须至少有一个前驱活动
            //            cr.Message += string.Format(Text.Message_MustHaveAtLeastOnePreactivity, ActivityName);
            //        }
            //        if (Type == ActivityType.AND_BRANCH
            //            || Type == ActivityType.OR_BRANCH)
            //        {
            //            if (EndRuleCollections != null
            //                && EndRuleCollections.Count > 1)
            //            {
            //                //cr.IsPass = false;//有且只能有一个前驱活动
            //                //cr.Message += string.Format(Text.Message_MustHaveOnlyOnePreactivity, ActivityName);
            //            }
            //        }

            //        if (Type == ActivityType.AND_MERGE
            //            || Type == ActivityType.OR_MERGE
            //            || Type == ActivityType.VOTE_MERGE)
            //        {
            //            if (BeginRuleCollections != null
            //                && BeginRuleCollections.Count > 1)
            //            {
            //                //cr.IsPass = false;
            //                // cr.Message += string.Format(Text.Message_MustHaveOnlyOneFollowUpActivity, ActivityName);
            //            }
            //        }
            //    }
            //}
            //isPassCheck = cr.IsPass;
            //if (!cr.IsPass)
            //{
            //    errorTipControl.Visibility = Visibility.Visible;
            //    errorTipControl.ErrorMessage = cr.Message.TrimEnd("\r\n".ToCharArray());
            //}
            //else
            //{
            //    if (_errorTipControl != null)
            //    {
            //        _errorTipControl.Visibility = Visibility.Collapsed;
            //        container.Children.Remove(_errorTipControl);
            //        _errorTipControl = null;
            //    }
            //}
            return cr;


        }
        ErrorTip _errorTipControl;
        ErrorTip errorTipControl
        {
            get
            {
                if (_errorTipControl == null)
                {
                    _errorTipControl = new ErrorTip();
                    _errorTipControl.ParentElement = this;
                    containerUI.Children.Add(_errorTipControl);

                }
                _errorTipControl.SetValue(Canvas.ZIndexProperty, 1);
                _errorTipControl.SetValue(Canvas.TopProperty, -this.Height / 2);
                _errorTipControl.SetValue(Canvas.LeftProperty, this.Width);
                return _errorTipControl;
            }
        }
        public WorkFlowElementType ElementType
        {
            get
            {
                return WorkFlowElementType.BarCode;
            }
        }

        public string ToXmlString()
        {
            System.Text.StringBuilder xml = new System.Text.StringBuilder();
            xml.Append(@"       <BarCode ");
            xml.Append(@" UniqueID=""" + UniqueID + @"""");
            xml.Append(@" BarCodeID=""" + BarCodeID + @"""");
            xml.Append(@" BarCodeInfo=""" + BarCodeInfo + @"""");
            xml.Append(@" Type=""" + Type.ToString() + @"""");
            xml.Append(@" PositionX=""" + CenterPoint.X + @"""");
            xml.Append(@" PositionY=""" + CenterPoint.Y + @"""");
            xml.Append(@" RepeatDirection=""" + RepeatDirection.ToString() + @"""");
            xml.Append(@" BarCodeWidth=""" + BarCodeWidth.ToString() + @"""");
            xml.Append(@" BarCodeHeight=""" + BarCodeHeight.ToString() + @"""");
            xml.Append(@" ZIndex=""" + ZIndex + @""">");
            xml.Append(Environment.NewLine);
            xml.Append("        </BarCode>");

            return xml.ToString();
        }
        public string ToEZPLString()
        {
            string EZPLString = "";
            EZPLString += "BA," + ((int)CenterPoint.X).ToString() + "," + ((int)CenterPoint.Y).ToString() + "," + BarCodeWidth.ToString() + "," + (BarCodeWidth * 2).ToString() + "," + BarCodeHeight.ToString() + ",";
            EZPLString += (RepeatDirection == MergePictureRepeatDirection.Horizontal ? 0 : 1).ToString() + ",";
            EZPLString += "3,";
            EZPLString += BarCodeInfo;
            return EZPLString;
        }

        public void LoadFromXmlString(string xmlString)
        {
        }

        public bool CanShowMenu
        {
            get
            {
                return canShowMenu;
            }
            set
            {
                canShowMenu = value;
            }
        }
        bool canShowMenu = false;

        public bool IsDeleted
        {
            get
            {
                return isDeleted;
            }
        }

        bool isDeleted = false;
        public void Delete()
        {


            if (!isDeleted)
            {
                isDeleted = true;
                canShowMenu = false;
                Storyboard story = (Storyboard)containerUI.FindResource("sbClose");
                story.Completed += new EventHandler(sbClose_Completed);
                story.Begin();
            }

        }

        void sbClose_Completed(object sender, EventArgs e)
        {
            if (isDeleted)
            {
                //if (this.BeginRuleCollections != null)
                //{
                //    foreach (Rule r in this.BeginRuleCollections)
                //    {
                //        r.RemoveBeginActivity(this);
                //    }
                //}
                //if (this.EndRuleCollections != null)
                //{
                //    foreach (Rule r in this.EndRuleCollections)
                //    {
                //        r.RemoveEndActivity(this);
                //    }
                //}
                if (DeleteBarCode != null)
                    DeleteBarCode(this);

                _container.RemoveBarCode(this);

                //if (ActivityChanged != null)
                //    ActivityChanged(this);
            }
        }

        BarCode originBarCode;
        public BarCode OriginBarCode
        {
            get
            {
                return originBarCode;
            }
            set
            {
                originBarCode = null;
            }

        }

        IContainer _container;
        public IContainer Container
        {
            get
            {
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        public void ShowMessage(string message)
        {
            _container.ShowMessage(message);
        }
        System.Windows.Threading.DispatcherTimer _doubleClickTimer;

        void DoubleClick_Timer(object sender, EventArgs e)
        {
            _doubleClickTimer.Stop();
        }


        bool trackingMouseMove = false;

        Point mousePosition;
        bool hadActualMove = false;
        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {


            if (trackingMouseMove)
            {
                FrameworkElement element = sender as FrameworkElement;
                element.Cursor = Cursors.Hand;

                if (e.GetPosition(null) == mousePosition)
                    return;
                hadActualMove = true;
                double deltaV = e.GetPosition(null).Y - mousePosition.Y;
                double deltaH = e.GetPosition(null).X - mousePosition.X;
                double newTop = deltaV + Position.Y;
                double newLeft = deltaH + Position.X;




                double containerWidth = (double)this.Parent.GetValue(Canvas.WidthProperty);
                double containerHeight = (double)this.Parent.GetValue(Canvas.HeightProperty);
                if ((CenterPoint.X < 2 && deltaH < 0)
                    || (CenterPoint.Y < 2 && deltaV < 0)
                    )
                {
                    //超过流程容器的范围
                }
                else
                {
                    positionIsChange = true;
                    this.SetValue(Canvas.TopProperty, newTop);
                    this.SetValue(Canvas.LeftProperty, newLeft);

                    Move(this, e);
                    mousePosition = e.GetPosition(null);
                    _container.MoveControlCollectionByDisplacement(deltaH, deltaV, this);
                }


            }
        }


        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            hadActualMove = false;
            if (_doubleClickTimer.IsEnabled)
            {

                _doubleClickTimer.Stop();
                _container.ShowBarCodeSetting(this);


            }
            else
            {

                _doubleClickTimer.Start();
                this.SetValue(Canvas.ZIndexProperty, _container.NextMaxIndex);
                FrameworkElement element = sender as FrameworkElement;
                mousePosition = e.GetPosition(null);
                trackingMouseMove = true;

                if (null != element)
                {
                    element.CaptureMouse();
                    element.Cursor = Cursors.Hand;
                }
            }
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_container.IsMouseSelecting != null)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;

            canShowMenu = true;
            if (!hadActualMove && !_container.IsMouseSelecting)
            {


                IsSelectd = !IsSelectd;
                _container.SetWorkFlowElementSelected(this, IsSelectd);
            }
            FrameworkElement element = sender as FrameworkElement;
            trackingMouseMove = false;
            element.ReleaseMouseCapture();

            mousePosition.X = mousePosition.Y = 0;
            element.Cursor = null;

            if (hadActualMove)
                barCodeChange();
        }
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            canShowMenu = true;

            //ToolTip tt = new ToolTip();
            //ttActivityTip.Content = ActivityData.ActivityName + "\r\n" + ActivityData.ActivityType;
            return;

        }
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            canShowMenu = false;

        }

        public void SetPositionByDisplacement(double x, double y)
        {


            Point p = new Point();
            p.X = (double)this.GetValue(Canvas.LeftProperty);
            p.Y = (double)this.GetValue(Canvas.TopProperty);

            this.SetValue(Canvas.TopProperty, p.Y + y);
            this.SetValue(Canvas.LeftProperty, p.X + x);
            Move(this, null);

        }

        public bool PointIsInside(Point p)
        {
            bool isInside = false;


            double thisWidth = Width;
            double thisHeight = Height;

            double thisX = CenterPoint.X - thisWidth / 2;
            double thisY = CenterPoint.Y - thisHeight / 2;

            if (thisX < p.X && p.X < thisX + thisWidth
                && thisY < p.Y && p.Y < thisY + thisHeight)
            {
                isInside = true;
            }


            return isInside;
        }

        public BarCode Clone()
        {
            BarCode clone = new BarCode(this._container, this.Type, this.BarCodeInfo, this.BarCodeWidth, this.BarCodeHeight);
            clone.originBarCode = this;
            clone.BarCodeData = new BarCodeComponent();
            clone.BarCodeData.BarCodeInfo = this.BarCodeData.BarCodeInfo;
            clone.BarCodeData.BarCodeType = this.BarCodeData.BarCodeType;
            clone.BarCodeData.BarCodeWidth = this.BarCodeData.BarCodeWidth;
            clone.BarCodeData.BarCodeHeight = this.BarCodeData.BarCodeHeight;
            clone.setUIValueByBarCodeData(clone.BarCodeData);
            // clone.CenterPoint = this.CenterPoint;
            clone.CenterPoint = this.CenterPoint;
            //clone.ZIndex = this.ZIndex;
            //_container.AddActivity(clone);

            return clone;
        }

        public void Zoom(double zoomDeep)
        {
            //if (origPictureWidth == 0)
            //{
            //    origPictureWidth = sdPicture.PictureWidth;
            //    origPictureHeight = sdPicture.PictureHeight;
            //}
            //if (positionIsChange)
            //{
            //    origPosition = this.Position;
            //    positionIsChange = false;
            //}

            //sdPicture.PictureHeight = origPictureHeight * zoomDeep;
            //sdPicture.PictureWidth = origPictureWidth * zoomDeep;
            //this.Position = new Point(origPosition.X * zoomDeep, origPosition.Y * zoomDeep);

        }
        public void UpperZIndex()
        {
            ZIndex = _container.NextMaxIndex;
        }

        public int ZIndex
        {
            get
            {
                return (int)this.GetValue(Canvas.ZIndexProperty);

            }
            set
            {
                this.SetValue(Canvas.ZIndexProperty, value);
            }

        }
    }
}
