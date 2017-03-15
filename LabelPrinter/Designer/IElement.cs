using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinter.Designer
{
    public enum MergePictureRepeatDirection { Vertical = 0, Horizontal, None }
    public enum ActivityType { AND_BRANCH = 0, AND_MERGE, AUTOMATION, COMPLETION, DUMMY, INITIAL, INTERACTION, OR_BRANCH, OR_MERGE, SUBPROCESS, VOTE_MERGE }
    public enum BarCodeType { Code39, Code128 }
    public enum WorkFlowElementType { Activity = 0, Rule, Label, BarCode }
    public enum PageEditType { Add = 0, Modify, None }
    public enum RuleLineType { Line = 0, Polyline }
    public enum HistoryType { New, Next, Previous };
    public class CheckResult
    {
        bool isPass = true;
        public bool IsPass { get { return isPass; } set { isPass = value; } }
        string message = "";
        public string Message { get { return message; } set { message = value; } }
    }

    public interface IElement
    {

        CheckResult CheckSave();

        string ToXmlString();
        string ToEZPLString();
        void LoadFromXmlString(string xmlString);
        void ShowMessage(string message);
        WorkFlowElementType ElementType { get; }

        PageEditType EditType { get; set; }

        bool IsSelectd { get; set; }
        IContainer Container { get; set; }
        void Delete();
        void UpperZIndex();
        bool IsDeleted { get; }
        void Zoom(double zoomDeep);

    }
}
