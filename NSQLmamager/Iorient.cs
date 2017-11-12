using Newtonsoft.Json;
using System;

using IQueryManagers;

namespace IOrientObjects
{

    //For all OrientDb objects (Vertexes and Edges both)   
    public interface IOrientObject
    {

    }
    public interface IOrientDatabase : IOrientObject
    {

    }
    public interface IOrientClass : IOrientObject
    {

    }
    public interface IOrientProperty : IOrientObject
    {

    }
    public interface IOrientVertex : IOrientObject
    {
        [JsonProperty("@type")]
        string type { get; set; }
        [JsonProperty("@rid", Order = 0)]
        string id { get; set; }
        [JsonProperty("@version")]
        string version { get; set; }
        [JsonProperty("@class")]
        string class_ { get; set; }
    }
    //Specific for OrientDb (additional for Edges)
    public interface IOrientEdge : IOrientVertex
    {
        string Out { get; set; }
        string In { get; set; }
    }

    public interface ITypeConverter
    {
        void Add(Type type_, ITypeToken token_);
        ITypeToken Get(IOrientVertex object_);
        ITypeToken Get(IOrientDatabase object_);
        ITypeToken GetBase(IOrientVertex object_);
        ITypeToken Get(Type type_);
        ITypeToken GetBase(Type type_);
    }

}