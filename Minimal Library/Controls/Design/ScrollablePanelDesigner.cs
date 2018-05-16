using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace MinimalLibrary.Controls.Design
{
    class ScrollablePanelDesigner : ParentControlDesigner
    {

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            MScrollablePanel panel = component as MScrollablePanel;
            this.EnableDesignMode(panel.Panel, "Panel");
        }

    }
}
