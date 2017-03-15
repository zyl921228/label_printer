using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LabelPrinter.Designer
{
    public interface IContainer
    {
        void ShowMessage(string message);
        List<Activity> ActivityCollections { get; }
        void AddActivity(Activity a);
        void RemoveActivity(Activity a);
        void RemoveBarCode(BarCode b);
        void AddBarCode(BarCode b);
        int NextMaxIndex { get; }
        string ToXmlString();
        void LoadFromXmlString(string xmlString);
        PageEditType EditType { get; set; }

        void ShowActivitySetting(Activity ac);
        void ShowBarCodeSetting(BarCode bc);
        List<System.Windows.Controls.Control> CurrentSelectedControlCollection { get; }
        void AddSelectedControl(System.Windows.Controls.Control uc);
        void RemoveSelectedControl(System.Windows.Controls.Control uc);
        void SetWorkFlowElementSelected(System.Windows.Controls.Control uc, bool isSelect);
        void MoveControlCollectionByDisplacement(double x, double y, UserControl uc);
        bool CtrlKeyIsPress { get; }
        double ContainerWidth { get; set; }
        double ContainerHeight { get; set; }
        double ScrollViewerHorizontalOffset { get; set; }
        double ScrollViewerVerticalOffset { get; set; }
        void ClearSelectFlowElement(System.Windows.Controls.Control uc);
        void SaveChange(HistoryType action);
        int NextNewActivityIndex { get; }
        void CopySelectedControlToMemory(System.Windows.Controls.Control currentControl);
        void PastMemoryToContainer();
        void PreviousAction();
        void NextAction();
        List<System.Windows.Controls.Control> CopyElementCollectionInMemory { get; set; }
        System.Collections.Generic.Stack<string> WorkFlowXmlPreStack { get; }
        System.Collections.Generic.Stack<string> WorkFlowXmlNextStack { get; }

        void DeleteSeletedControl();
        bool IsMouseSelecting { get; }
        CheckResult CheckSave();
        bool Contains(UIElement uiel);
        bool MouseIsInContainer { get; set; }

    }
}
