using System;
using System.Collections.Generic;
using System.Linq;

using IUOW;
using IOrientObjects;
using IQueryManagers;
using IJsonManagers;
using IWebManagers;
using POCO;
using OrientRealization;
using QueryManagers;

namespace UOWs
{
    
    public class PersonUOW : IPersonUOW
    {
        IRepos.IRepo _repo;      
        ITypeConverter _typeConverter;
        ITokenAggreagtor _textBuilder;
        IJsonManger _jsonManager;
        ITokenCompilator _tokenAggregator;
        IWebManager _webManager;
        IResponseReader _responseReader;

        public PersonUOW(IRepos.IRepo repo_,ITypeConverter typeConverter_,ITokenAggreagtor textBuilder_,IJsonManger jsonManager_,ITokenCompilator tokenAggregator_,
        IWebManager webManager_, IResponseReader responseReader_)
        {
            _jsonManager = jsonManager_;
            _tokenAggregator = tokenAggregator_;
            _typeConverter = typeConverter_;
            _textBuilder = textBuilder_;
            _webManager = webManager_;
            _responseReader = responseReader_;

            _repo = repo_;
        }


        public IEnumerable<Person> GetObjByGUID(string GUID)
        {
            IEnumerable<Person> result = null;
            TextToken condition_ = new TextToken() { Text = "1=1 and GUID ='" + GUID + "'" };
            try
            {
                result = _repo.Select<Person>(typeof(Person), condition_);
            }
            catch (Exception e) { System.Diagnostics.Trace.WriteLine(e.Message); }

            return result;
        }
        public string GetByGUID(string GUID)
        {
            string result = string.Empty;
            IEnumerable<Person> persons = null;
            TextToken condition_ = new TextToken() { Text = "1=1 and GUID ='" + GUID + "'" };
            try
            {
                persons = _repo.Select<Person>(typeof(Person), condition_);
                result = _jsonManager.SerializeObject(persons);
            }
            catch (Exception e) { System.Diagnostics.Trace.WriteLine(e.Message); }

            return result;
        }
        public IEnumerable<Person> GetAll()
        {

            IEnumerable<Person> result = null;
            TextToken condition_ = new TextToken() { Text = "1=1" };
            try
            {
                result = _repo.Select<Person>(typeof(Person), condition_);

            }
            catch (Exception e) { System.Diagnostics.Trace.WriteLine(e.Message); }

            return result;
        }

        public string GetTrackedBirthday(string GUID)
        {
            string result = string.Empty;
            IEnumerable<Person> persons = null;
            TextToken condition_ = new TextToken() { Text = "1=1 and GUID ='" + GUID + "'" };
            List<ITypeToken> tokens = _tokenAggregator.outEinVExp(new OrientSelectToken(),
                _typeConverter.Get(typeof(Person)), _typeConverter.Get(typeof(TrackBirthdays)), condition_);
            string command = _textBuilder.Build(tokens, new OrientOutEinVFormat() { });
            persons = _repo.Select<Person>(command);
            result = _jsonManager.SerializeObject(persons);
            return result;
        }

        public string AddTrackBirthday(IOrientEdge edge, string guidFrom, string guidTo)
        {
            Person from = GetObjByGUID(guidFrom).FirstOrDefault();
            Person to = GetObjByGUID(guidTo).FirstOrDefault();
            TrackBirthdays tb = new TrackBirthdays();
            string result = null;
            if (from != null && to != null)
            {
                result = _repo.Add(tb, from, to);
            }
            return result;
        }

    }

}
