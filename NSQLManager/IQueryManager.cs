
using System;
using System.Reflection;
using System.Collections.Generic;

using IOrientObjects;

namespace IQueryManagers
{

    /// <summary>
    /// Main manager interfaces
    /// </summary>

    //For token items
    /// <summary>
    /// Tokens for Orient API URIs 
    /// Different API types tend to different Http req strategies example: Fucntion/param or: Batch/ + JSON-body
    /// (add types to ItypeToken for plugging-in)
    /// </summary>  
    public interface ITypeToken
    {
        string Text {get; set;}      
    }

    public interface ITokenMiniFactory
    {
        ITypeToken NewToken(string text_=null);
        ITypeToken EmptyString();

        ITypeToken Dot();
        ITypeToken Coma();
        ITypeToken Gap();

        ITypeToken Deleted();
        ITypeToken Created();

        ITypeToken Apostrophe();
    }
    
    
    public interface IBodyShemas
    {
      ICommandBuilder Batch(ICommandBuilder command_);
      ICommandBuilder Command(ICommandBuilder command_);
      ICommandBuilder SelectCommand(ICommandBuilder command_);
    }

    
    public interface IUrlShemasExplicit
    {
      void AddHost(string host_);
      ICommandBuilder Batch(ITypeToken databaseName_);
      void Build(List<ITypeToken> tokenList_, ITypeToken format = null);
      ICommandBuilder Command(ITypeToken databaseName_);
      ICommandBuilder Connect(ITypeToken databaseName_);
      ICommandBuilder Database(ITypeToken databaseName_);
      ITypeToken GetHost();
      void ReBuild(List<ITypeToken> tokenList_, ITypeToken format = null);
      void ReBuildDelimeter(List<ITypeToken> tokenList_, ITypeToken delimeter_);
    }

    public interface ICommandsChain
    {
      ICommandsChain And(ITypeToken param_ = null);
      ICommandsChain As(ITypeToken aliace_);
      ICommandsChain Between(ITypeToken param_ = null);
      ICommandsChain Class(ITypeToken param_ = null);
      ICommandsChain ClassCheck(ITypeToken param_);
      ICommandsChain Coma();
      ICommandsChain Content(ITypeToken param_ = null);
      ICommandsChain Content(ICommandBuilder param_ = null);
      ICommandsChain Create(ITypeToken param_ = null);
      ICommandsChain Delete(ITypeToken param_ = null);
      ICommandsChain Dot();
      ICommandsChain Edge(ITypeToken param_ = null);
      ICommandsChain Expand(ITypeToken aliace_);
      ICommandsChain Extends(ITypeToken param_ = null);
      ICommandsChain From(ITypeToken param_ = null);
      ICommandsChain FromV(ITypeToken param_ = null);
      ICommandsChain Gap();
      ICommandBuilder GetBuilder();
      string GetCommand();
      IFormatFromListGenerator GetGenerator();
      ICommandsChain In(List<ITypeToken> param_);
      ICommandsChain In(ITypeToken param_);
      ICommandsChain InE(ITypeToken param_);
      ICommandsChain InV(ITypeToken param_);
      ICommandsChain Nest(ITypeToken leftToken, ITypeToken rightToken);
      ICommandsChain Nest(ITypeToken leftToken_ = null, ITypeToken rightToken_ = null, ITypeToken format = null);
      ICommandsChain NestRnd();
      ICommandsChain NestSq();
      ICommandsChain Out(List<ITypeToken> param_);
      ICommandsChain Out(ITypeToken param_);
      ICommandsChain OutE(ITypeToken param_);
      ICommandsChain OutV(ITypeToken param_);
      ICommandsChain Property(ITypeToken class_, ITypeToken property_, ITypeToken type_, ITypeToken mandatory_, ITypeToken notnull_);
      ICommandsChain Select(ICommandBuilder columns_ = null);
      ICommandsChain ToV(ITypeToken param_ = null);
      ICommandsChain Traverse(ICommandBuilder columns_ = null);
      ICommandsChain Update(ITypeToken param_ = null);
      ICommandsChain Vertex(ITypeToken param_ = null);
      ICommandsChain Where(ITypeToken param_ = null);
      ICommandsChain Where(ICommandBuilder param = null);
    }

    public interface IOrientQueryFactory
    {

        ITypeToken AlterToken();
        ITypeToken DefaultToken();
        ITypeToken UUIDToken();
        ITypeToken DoubleQuotes();

        ITypeToken Between();

        ITypeToken UpdateToken();

        ITypeToken ClassToken();
        ITypeToken ContentToken();
        ITypeToken CreateToken();
        ITypeToken DeleteToken();
        ITypeToken EdgeToken();
        ITypeToken Equals();
        ITypeToken ExtendsToken();
        ITypeToken FromToken();
        ITypeToken LeftRoundBraket();
        ITypeToken LeftSquareBraket();
        ITypeToken Mandatory();
        ITypeToken NotNull();
        ITypeToken PropertyItemFormatToken();
        ITypeToken PropertyToken();
        ITypeToken PropertyTypeToken();
        ITypeToken RightRoundBraket();
        ITypeToken RightSquareBraket();
        ITypeToken SelectToken();
        ITypeToken TraverseToken();
        ITypeToken ToToken();
        ITypeToken VertexToken();
        ITypeToken WhereToken();
        ITypeToken AndToken();

