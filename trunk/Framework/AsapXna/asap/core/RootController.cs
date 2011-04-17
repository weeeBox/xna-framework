﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace asap.core
{
    public class RootController : TickListener
    {
        private Controller activeController;

        public virtual void OnStart()
        {
            
        }

        public virtual void OnStop()
        {

        }

        public virtual void OnSuspend()
        {
            
        }

        public virtual void OnResume()
        {

        }

        public void StartController(Controller controller, int param)
        {
            Debug.WriteLine(controller.GetType().Name + " started with param " + param);

            activeController = controller;
            activeController.parent = null;
            activeController.Start(param);
        }

        public void StartChildController(Controller child, int param)
        {
            Debug.WriteLine(child.GetType().Name + " started as a child");

            Controller parentController = activeController;
            activeController = parentController;
            activeController.parent = parentController;
            activeController.Start(param);
        }

        public void OnControllerStop(Controller controller, int param)
        {
            Debug.Assert(activeController == controller);
            
            activeController = controller.parent;
            if (activeController != null)
            {
                Debug.WriteLine("Return to " + activeController.GetType().Name);
                activeController.OnChildStop(controller, param);
            }            
        }

        public void Tick(float delta)
        {
            activeController.Tick(delta);
        }
    }
}
