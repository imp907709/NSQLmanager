using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using JsonManagers;
using WebManagers;
using QueryManagers;
using POCO;


using IOrientObjects;
using OrientRealization;
using Repos;
using UOWs;

namespace NSQLmanager
{

    class OrientDriverConnnect
    {

        static void Main(string[] args)
        {            
            Trash.RepoCheck rc = new Trash.RepoCheck();
            rc.GO();
        }

    }

}

