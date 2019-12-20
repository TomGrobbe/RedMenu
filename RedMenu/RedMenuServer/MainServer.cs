using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using RedMenuShared;

namespace RedMenuServer
{
    public class MainServer : BaseScript
    {
        public MainServer()
        {
            if (!ConfigManager.EnablePermissions && !ConfigManager.IgnoreConfigWarning)
            {
                Debug.WriteLine("^3[WARNING] RedMenu is setup to ignore permissions! If this was not intended, please read the installation instructions. You can silence this warning by adding ^7setr ignore_config_warning \"true\" ^3to your server.cfg, above the ^7start RedMenu ^3line.^7");
            }
            else if (!ConfigManager.IgnoreConfigWarning)
            {
                Debug.WriteLine("^2[RedMenu] You successfully executed the config.cfg file, good job.^7");
            }

            if (GetCurrentResourceName() != "RedMenu")
            {
                Debug.WriteLine("^1[ERROR] RedMenu is not correctly installed. Please make sure that the folder is called RedMenu (case sensitive)! RedMenu will not function if it's incorrectly named.^7");
                throw new Exception("Installation error: Invalid resource name.");
            }
            else
            {
                Debug.WriteLine("^2[RedMenu] The resource is installed in the correct folder, well done.^7");
            }
        }
    }
}
