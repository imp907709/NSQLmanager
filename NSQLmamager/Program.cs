using System;
using System.Collections.Generic;
using System.Linq;

using JsonManagers;
using WebManagers;
using QueryManagers;
using POCO;

using OrientRealization;

/// <summary>
/// install-package Newtonsoft.Json
/// add reference to FRAMEWORK system.configuration
/// /// add reference to FRAMEWORK system.net.http
/// If any fuck up /broke references appear
/// 1) delete package folder and config
/// 2) clean reference folder
/// 3) refere  system.net.http again
/// </summary>
namespace ConsoleApp1
{

    class OrientDriverConnnect
    {

        static void Main(string[] args)
        {
            RepoCheck rc = new RepoCheck();
            rc.GO();
        }

    }


    //boilerplate usage
    public class RepoCheck
    {
        JSONManager jm;
        OrientTokenBuilder tb;
        TypeConverter tc;
        Textbuilder ocb;
        WebManager wm ;
        WebResponseReader wr;

        OrientRepo repo;
        Person p;
        Unit u;
        SubUnit s;

        MainAssignment m;
        List<string> lp,lu;

        public RepoCheck()
        {
            jm = new JSONManager();
            tb = new OrientTokenBuilder();
            tc = new TypeConverter();
            ocb = new OrientCommandBuilder();
            wm = new WebManager();
            wr = new WebResponseReader();          

            repo = new OrientRepo(jm, tb, tc, ocb, wm, wr);

            s = new SubUnit();

            p =
new Person() { Name = "0", GUID = "0", Changed = new DateTime(2017, 01, 01, 00, 00, 00), Created = new DateTime(2017, 01, 01, 00, 00, 00) };

            u =
new Unit() { Name = "0", GUID = "0", Changed = new DateTime(2017, 01, 01, 00, 00, 00), Created = new DateTime(2017, 01, 01, 00, 00, 00) };

            m =
new MainAssignment() { Name = "0", GUID = "0", Changed = new DateTime(2017, 01, 01, 00, 00, 00), Created = new DateTime(2017, 01, 01, 00, 00, 00) };

           lp = new List<string>();
           lu = new List<string>();
        }



        public void GO()
        {

            AddCheck();
            DeleteCheck();
        }
        public void AddCheck()
        {
            int lim = 500;

            for (int i = 0; i <= lim; i++)
            {
                lp.Add(jm.DeserializeFromParentNode<Person>(repo.Add(p), new RESULT().Text).Select(s => s.id.Replace(@"#","")).FirstOrDefault());
                lu.Add(jm.DeserializeFromParentNode<Unit>(repo.Add(u), new RESULT().Text).Select(s=>s.id.Replace(@"#", "")).FirstOrDefault());
              
            }
            for (int i = 0; i <= lim/2; i++)
            {              
                repo.Add(m, new TextToken() { Text = lp[i ] }, new TextToken() { Text = lp[i + 1] });                
            }
            for (int i = 0; i <= lim / 2; i++)
            {
                repo.Add(s, new TextToken() { Text = lu[i] }, new TextToken() { Text = lu[i + 1] });
            }
           

        }
        public void DeleteCheck()
        {
            string str;
            str = repo.Delete(Person);
            str = repo.Delete(typeof(Unit));
            str = repo.Delete(typeof(MainAssignment));
            str = repo.Delete(typeof(SubUnit));
        }
    }
    

}