        ITypeToken In();
        ITypeToken Out();
        ITypeToken E();
        ITypeToken V();

        ITypeToken As();

        ITypeToken Expand();

        ITypeToken Dog();
    }
    public interface IOrientBodyFactory
    {

        ITypeToken BackSlash();
        ITypeToken Slash();
        ITypeToken Colon();
        ITypeToken Comma();
        ITypeToken Batch();
        ITypeToken Command();
        ITypeToken PLocal();
        ITypeToken Database();
        ITypeToken Connect();
        ITypeToken Content();
        ITypeToken Language();
        ITypeToken sql();
        ITypeToken LeftFgGap();
        ITypeToken RightFgGap();
        ITypeToken LeftSqGap();
        ITypeToken RightSqGap();
        ITypeToken Operations();
        ITypeToken Quotes();
        ITypeToken Sctipt();
        ITypeToken Transactions();
        ITypeToken True();
        ITypeToken False();
        ITypeToken Type();

        ITypeToken StringToken();
        ITypeToken BooleanToken();

    }

    public interface ICommandFactory
    {
        ICommandBuilder CommandBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatFactory_);

        ICommandBuilder CommandBuilder(ITokenMiniFactory tokenFactory_, IFormatFactory formatFactory_
            , List<ITypeToken> tokens_, ITypeToken format_);


    }

    public interface IFormatFactory
    {
        IFormatFromListGenerator FormatGenerator(ITokenMiniFactory tokkenFactory_);
    }

    public interface IFormatFromListGenerator
    {
        ITypeToken FromatFromTokenArray(List<ITypeToken> tokens_, ITypeToken delimeter_=null);
        ITypeToken FromatFromTokenArray(List<ICommandBuilder> tokens_, ITypeToken delimeter_=null);

    }

    //Building Item from Token types
    public interface ICommandBuilder
    {
        IFormatFromListGenerator formatGenerator {get;}
        ITypeToken typeToken {get;}
        ITypeToken Text {get;}
        ITypeToken FormatPattern {get; set;}
        List<ITypeToken> Tokens {get;}

        void BindTokens(List<ITypeToken> tokens_);
        void AddTokens(List<ITypeToken> tokens_);
        void BindFormat(ITypeToken formatPatern_);
        void AddFormat(ITypeToken formatPatern_);
        void BindFormatGenerator(IFormatFromListGenerator formatGenerator_);

        void BindBuilders(List<ICommandBuilder> texts_, ITypeToken FormatPattern_=null);

        ICommandBuilder Build();
        string Build(List<ICommandBuilder> tokens_, ITypeToken FormatPattern_);
        string GetText();
        void SetText(List<ITypeToken> tokens_, ITypeToken FormatPattern_);
    }

    /// <summary>
    /// Converts from Database and POCO classes to Tokens
    /// </summary>
    public interface ITypeConverter
    {
        void Add(Type type_, ITypeToken token_);
        ITypeToken Get(IOrientObject object_);
        ITypeToken GetBase(IOrientObject object_);
        ITypeToken Get(Type type_);
        ITypeToken GetBase(Type type_);
        ITypeToken Get(string object_);
        ITypeToken GetBase(string object_);

        Type GegtypeFromAsm(string typeName_, string asm_ = null);
    }

    public interface IPropertyConverter
    {      
        ITypeToken GetBoolean(bool bool_);
        ITypeToken Get(Type type_);
    }

    
    public interface IOrientRepo
    {
      void AlterProperty(ITypeToken class_, ITypeToken prop_, ITypeToken func_);
      void BindDbName(string dbName_);
      void BindUrlName(string input_);
      T ContentStringToObject<T>(string item_) where T : class, IorientDefaultObject;
      IOrientRepo CreateClass(string class_, string extends_, string dbName_ = null);
      Type CreateClass<T, K>(string dbName_ = null)
        where T : IOrientEntity
        where K : IOrientEntity;
      IOrientRepo CreateDb(string dbName_ = null, string host = null);
      T CreateEdge<T>(IOrientEdge edge_, IOrientVertex vFrom, IOrientVertex vTo, string dbName_ = null) where T : class, IOrientEdge;
      IOrientRepo CreateProperty(string class_, string property_, Type type_, bool mandatory_, bool notnull_, string dbName_ = null);
      T CreateProperty<T>(T item = null, string dbName_ = null) where T : class, IorientDefaultObject;
      IOrientRepo CreateVertex(string vertex, string content_ = null, string dbName_ = null);
      T CreateVertex<T>(IOrientVertex vertex, string dbName_ = null) where T : class, IOrientVertex;
      T CreateVertex<T>(string content_, string dbName_ = null) where T : class, IOrientVertex;
      void DbPredefinedParameters();
      IOrientRepo Delete<T>(T item = null, string condition_ = null, string dbName_ = null) where T : class, IorientDefaultObject;
      IOrientRepo DeleteDb(string dbName_ = null, string host = null);
      IOrientRepo DeleteEdge<T, K, C>(K from, C to, string condition_ = null, string dbName_ = null)
        where T : IOrientEdge
        where K : IOrientVertex
        where C : IOrientVertex;
      IOrientRepo DeleteEdge<T>(string from, string to, string condition_ = null, string dbName_ = null) where T : class, IOrientEdge;
      string GetResult();
      ICommandsChain NewChain();
      string ObjectToContentString<T>(IEnumerable<T> item_) where T : class, IorientDefaultObject;
      string ObjectToContentString<T>(T item_) where T : class, IorientDefaultObject;
      T OrientStringToObject<T>(string item_) where T : class, IorientDefaultObject;
      T PropertyTryReturnAttribute<T>(PropertyInfo p_) where T : Attribute;
      PropertyInfo[] Props<T>() where T : IOrientObject;
      IEnumerable<T> Select<T, InE>(T vertexFrom_ = null, string dbName_ = null)
        where T : class, IOrientVertex
        where InE : class, IOrientEdge;
      IEnumerable<InV> Select<T, OutE, InV>(T vertexFrom_ = null, string dbName_ = null)
        where T : class, IOrientVertex
        where OutE : class, IOrientEdge
        where InV : class, IOrientVertex;
      IEnumerable<T> SelectAll<T>(IorientDefaultObject t_, string dbName_ = null) where T : class, IorientDefaultObject;
      IEnumerable<T> SelectByGUIDfromType<T>(Type type_, string cond_ = null, string dbName_ = null) where T : class, IorientDefaultObject;
      IEnumerable<T> SelectByIDWithCondition<T>(string ID_, string cond_ = null, string dbName_ = null) where T : class, IorientDefaultObject;
      IEnumerable<InV> SelectCommentToComment<T, InE, InV>(T vertexFrom_ = null, string dbName_ = null)
        where T : class, IOrientVertex
        where InE : class, IOrientEdge
        where InV : class, IOrientVertex;
      IEnumerable<T> SelectFromTraverseWithOffset<T, outE, inV, inE, inE2>(string ID_, string depthPropName_, int depthFrom_, int? depthOfset_ = null, string dbName_ = null) where T : class, IorientDefaultObject;
      IEnumerable<T> SelectFromType<T>(string cond_ = null, string dbName_ = null) where T : class, IorientDefaultObject;
      IEnumerable<OutV> SelectInEOutV<T, InE, OutV>(T vertexFrom_ = null, string dbName_ = null)
        where T : class, IorientDefaultObject
        where InE : class, IOrientEdge
        where OutV : class, IOrientVertex;
      T SelectSingle<T>(string cond_, string dbName_ = null) where T : class, IorientDefaultObject;
      IEnumerable<T> SelectTraverseWithOffset<T, outE, inV, inE, inE2>(string ID_, string depthPropName_, int depthFrom_, int? depthOfset_ = null, string dbName_ = null) where T : class, IorientDefaultObject;
      T TestDeserialize<T>(string item_) where T : class;
      IEnumerable<T> TraverseFrom<T, outE, inV, inE, inE2>(string ID_, string dbName_ = null) where T : class, IorientDefaultObject;
      IEnumerable<T> TraverseInID<T>(string fromId_, List<Type> types_, string cond_ = null, string dbName_ = null) where T : class, IOrientVertex;
      T UpdateEntity<T>(string item_, string dbName_ = null) where T : class, IorientDefaultObject;
      T UpdateEntity<T>(T item_, string dbName_ = null) where T : class, IorientDefaultObject;
      T UpdateProperties<T>(T fromObject, T toObject) where T : class, IorientDefaultObject;
    }

    //<<< obsolette
    public interface ITokenBuilder
    {

        List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject);
        List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType);
        List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType, ITypeToken context_);
        List<ITypeToken> Command(ITypeToken command_, ITypeToken orientObject, ITypeToken orientType, ITypeToken tokenA, ITypeToken tokenB, ITypeToken content );       

    }
    public interface ITokenBuilderNoGen
    {

        List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_, ITypeToken orientObjectToken_, ITypeToken content=null);
        List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_, ITypeToken orientObject, ITypeToken from, ITypeToken to, ITypeToken content=null);
        List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_, ITypeToken orientObject);

    }
    public interface ITokenBuilderTypeGen
    {
        List<ITypeToken> Command(ITypeToken name_, ITypeToken type_);
        List<ITypeToken> Command(ITypeToken command_, Type orientClass_, ITypeToken content=null);
        List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_, ITypeToken content=null);
        List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_,  ITypeToken from, ITypeToken to, ITypeToken content=null);
        List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_);
        List<ITypeToken> Command(ITypeToken command_, IOrientObject orientClass_,IOrientObject orientProperty_, ITypeToken orientType_, bool mandatory =false, bool notnull=false);
    }
   

    
}