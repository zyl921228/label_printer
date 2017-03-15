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

    public delegate void MoveDelegate(Activity a, MouseEventArgs e);
    public delegate void DeleteDelegate(Activity a);

    public delegate void ActivityChangeDelegate(Activity a);



    public partial class Activity : UserControl, IElement
    {
        //
        double origPictureWidth = 0;
        double origPictureHeight = 0;
        Point origPosition;
        bool positionIsChange = true;
        public void Zoom(double zoomDeep)
        {
            if (origPictureWidth == 0)
            {
                origPictureWidth = sdPicture.PictureWidth;
                origPictureHeight = sdPicture.PictureHeight;
            }
            if (positionIsChange)
            {
                origPosition = this.Position;
                positionIsChange = false;
            }

            sdPicture.PictureHeight = origPictureHeight * zoomDeep;
            sdPicture.PictureWidth = origPictureWidth * zoomDeep;
            this.Position = new Point(origPosition.X * zoomDeep, origPosition.Y * zoomDeep);

        }

        public double PictureWidth
        {
            get
            {
                return sdPicture.PictureWidth;
            }
        }
        public double PictureHeight
        {
            get
            {
                return sdPicture.PictureHeight;
            }
        }

        bool isPassCheck
        {
            set
            {
                if (value)
                {

                    sdPicture.ResetInitColor();
                }
                else
                {
                    sdPicture.SetWarningColor();
                }
            }
        }

        public CheckResult CheckSave()
        {
            CheckResult cr = new CheckResult();
            cr.IsPass = true;
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
                _errorTipControl.SetValue(Canvas.TopProperty, -this.PictureHeight / 2);
                _errorTipControl.SetValue(Canvas.LeftProperty, this.PictureWidth);
                return _errorTipControl;
            }
        }





        ActivityType type = ActivityType.INTERACTION;
        public ActivityType Type
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
                if (type == ActivityType.COMPLETION)
                {
                    //eiCenterEllipse.Visibility = Visibility.Collapsed;

                }
                else
                {
                    //eiCenterEllipse.Visibility = Visibility.Visible;
                }
                sdPicture.Type = type;
                if (isChanged)
                    Move(this, null);

            }
        }

        public void SetActivityData(ActivityComponent activityData)
        {
            bool isChanged = false;


            if (ActivityData.ActivityName != activityData.ActivityName
                || ActivityData.ActivityType != activityData.ActivityType
                || ActivityData.RepeatDirection != activityData.RepeatDirection || ActivityData.FontSize != activityData.FontSize || ActivityData.PositionX != activityData.PositionX || ActivityData.PositionY != activityData.PositionY)
            {
                isChanged = true;

            }

            ActivityData = activityData;
            setUIValueByActivityData(activityData);
            if (isChanged)
            {
                if (ActivityChanged != null)
                    ActivityChanged(this);
            }
            IsSelectd = IsSelectd;

        }

        void setUIValueByActivityData(ActivityComponent activityData)
        {
            sdPicture.AcitivtyName = activityData.ActivityName;
            CenterPoint = new Point(activityData.PositionX, activityData.PositionY);
            ActivityType type = (ActivityType)Enum.Parse(typeof(ActivityType), activityData.ActivityType, true);
            MergePictureRepeatDirection repeatDirection = (MergePictureRepeatDirection)Enum.Parse(typeof(MergePictureRepeatDirection), activityData.RepeatDirection, true);
            Type = type;
            SubFlow = activityData.SubFlow;
            sdPicture.FontSize = activityData.FontSize;


        }
        string _subFlow;
        public string SubFlow
        {
            get
            {
                return _subFlow;
            }
            set
            {
                _subFlow = value;
            }
        }


        public PointCollection ThisPointCollection
        {
            get
            {
                return sdPicture.ThisPointCollection;
            }
        }
        ActivityComponent getActivityComponentFromServer(string activityID)
        {
            ActivityComponent ac = new ActivityComponent();
            ac = new ActivityComponent();
            ac.ActivityID = this.ActivityID;
            ac.UniqueID = this.UniqueID;
            ac.ActivityName = sdPicture.AcitivtyName;
            ac.ActivityType = Type.ToString();
            ac.FontSize = sdPicture.FontSize;
            ac.PositionX = CenterPoint.X;
            ac.PositionY = CenterPoint.Y;
            ac.SubFlow = this.SubFlow;
            return ac;
        }
        ActivityComponent activityData;
        public ActivityComponent ActivityData
        {
            get
            {
                if (activityData == null)
                {
                    if (EditType == PageEditType.Add)
                    {
                        activityData = new ActivityComponent();
                        activityData.ActivityID = this.ActivityID;
                        activityData.UniqueID = this.UniqueID;
                        activityData.ActivityName = sdPicture.AcitivtyName;
                        activityData.ActivityType = Type.ToString();
                        activityData.FontSize = sdPicture.FontSize;
                        activityData.PositionX = CenterPoint.X;
                        activityData.PositionY = CenterPoint.Y;
                        activityData.SubFlow = SubFlow;


                    }
                    else if (EditType == PageEditType.Modify)
                    {
                        activityData = getActivityComponentFromServer(this.ActivityID);

                    }
                }
                activityData.PositionX = CenterPoint.X;
                activityData.PositionY = CenterPoint.Y;
                //activityData.FontSize = sdPicture.FontSize;
                return activityData;
            }
            set
            {
                activityData = value;
            }
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

        public event ActivityChangeDelegate ActivityChanged;

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
        public WorkFlowElementType ElementType
        {
            get
            {
                return WorkFlowElementType.Activity;
            }
        }
        public string ToXmlString()
        {
            System.Text.StringBuilder xml = new System.Text.StringBuilder();
            xml.Append(@"       <Activity ");
            xml.Append(@" UniqueID=""" + UniqueID + @"""");
            xml.Append(@" ActivityID=""" + ActivityID + @"""");
            xml.Append(@" ActivityName=""" + ActivityName + @"""");
            xml.Append(@" Type=""" + Type.ToString() + @"""");
            xml.Append(@" FontSize=""" + sdPicture.FontSize.ToString() + @"""");
            xml.Append(@" SubFlow=""" + (Type == ActivityType.SUBPROCESS ? SubFlow : @"") + @"""");
            xml.Append(@" PositionX=""" + CenterPoint.X + @"""");
            xml.Append(@" PositionY=""" + CenterPoint.Y + @"""");
            xml.Append(@" ZIndex=""" + ZIndex + @""">");

            xml.Append(Environment.NewLine);
            xml.Append("        </Activity>");

            return xml.ToString();
        }
        public string ToEZPLString()
        {
            string EZPLString = "";
            EZPLString += "A";
            if (sdPicture.FontSize == 18)
            {
                EZPLString += "A,";
            }
            else if (sdPicture.FontSize == 24)
            {
                EZPLString += "B,";
            }
            else if (sdPicture.FontSize == 30)
            {
                EZPLString += "C,";
            }
            EZPLString += ((int)CenterPoint.X).ToString() + "," + ((int)CenterPoint.Y).ToString() + ",1,1,0,";
            EZPLString += (Type == ActivityType.INTERACTION ? 0 : 4).ToString() + ",";
            EZPLString += ActivityName;
            return EZPLString;
        }
        public void LoadFromXmlString(string xmlString)
        {
        }




        public event MoveDelegate ActivityMove;
        public event DeleteDelegate DeleteActivity;

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
                Storyboard story = (Storyboard)containerUI.FindResource("sbClose");
                story.Completed += new EventHandler(sbClose_Completed);
                story.Begin();
            }

        }

        void sbClose_Completed(object sender, EventArgs e)
        {
            if (isDeleted)
            {
                if (DeleteActivity != null)
                    DeleteActivity(this);

                _container.RemoveActivity(this);

                //if (ActivityChanged != null)
                //    ActivityChanged(this);
            }
        }

        public void Move(Activity a, MouseEventArgs e)
        {
            if (ActivityMove != null)
                ActivityMove(a, e);
        }

        Activity originActivity;
        public Activity OriginActivity
        {
            get
            {
                return originActivity;
            }
            set
            {
                originActivity = null;
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
        string activityID;
        public string ActivityID
        {
            get
            {

                return activityID;
            }
            set
            {
                activityID = value;
            }

        }
        public string ActivityName
        {
            get
            {
                return sdPicture.AcitivtyName;
            }
            set
            {
                sdPicture.AcitivtyName = value;
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
        public Activity(IContainer container, ActivityType at, double _fontsize)
        {
            InitializeComponent();
            _container = container;
            editType = PageEditType.Add;
            this.Type = at;
            this.sdPicture.FontSize = _fontsize;
            //this.Name = UniqueID;


            _doubleClickTimer = new System.Windows.Threading.DispatcherTimer();
            _doubleClickTimer.Interval = new TimeSpan(0, 0, 0, 0, 400);
            _doubleClickTimer.Tick += new EventHandler(DoubleClick_Timer);

            Storyboard story = (Storyboard)containerUI.FindResource("sbDisplay");
            story.Begin();

        }

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
                if ((CenterPoint.X - sdPicture.PictureWidth / 2 < 2 && deltaH < 0)
                    || (CenterPoint.Y - sdPicture.PictureHeight / 2 < 2 && deltaV < 0)
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
                _container.ShowActivitySetting(this);

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
                activityChange();
        }
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {

            //ToolTip tt = new ToolTip();
            //ttActivityTip.Content = ActivityData.ActivityName + "\r\n" + ActivityData.ActivityType;
            return;

        }
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {

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
                    sdPicture.SetSelectedColor();

                    if (!_container.CurrentSelectedControlCollection.Contains(this))
                        _container.AddSelectedControl(this);



                }
                else
                {
                    sdPicture.ResetInitColor();
                }
            }

        }


        public bool PointIsInside(Point p)
        {
            bool isInside = false;


            double thisWidth = sdPicture.PictureWidth;
            double thisHeight = sdPicture.PictureHeight;

            double thisX = CenterPoint.X - thisWidth / 2;
            double thisY = CenterPoint.Y - thisHeight / 2;

            if (thisX < p.X && p.X < thisX + thisWidth
                && thisY < p.Y && p.Y < thisY + thisHeight)
            {
                isInside = true;
            }


            return isInside;
        }



        public Activity Clone()
        {
            Activity clone = new Activity(this._container, this.Type, this.FontSize);
            clone.originActivity = this;
            clone.ActivityData = new ActivityComponent();
            clone.ActivityData.ActivityName = this.ActivityData.ActivityName;
            clone.ActivityData.ActivityType = this.ActivityData.ActivityType;
            clone.ActivityData.FontSize = this.ActivityData.FontSize;
            clone.setUIValueByActivityData(clone.ActivityData);
            // clone.CenterPoint = this.CenterPoint;
            clone.CenterPoint = this.CenterPoint;
            clone.ZIndex = this.ZIndex;
            //_container.AddActivity(clone);

            return clone;
        }

        void activityChange()
        {
            if (ActivityChanged != null)
                ActivityChanged(this);
        }
    }
}
