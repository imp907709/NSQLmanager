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
        OrientTokenBuilderImplicit tb;
        TypeConverter tc;
        Textbuilder ocb;
        WebManager wm ;
        WebResponseReader wr;

        OrientRepo repo;
        Person p;
        Unit u;
        SubUnit s;

        UserSettings us;
        CommonSettings cs;

        MainAssignment m;
        List<string> lp,lu;

        public RepoCheck()
        {
            jm = new JSONManager();
            tb = new OrientTokenBuilderImplicit();
            tc = new TypeConverter();
            ocb = new OrientCommandBuilder();
            wm = new WebManager();
            wr = new WebResponseReader();          

            repo = new OrientRepo(jm, tb, tc, ocb, wm, wr);

            us = new UserSettings() { showBirthday = true };
            cs = new CommonSettings();

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
            BirthdayConditionAdd();
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
            str = repo.Delete(typeof(Person), new TextToken() { Text="1=1"});
            str = repo.Delete(typeof(Unit), new TextToken() { Text = "1=1" });
            str = repo.Delete(typeof(MainAssignment), new TextToken() { Text = "1=1" });
            str = repo.Delete(typeof(SubUnit), new TextToken() { Text = "1=1" });
		  }
        public void ExplicitCommandsCheck()
        {

            OrientCommandBuilder cb = new OrientCommandBuilder();
            OrientTokenBuilderExplicit eb = new OrientTokenBuilderExplicit();
            TypeConverter tc = new TypeConverter();

            List<IQueryManagers.ITypeToken> lt = new List<IQueryManagers.ITypeToken>();
            List<string> ls = new List<string>();


lt = eb.Create(new OrientClassToken() { Text = "VSCN" }, new OrientClassToken() { Text = "V" });
ls.Add(cb.Build(lt, new TextFormatGenerate(lt)));

lt = eb.Create(new OrientClassToken() { Text = "VSCN" }, new OrientPropertyToken() { Text = "Name" }, new OrientSTRINGToken(), true,true);
ls.Add(cb.Build(lt, new TextFormatGenerate(lt)));

lt = eb.Create(new OrientClassToken() { Text = "VSCN" }, new OrientPropertyToken() { Text = "Created" }, new OrientDATEToken(), true, true);
ls.Add(cb.Build(lt, new TextFormatGenerate(lt)));



lt = eb.Create(new OrientClassToken() { Text = "ESCN" }, new OrientClassToken() { Text = "E" });
ls.Add(cb.Build(lt, new TextFormatGenerate(lt)));

lt = eb.Create(new OrientClassToken() { Text = "ESCN" }, new OrientPropertyToken() { Text = "Name" }, new OrientSTRINGToken(), true, true);
ls.Add(cb.Build(lt, new TextFormatGenerate(lt)));

lt = eb.Create(new OrientClassToken() { Text = "ESCN" }, new OrientPropertyToken() { Text = "Created" }, new OrientDATEToken(), true, true);
ls.Add(cb.Build(lt, new TextFormatGenerate(lt)));



lt = eb.Create(new OrientClassToken() { Text = "VSCN" }, new OrientClassToken() { Text = "VSCN" });
ls.Add(cb.Build(lt, new TextFormatGenerate(lt)));

lt = eb.Create(new OrientClassToken() { Text = "Beer" }, new OrientClassToken() { Text = "VSCN" });
ls.Add(cb.Build(lt, new TextFormatGenerate(lt)));

lt = eb.Create(new OrientClassToken() { Text = "Produces" }, new OrientClassToken() { Text = "ESCN" });
ls.Add(cb.Build(lt, new TextFormatGenerate(lt)));



        }
        public void BirthdayConditionAdd()
        {

            List<string> persIds = new List<string>();
            List<string> edgeIds = new List<string>();

            persIds.AddRange(
                jm.DeserializeFromParentNode<Person>(repo.Select(typeof(Person), new TextToken() { Text = "1=1 and outE(\"CommonSettings\").inv(\"UserSettings\").showBirthday[0] is null" }), new RESULT().Text).Select(s => s.id.Replace(@"#", ""))
            );

            for (int i = 0; i < persIds.Count(); i++)
            {
                string id = jm.DeserializeFromParentNode<UserSettings>(repo.Add(us), new RESULT().Text).Select(s => s.id.Replace(@"#", "")).FirstOrDefault();

                repo.Add(cs, new TextToken() { Text = persIds[i] }, new TextToken() { Text = id });
            }

            repo.Delete(typeof(UserSettings), new TextToken() { Text = @"1 =1" });
            repo.Delete(typeof(CommonSettings), new TextToken() { Text = @"1 =1" });

        }
    }
    

}

