using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine.Utility;
using Create_WI_from_Template;

namespace SMTemplateCreate
{
    class Program
    {
        static void Main(string[] args)
        {
            Arguments CommandLine = new Arguments(args);

            if (CommandLine["help"] != null || CommandLine["h"] != null || CommandLine["?"] != null)
            {
                DisplayHelp();
            }
            else
            {
                // Get computer name set to localhost if not speicified
                string computername = CommandLine["computername"];
                if (computername == null)
                {
                    computername = "localhost";
                }

                // Check for valid Guid in templateid
                Guid templateid;
                TryParseGuid(CommandLine["templateid"], out templateid);

                try
                {
                    // Create Work Item
                    CreateWI WI = new CreateWI();
                    WI.TemplateID = templateid;
                    WI.emg = CreateWI.GetManagementGroupConnection(computername);
                    string strWI = WI.WorkItem();
                    if (strWI != null)
                    {
                        Console.WriteLine(String.Format("{0} ", strWI));
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.InnerException.Message);
                }
            }
        }

        public static bool TryParseGuid(string guidString, out Guid guid)
        {
            if (guidString == null) throw new System.ArgumentException("Parameter cannot be null", "templateid");
            try
            {
                guid = new Guid(guidString);
                return true;
            }
            catch (FormatException)
            {
                guid = default(Guid);
                throw new System.ArgumentException(guidString + " is not a valid Guid !", "templateid");
            }
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("Creates a Work Item in Service Manager from a Template\n");
            Console.WriteLine("SMTemplateCreate [/computername string] [/templateid Guid]\n");
            Console.WriteLine("  /computername      Name of Service Manager management server. Defaults to localhost if not specified");
            Console.WriteLine("  /templateid        The Guid of the template you want to create the new work item from\n");
            Console.WriteLine("EXAMPLE:   SMTemplateCreate.exe /computername sm1 /templateid e0287ab6-089e-5172-0534-49edbd841f34");
        }

    }
}
