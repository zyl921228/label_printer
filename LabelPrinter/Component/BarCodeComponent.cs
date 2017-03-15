using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinter.Component
{
    public class BarCodeComponent
    {
        string barCodeType;
        public string BarCodeType
        {
            get
            {
                return barCodeType;
            }
            set
            {
                barCodeType = value;
            }
        }

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

        int barCodeWidth;

        public int BarCodeWidth
        {
            get
            {
                return barCodeWidth;
            }
            set
            {
                barCodeWidth = value;
            }
        }

        int barCodeHeight;

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





    }
}
