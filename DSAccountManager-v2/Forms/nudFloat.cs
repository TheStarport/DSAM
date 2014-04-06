using System.Windows.Forms;

namespace DSAccountManager_v2.Forms
{
    public class NudFloat : NumericUpDown
    {
        public new float Value
        {
            get { return (float)base.Value; }
            set { base.Value = (decimal)value; }
        }
    }
}
