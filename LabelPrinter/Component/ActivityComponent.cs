using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinter.Component
{
    public class ActivityComponent
    {
        double _fontSize;
        public double FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
            }
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

        string _repeatDirection = "Horizontal";
        public string RepeatDirection
        {
            get
            {
                return _repeatDirection;
            }
            set
            {
                _repeatDirection = value;
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

        string activityName;

        public string ActivityName
        {
            get
            {
                return activityName;
            }
            set
            {
                activityName = value;
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

        string activityType;
        public string ActivityType
        {
            get
            {
                return activityType;
            }
            set
            {
                activityType = value;
            }
        }
    }
}
