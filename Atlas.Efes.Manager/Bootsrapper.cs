
using BigCinch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Atlas.Efes.Manager
{
    public class Bootsrapper
    {
        public void Run()
        {
           
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            Assembly viewRegisterRef = AppDomain.CurrentDomain.GetAssemblies().Where(f => f.ManifestModule.Name == "Atlas.Efes.Manager.exe").FirstOrDefault();

            if (viewRegisterRef == null)
            {
                //throw new NotImplementedException("Atlantis View Ref Dll could not load");
            }

            if (viewRegisterRef != null)
            {

                CinchBootStrapper.Initialise(new List<Assembly> 
                { 
                    {
                        viewRegisterRef
                    }
                });
            }

            MasterWindow master = new MasterWindow();
            master.ShowDialog();
        }
    }
}
