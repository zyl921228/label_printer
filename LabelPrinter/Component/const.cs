using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LabelPrinter.Component
{
    public class SystemConst
    {
        public class ColorConst
        {
            public static Brush WarningColor
            {
                get
                {
                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = Color.FromArgb(255, 255, 0, 0);
                    return brush;
                }
            }
            public static Brush SelectedColor
            {
                get
                {
                    SolidColorBrush brush = new SolidColorBrush();
                    brush.Color = Color.FromArgb(255, 255, 181, 0);
                    return brush;
                }
            }

        }
        public static double RuleThickness
        {
            get
            {
                return 2.0;
            }
        }
    }
}
