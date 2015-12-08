using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gwearable
{
    class MouseControl
    {
        protected Control newCtrl;
        protected MouseButtons FinalClick;
        public event EventHandler LeftClick;
        public event EventHandler RightClick;
        public event MouseEventHandler LeftMouseDown;
        public event MouseEventHandler LeftMouseUp;
        public event MouseEventHandler RightMouseDown;
        public event MouseEventHandler RightMouseUp;
        public event MouseEventHandler LRMouseMove;
        public event MouseEventHandler LRMouseWheel;

        public Control Control
        {
            get { return newCtrl; }
            set
            {
                newCtrl = value;
                Initialize();
            }
        }

        public MouseControl()
        {
        }

        public MouseControl(Control ctrl)
        {
            Control = ctrl;
        }

        public void AddControl(Control ctrl)
        {
            Control = ctrl;
        }

        protected virtual void Initialize()
        {
            newCtrl.Click += new EventHandler(OnClick);
            newCtrl.MouseDown += new MouseEventHandler(OnMouseDown);
            newCtrl.MouseUp += new MouseEventHandler(OnMouseUp);
            newCtrl.MouseMove += new MouseEventHandler(OnMouseMove);
            newCtrl.MouseWheel += new MouseEventHandler(OnMouseWheel);
        }

        private void OnClick(object sender, EventArgs e)
        {
            switch (FinalClick)
            {
                case MouseButtons.Left:
                    if (LeftClick != null)
                    {
                        LeftClick(sender, e);
                    }
                    break;

                case MouseButtons.Right:
                    if (RightClick != null)
                    {
                        RightClick(sender, e);
                    }
                    break;
            }
        }

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {

            switch (e.Button)
            {
                case MouseButtons.Middle:
                    LRMouseWheel(sender, e);
                    break;
            }

        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    LRMouseMove(sender, e);
                    break;
                case MouseButtons.Right:
                    LRMouseMove(sender, e);
                    break;
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            FinalClick = e.Button;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        if (LeftMouseDown != null)
                        {
                            LeftMouseDown(sender, e);
                        }
                        break;
                    }

                case MouseButtons.Right:
                    {
                        if (RightMouseDown != null)
                        {
                            RightMouseDown(sender, e);
                        }
                        break;
                    }
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        if (LeftMouseUp != null)
                        {
                            LeftMouseUp(sender, e);
                        }
                        break;
                    }
                case MouseButtons.Right:
                    {
                        if (RightMouseUp != null)
                        {
                            RightMouseUp(sender, e);
                        }
                        break;
                    }
            }
        }
    }
}
